using Microsoft.EntityFrameworkCore;
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
    public class AccountingPrivilegeManager : IAccountingPrivilegeManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUser _user;
        private readonly ITimeService _time;
        private readonly IPinYinConverter _pinYin;

        public AccountingPrivilegeManager(
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

        public async Task<bool> CheckUserAuthorization(int userId, int targetUserId, AccountingAuthorizeLevel level)
        {
            if (userId == targetUserId)
            {
                return true;
            }

            return await _db.AccountingUserAuthorization
                .AnyAsync(x =>
                x.UserId == targetUserId &&
                x.AuthorizedUserId == userId &&
                ((x.Level & level) == level)).ConfigureAwait(false);
        }

        public async Task Set(int userId, int authorizedUserId, AccountingAuthorizeLevel level)
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

        public IQueryable<AccountingUserAuthorization> AuthorizedUsers(int userId)
        {
            return _db.AccountingUserAuthorization
                .Include(x => x.User)
                .Where(x => x.AuthorizedUserId == userId);
        }

        public async Task<bool> CanIModify(int targetUserId)
        {
            return await 
                CheckUserAuthorization(_user.UserId, targetUserId, AccountingAuthorizeLevel.Modify)
                .ConfigureAwait(false);
        }

        public List<IdName<AccountingAuthorizeLevel>> AllPrivileges()
        {
            return new List<IdName<AccountingAuthorizeLevel>>
            {
                IdName.Create(AccountingAuthorizeLevel.QueryIncomes, "查询收入"),
                IdName.Create(AccountingAuthorizeLevel.QuerySpendings, "查询支出"),
                IdName.Create(AccountingAuthorizeLevel.Modify, "改动"),
            };
        }
    }
}
