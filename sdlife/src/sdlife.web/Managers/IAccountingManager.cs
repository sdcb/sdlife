using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using System;
using sdlife.web.Dtos;
using System.Collections.Generic;

namespace sdlife.web.Managers
{
    public interface IAccountingManager
    {
        IAccountingPrivilegeManager Privilege { get; }

        Task<Result<AccountingDto>> Create(AccountingDto dto, int createUserId);
        Task<List<string>> SearchIncomeTitles(string titleQuery, int limit = 20);
        Task<List<string>> SearchSpendingTitles(string titleQuery, int limit = 20);
        Task<Result> UpdateTime(int accountId, DateTime time);
        Task<Result<AccountingDto>> Update(AccountingDto dto);

        Task UpdateTitleShortCuts();
        
        Task<IQueryable<AccountingDto>> UserAccountingInRange(DateTime start, DateTime end, int userId);
        Task<Result> Delete(int id);
        Task<PagedList<AccountingDto>> GetAccountingPagedList(AccountingPagedListQuery query);
        Result<Task<PagedList<AccountingDto>>> GetAccountingPagedList(SqlPagedListQuery query);
    }
}