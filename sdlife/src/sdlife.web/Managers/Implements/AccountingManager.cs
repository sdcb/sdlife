using Microsoft.Data.Entity;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Managers.Implements
{
    public class AccountingManager : IAccountingManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUser _user;
        private readonly ITimeService _time;
        private readonly IPinYinConverter _pinYin;

        public AccountingManager(
            ApplicationDbContext db,
            ICurrentUser user,
            ITimeService time,
            IPinYinConverter pinYin)
        {
            _db = db;
            _user = user;
            _time = time;
            _pinYin = pinYin;
        }

        public async Task<AccountingDto> Create(AccountingDto dto)
        {
            var titleEntity = await GetOrCreateTitle(dto.Title).ConfigureAwait(false);

            var entity = new Accounting
            {
                Amount = dto.Amount,
                CreateUserId = _user.UserId,
                EventTime = dto.Time,
                CreateTime = _time.Now,
                Title = titleEntity,
            };

            if (!string.IsNullOrWhiteSpace(dto.Comment))
            {
                entity.Comment = new AccountingComment
                {
                    Comment = dto.Comment
                };
            }

            _db.Add(entity);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            _db.Entry(entity).State = EntityState.Detached;
            _db.Entry(entity.Title).State = EntityState.Detached;
            if (entity.Comment != null)
            {
                _db.Entry(entity.Comment).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task<List<string>> SearchTitles(string titleQuery, int limit)
        {
            var query = await _db.AccountingTitle
                .Include(x => x.Accountings)
                .Where(x => x.Title.StartsWith(titleQuery) || x.ShortCut.StartsWith(titleQuery))
                .ToListAsync().ConfigureAwait(false);
            return query
                .OrderByDescending(x => x.Accountings.Count)
                .Select(x => x.Title)
                .Take(limit)
                .ToList();
        }

        public async Task UpdateTime(int accountId, DateTime time)
        {
            var entity = await _db.Accounting
                .Where(x => x.Id == accountId)
                .SingleAsync().ConfigureAwait(false);

            entity.EventTime = time;
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<decimal> MyTotalAmountInRange(DateTime start, DateTime end)
        {
            var amount = await _db.Accounting
                .Where(x => x.EventTime >= start && x.EventTime < end)
                .SumAsync(x => x.Amount).ConfigureAwait(false);
            return amount;
        }

        public async Task<AccountingDto> Update(AccountingDto dto)
        {
            var entity = await _db.Accounting
                .Include(x => x.Title)
                .Include(x => x.Comment)
                .Where(x => x.Id == dto.Id)
                .SingleAsync().ConfigureAwait(false);

            if (entity.Title.Title != dto.Title)
            {
                entity.Title = await GetOrCreateTitle(dto.Title).ConfigureAwait(false);
            }

            if (entity.EventTime != dto.Time)
            {
                entity.EventTime = dto.Time;
            }

            if (entity.Amount != dto.Amount)
            {
                entity.Amount = dto.Amount;
            }

            if (entity.Comment?.Comment != dto.Comment)
            {
                if (string.IsNullOrWhiteSpace(dto.Comment))
                {
                    entity.Comment = null;
                }
                else if (entity.Comment == null)
                {
                    entity.Comment = new AccountingComment
                    {
                        Comment = dto.Comment
                    };
                }
                else
                {
                    entity.Comment.Comment = dto.Comment;
                }
            }

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public IQueryable<AccountingDto> MyAccountingInRange(DateTime start, DateTime end)
        {
            //return _db.Accounting
            //    .Where(x => 
            //        x.EventTime >= start && 
            //        x.EventTime < end && 
            //        x.CreateUserId == _user.UserId)
            //    .ToDto();
            return _db.Accounting
                .Include(x => x.Comment)
                .Include(x => x.Title)
                .Where(x =>
                    x.EventTime >= start &&
                    x.EventTime < end &&
                    x.CreateUserId == _user.UserId)
                .ToList().Select(x => (AccountingDto)x).AsQueryable();
        }

        #region private functions 
        private async Task<AccountingTitle> GetOrCreateTitle(string title)
        {
            var result = await _db.AccountingTitle
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (result != null)
            {
                return result;
            }
            else
            {
                return await CreateTitle(title).ConfigureAwait(false);
            }
        }

        private async Task<AccountingTitle> CreateTitle(string title)
        {
            var newOne = new AccountingTitle
            {
                CreateTime = DateTime.Now,
                CreateUserId = _user.UserId,
                Title = title,
                ShortCut = _pinYin.GetStringCapitalPinYin(title)
            };
            _db.Add(newOne);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return newOne;
        }

        private async Task<List<AccountingTitle>> DeleteUnreferencedTitle()
        {
            var titles = await _db.AccountingTitle
                .Where(x => x.Accountings.Count == 0)
                .ToListAsync().ConfigureAwait(false);

            _db.RemoveRange(titles);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return titles;
        }
        #endregion
    }
}
