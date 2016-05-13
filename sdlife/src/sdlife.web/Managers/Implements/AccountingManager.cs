using Microsoft.Data.Entity;
using sdlife.web.Models;
using sdlife.web.Services;
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

        public AccountingManager(
            ApplicationDbContext db, 
            ICurrentUser user, 
            ITimeService time)
        {
            _db = db;
            _user = user;
            _time = time;
        }

        public IQueryable<Accounting> Get()
        {
            return _db.Accounting
                .Include(x => x.Comment)
                .Include(x => x.CreateUser)
                .Include(x => x.Title);
        }

        public async Task<Accounting> Create(string title, decimal amount, string comment, DateTime time)
        {
            var titleEntity = await GetOrCreateTitle(title).ConfigureAwait(false);

            var entity = new Accounting
            {
                Amount = amount, 
                Comment = new AccountingComment
                {
                    Comment = comment
                }, 
                CreateUserId = _user.UserId, 
                EventTime = time, 
                CreateTime = _time.Now, 
                Title = titleEntity, 
            };

            _db.Add(entity);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<AccountingTitle> GetOrCreateTitle(string title)
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

        public IQueryable<AccountingTitle> SearchTitles(string titleQuery)
        {
            return _db.AccountingTitle
                .Where(x => x.Title.StartsWith(titleQuery));
        }

        public async Task<AccountingTitle> CreateTitle(string title)
        {
            var newOne = new AccountingTitle
            {
                CreateTime = DateTime.Now,
                CreateUserId = _user.UserId,
                Title = title
            };
            _db.Add(newOne);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return newOne;
        }

        public async Task UpdateComment(int accountId, string comment)
        {
            var account = _db.Accounting
                .Include(x => x.Comment)
                .Where(x => x.Id == accountId)
                .Single();

            if (!string.IsNullOrWhiteSpace(comment))
            {
                if (account.Comment == null)
                {
                    await CreateComment(accountId, comment).ConfigureAwait(false);
                }
                else
                {
                    await UpdateComment(account.Comment, comment).ConfigureAwait(false);
                }
            }
            else if (account.Comment != null)
            {
                await DeleteComment(account.Comment).ConfigureAwait(false);
            }
        }

        private async Task<AccountingComment> CreateComment(int accountId, string comment)
        {
            var commentEntity = new AccountingComment
            {
                AccountingId = accountId,
                Comment = comment
            };

            _db.Add(commentEntity);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return commentEntity;
        }

        private async Task DeleteComment(AccountingComment comment)
        {
            _db.Remove(comment);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task UpdateComment(AccountingComment entity, string comment)
        {
            entity.Comment = comment;
            _db.Update(entity);

            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateTitle(int accountId, string title)
        {
            var entity = await _db.Accounting
                .Where(x => x.Id == accountId)
                .SingleAsync().ConfigureAwait(false);

            var titleEntity = await GetOrCreateTitle(title).ConfigureAwait(false);

            entity.TitleId = titleEntity.Id;
            await _db.SaveChangesAsync().ConfigureAwait(false);
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
    }
}
