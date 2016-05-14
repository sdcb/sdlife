using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using System;
using sdlife.web.ViewModels;
using System.Collections.Generic;

namespace sdlife.web.Managers
{
    public interface IAccountingManager
    {
        Task<AccountingDto> Create(AccountingDto dto);
        Task<List<string>> SearchTitles(string titleQuery, int limit = 20);
        Task UpdateTime(int accountId, DateTime time);
        Task<AccountingDto> Update(AccountingDto dto);

        Task<decimal> MyTotalAmountInRange(DateTime start, DateTime end);
        IQueryable<AccountingDto> MyAccountingInRange(DateTime from, DateTime to);
    }
}