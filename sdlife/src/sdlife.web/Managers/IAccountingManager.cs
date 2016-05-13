using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using System;
using sdlife.web.ViewModels;

namespace sdlife.web.Managers
{
    public interface IAccountingManager
    {
        Task<AccountingDto> Create(AccountingDto dto);
        IQueryable<string> SearchTitles(string titleQuery);
        Task UpdateTime(int accountId, DateTime time);
        Task<AccountingDto> Update(AccountingDto dto);

        Task<decimal> MyTotalAmountInRange(DateTime start, DateTime end);
        IQueryable<AccountingDto> MyAccountingInRange(DateTime from, DateTime to);
    }
}