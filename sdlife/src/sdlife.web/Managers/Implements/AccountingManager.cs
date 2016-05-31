﻿using Microsoft.EntityFrameworkCore;
using sdlife.web.Data;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Dtos;
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

        public async Task<AccountingDto> Create(AccountingDto dto, int createUserId)
        {
            var titleEntity = await GetOrCreateTitle(dto.Title, dto.IsIncome).ConfigureAwait(false);

            var entity = new Accounting
            {
                Amount = dto.Amount,
                CreateUserId = createUserId,
                EventTime = dto.Time,
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

        public async Task<List<string>> SearchIncomeTitles(string titleQuery, int limit)
        {
            return await SearchTitlesInternal(titleQuery, true, limit).ConfigureAwait(false);
        }

        public async Task<List<string>> SearchSpendingTitles(string titleQuery, int limit = 20)
        {
            return await SearchTitlesInternal(titleQuery, false, limit).ConfigureAwait(false);
        }

        private async Task<List<string>> SearchTitlesInternal(string titleQuery, bool isIncome, int limit)
        {
            return await _db.AccountingTitle
                .Include(x => x.Accountings)
                .Where(x => x.IsIncome == isIncome)
                .Where(x => x.Title.StartsWith(titleQuery) || x.ShortCut.StartsWith(titleQuery))
                .OrderByDescending(x => x.Accountings.Count())
                .Select(x => x.Title)
                .Take(limit)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateTime(int accountId, DateTime time)
        {
            var entity = await _db.Accounting
                .Where(x => x.Id == accountId)
                .SingleAsync().ConfigureAwait(false);

            entity.EventTime = time;
            await _db.SaveChangesAsync().ConfigureAwait(false);
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
                var oldTitle = entity.Title;
                entity.Title = await GetOrCreateTitle(dto.Title, dto.IsIncome).ConfigureAwait(false);

                var titleRefCount = await _db.Accounting
                    .CountAsync(x => x.Id != entity.Id && x.TitleId == entity.TitleId).ConfigureAwait(false);
                if (titleRefCount == 0)
                {
                    _db.Remove(oldTitle);
                }
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

        public async Task<IQueryable<AccountingDto>> UserAccountingInRange(DateTime start, DateTime end, int userId)
        {
            var query = 
                from accounting in _db.Accounting
                join title in _db.AccountingTitle on accounting.TitleId equals title.Id 
                join comment in _db.AccountingComment on accounting.Id equals comment.AccountingId into commentGroup
                from comment in commentGroup.DefaultIfEmpty()
                where accounting.EventTime >= start && accounting.EventTime < end && accounting.CreateUserId == userId
                select new AccountingDto
                {
                    Id = accounting.Id,
                    Amount = accounting.Amount,
                    Comment = comment == null ? null : comment.Comment,
                    Time = accounting.EventTime,
                    IsIncome = title.IsIncome, 
                    Title = title.Title
                };
            if (_user.UserId == userId)
            {
                return query;
            }
            else
            {
                var access = await GetUserAccess(_user.UserId, userId).ConfigureAwait(false);
                if (access == AccountingAuthorizeLevel.QueryAll)
                {
                    return query;
                }
                else if (access == AccountingAuthorizeLevel.QueryIncomes)
                {
                    return query.Where(x => x.IsIncome);
                }
                else if (access == AccountingAuthorizeLevel.QuerySpendings)
                {
                    return query.Where(x => !x.IsIncome);
                }
                else
                {
                    return new List<AccountingDto>().AsQueryable();
                }
            }
        }

        private async Task<AccountingAuthorizeLevel> GetUserAccess(int userId, int targetUserId)
        {
            return await _db.AccountingUserAuthorization
                .Where(x => x.AuthorizedUserId == userId && x.UserId == targetUserId)
                .Select(x => x.Level)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            var result = await _db.Accounting
                .Include(x => x.Title)
                .Include(x => x.Comment)
                .SingleAsync(x => x.Id == id).ConfigureAwait(false);

            _db.Remove(result);
            var titleRefCount = await _db.Accounting
                .CountAsync(x => x.Id != id && x.TitleId == result.TitleId)
                .ConfigureAwait(false);
            if (titleRefCount == 0)
            {
                _db.Remove(result.Title);
            }

            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateTitleShortCuts()
        {
            var data = await _db.AccountingTitle.ToListAsync().ConfigureAwait(false);
            foreach (var item in data)
            {
                item.ShortCut = _pinYin.GetStringCapitalPinYin(item.Title);
            }
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> CheckUserAuthorization(int userId, int targetUserId, AccountingAuthorizeLevel level)
        {
            return await _db.AccountingUserAuthorization
                .AnyAsync(x =>
                x.UserId == targetUserId &&
                x.AuthorizedUserId == userId &&
                ((x.Level & level)  == level)).ConfigureAwait(false);
        }

        public async Task SetUserAuthroize(int userId, int authorizedUserId, AccountingAuthorizeLevel level)
        {
            var existAuthorization = await _db.AccountingUserAuthorization
                .FirstOrDefaultAsync(x => x.UserId == userId && x.AuthorizedUserId == authorizedUserId)
                .ConfigureAwait(false);

            if (level != AccountingAuthorizeLevel.None)
            {
                if (existAuthorization == null)
                {
                    existAuthorization = new AccountingUserAuthorization
                    {
                        UserId = userId,
                        AuthorizedUserId = authorizedUserId,
                        Level = level
                    };
                    _db.Add(existAuthorization);
                }
                else
                {
                    existAuthorization.Level = level;
                }
            }
            else
            {
                if (existAuthorization != null)
                {
                    _db.Remove(existAuthorization);
                }
            }

            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public IQueryable<User> AuthorizedUsers(int userId)
        {
            return _db.AccountingUserAuthorization
                .Include(x => x.User)
                .Where(x => x.AuthorizedUserId == userId)
                .Select(x => x.User);
        }

        #region private functions 
        private async Task<AccountingTitle> GetOrCreateTitle(string title, bool isIncome)
        {
            var result = await _db.AccountingTitle
                .Where(x => x.Title == title && x.IsIncome == isIncome)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (result != null)
            {
                return result;
            }
            else
            {
                return await CreateTitle(title, isIncome).ConfigureAwait(false);
            }
        }

        private async Task<AccountingTitle> CreateTitle(string title, bool isIncome)
        {
            var newOne = new AccountingTitle
            {
                Title = title,
                ShortCut = _pinYin.GetStringCapitalPinYin(title), 
                IsIncome = isIncome, 
            };
            _db.Add(newOne);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return newOne;
        }

        #endregion
    }
}
