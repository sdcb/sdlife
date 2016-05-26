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
        Task<AccountingDto> Create(AccountingDto dto);
        Task<List<string>> SearchIncomeTitles(string titleQuery, int limit = 20);
        Task<List<string>> SearchSpendingTitles(string titleQuery, int limit = 20);
        Task UpdateTime(int accountId, DateTime time);
        Task<AccountingDto> Update(AccountingDto dto);

        Task UpdateTitleShortCuts();

        Task<decimal> MyTotalAmountInRange(DateTime start, DateTime end);
        IQueryable<AccountingDto> UserAccountingInRange(DateTime start, DateTime end, int userId);
        Task Delete(int id);
    }
}