using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using System;
using sdlife.web.Dtos;
using System.Collections.Generic;

namespace sdlife.web.Managers
{
    public interface IAccountingPrivilegeManager
    {
        Task<bool> CheckUserAuthorization(int userId, int targetUserId, AccountingAuthorizeLevel level);
        Task Set(int userId, int authorizedUserId, AccountingAuthorizeLevel level);
        IQueryable<AccountingUserAuthorization> AuthorizedUsers(int userId);
        Task<bool> CanIModify(int authorizedUserId);
    }
}