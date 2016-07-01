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
        Task SetUserAuthroize(int userId, int authorizedUserId, AccountingAuthorizeLevel level);
        IQueryable<User> AuthorizedUsers(int userId);
        Task<bool> CanUserModify(int userId, int authorizedUserId);
    }
}