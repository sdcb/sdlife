using Microsoft.EntityFrameworkCore;
using sdlife.web.Data;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using sdlife.web.Models.SqlAntlr;

namespace sdlife.web.Managers.Implements
{
    public class AccountingManager : IAccountingManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUser _user;
        private readonly ITimeService _time;
        private readonly IPinYinConverter _pinYin;

        public IAccountingPrivilegeManager Privilege { get; }

        public AccountingManager(
            ApplicationDbContext db,
            ICurrentUser user,
            ITimeService time,
            IPinYinConverter pinYin,
            IAccountingPrivilegeManager privilege)
        {
            _db = db;
            _user = user;
            _time = time;
            _pinYin = pinYin;
            Privilege = privilege;
        }

        public async Task<Result<AccountingDto>> Create(AccountingDto dto, int createUserId)
        {
            if (!await Privilege.CanIModify(createUserId))
            {
                return NoPrivilegeFail<AccountingDto>();
            }

            var titleEntity = await GetOrCreateTitle(dto.Title, dto.IsIncome);

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
            await _db.SaveChangesAsync();

            _db.Entry(entity).State = EntityState.Detached;
            _db.Entry(entity.Title).State = EntityState.Detached;
            if (entity.Comment != null)
            {
                _db.Entry(entity.Comment).State = EntityState.Detached;
            }

            return Result.Ok((AccountingDto)entity);
        }

        private Result<T> NoPrivilegeFail<T>()
        {
            return Result.Fail<T>("你没有修改此用户的权限。");
        }

        private Result NoPrivilegeFail()
        {
            return Result.Fail("你没有修改此用户的权限。");
        }

        public async Task<List<string>> SearchIncomeTitles(string titleQuery, int limit)
        {
            return await SearchTitlesInternal(titleQuery, true, limit);
        }

        public async Task<List<string>> SearchSpendingTitles(string titleQuery, int limit = 20)
        {
            return await SearchTitlesInternal(titleQuery, false, limit);
        }

        private async Task<List<string>> SearchTitlesInternal(string titleQuery, bool isIncome, int limit)
        {
            var before = DateTime.Now.AddMonths(-1);
            return await _db.AccountingTitle
                .Include(x => x.Accountings)
                .Where(x => x.IsIncome == isIncome)
                .Where(x => x.Title.StartsWith(titleQuery) || x.ShortCut.StartsWith(titleQuery))
                .OrderByDescending(x => x.Accountings.Count(a => a.EventTime >= before))
                .Select(x => x.Title)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Result> UpdateTime(int accountId, DateTime time)
        {
            var entity = await _db.Accounting
                .Where(x => x.Id == accountId)
                .SingleAsync();

            if (!await Privilege.CanIModify(entity.CreateUserId))
            {
                return NoPrivilegeFail();
            }

            entity.EventTime = time;
            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result<AccountingDto>> Update(AccountingDto dto)
        {
            var entity = await _db.Accounting
                .Include(x => x.Title)
                .Include(x => x.Comment)
                .Where(x => x.Id == dto.Id)
                .SingleAsync();

            if (!await Privilege.CanIModify(entity.CreateUserId))
            {
                return NoPrivilegeFail<AccountingDto>();
            }

            if (entity.Title.Title != dto.Title)
            {
                var oldTitle = entity.Title;
                entity.Title = await GetOrCreateTitle(dto.Title, dto.IsIncome);

                var titleRefCount = await _db.Accounting
                    .CountAsync(x => x.Id != entity.Id && x.TitleId == entity.TitleId);
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

            await _db.SaveChangesAsync();
            return Result.Ok((AccountingDto)entity);
        }

        public async Task<PagedList<AccountingDto>> GetAccountingPagedList(AccountingPagedListQuery query)
        {
            var data =
                from accounting in _db.Accounting
                join title in _db.AccountingTitle on accounting.TitleId equals title.Id
                join comment in _db.AccountingComment on accounting.Id equals comment.AccountingId into commentGroup
                from comment in commentGroup.DefaultIfEmpty()
                where accounting.CreateUserId == query.UserId
                select new AccountingDto
                {
                    Id = accounting.Id,
                    Amount = accounting.Amount,
                    Comment = comment == null ? null : comment.Comment,
                    Time = accounting.EventTime,
                    IsIncome = title.IsIncome,
                    Title = title.Title
                };

            if (query.Title != null)
            {
                data = data.Where(x => x.Title.StartsWith(query.Title));
            }
            if (query.Titles != null)
            {
                data = data.Where(x => query.Titles.Contains(x.Title));
            }
            if (query.From.HasValue)
            {
                data = data.Where(x => x.Time >= query.From.Value);
            }
            if (query.To.HasValue)
            {
                data = data.Where(x => x.Time < query.To.Value);
            }
            if (query.IsIncome.HasValue)
            {
                data = data.Where(x => x.IsIncome == query.IsIncome.Value);
            }
            if (query.MinAmount.HasValue)
            {
                data = data.Where(x => x.Amount >= query.MinAmount.Value);
            }
            if (query.MaxAmount.HasValue)
            {
                data = data.Where(x => x.Amount < query.MaxAmount.Value);
            }

            return await data.CreatePagedList(query);
        }

        public Result<Task<PagedList<AccountingDto>>> GetAccountingPagedList(SqlPagedListQuery query)
        {
            var data =
                from accounting in _db.Accounting
                join title in _db.AccountingTitle on accounting.TitleId equals title.Id
                join comment in _db.AccountingComment on accounting.Id equals comment.AccountingId into commentGroup
                from comment in commentGroup.DefaultIfEmpty()
                select new AccountingDtoWithUser
                {
                    Id = accounting.Id,
                    Amount = accounting.Amount,
                    Comment = comment == null ? null : comment.Comment,
                    Time = accounting.EventTime,
                    IsIncome = title.IsIncome,
                    Title = title.Title, 
                    UserId = accounting.CreateUserId
                };

            return data
                .Filter(query.Sql)
                .OnSuccess(q => q.Select(v => new AccountingDto
                {
                    Id = v.Id,
                    Amount = v.Amount,
                    Comment = v.Comment,
                    Time = v.Time,
                    IsIncome = v.IsIncome,
                    Title = v.Title
                }))
                .OnSuccess(async v =>
                {
                    return await v.CreatePagedList(query);
                });
        }

        public async Task<IQueryable<AccountingDto>> UserAccountingInRange(DateTime start, DateTime end, int userId)
        {
            var query =
                from accounting in _db.Accounting
                join title in _db.AccountingTitle on accounting.TitleId equals title.Id
                join comment in _db.AccountingComment on accounting.Id equals comment.AccountingId into commentGroup
                from comment in commentGroup.DefaultIfEmpty()
                where accounting.EventTime >= start && accounting.EventTime < end && accounting.CreateUserId == userId
                orderby accounting.EventTime descending
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
                var access = await GetUserAccess(_user.UserId, userId);
                if ((access & AccountingAuthorizeLevel.QueryAll) == AccountingAuthorizeLevel.QueryAll)
                {
                    return query;
                }
                else if ((access & AccountingAuthorizeLevel.QueryIncomes) == AccountingAuthorizeLevel.QueryIncomes)
                {
                    return query.Where(x => x.IsIncome);
                }
                else if ((access & AccountingAuthorizeLevel.QuerySpendings) == AccountingAuthorizeLevel.QuerySpendings)
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
                .FirstOrDefaultAsync();
        }

        public async Task<Result> Delete(int id)
        {
            var result = await _db.Accounting
                .Include(x => x.Title)
                .Include(x => x.Comment)
                .SingleAsync(x => x.Id == id);

            if (!await Privilege.CanIModify(result.CreateUserId))
            {
                return NoPrivilegeFail();
            }

            _db.Remove(result);
            var titleRefCount = await _db.Accounting
                .CountAsync(x => x.Id != id && x.TitleId == result.TitleId);
            if (titleRefCount == 0)
            {
                _db.Remove(result.Title);
            }

            await _db.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task UpdateTitleShortCuts()
        {
            var data = await _db.AccountingTitle.ToListAsync();
            foreach (var item in data)
            {
                item.ShortCut = _pinYin.GetStringCapitalPinYin(item.Title);
            }
            await _db.SaveChangesAsync();
        }

        #region private functions 
        private async Task<AccountingTitle> GetOrCreateTitle(string title, bool isIncome)
        {
            var result = await _db.AccountingTitle
                .Where(x => x.Title == title && x.IsIncome == isIncome)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                return result;
            }
            else
            {
                return await CreateTitle(title, isIncome);
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

            await _db.SaveChangesAsync();
            return newOne;
        }

        #endregion
    }
}
