using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using System;

namespace sdlife.web.Managers
{
    public interface IAccountingManager
    {
        Task<Accounting> Create(string title, decimal amount, string comment, DateTime time);
        IQueryable<Accounting> Get();
        IQueryable<AccountingTitle> SearchTitles(string titleQuery);
        Task UpdateComment(int accountId, string comment);
        Task UpdateTitle(int accountId, string title);
        Task UpdateTime(int accountId, DateTime time);
    }
}