using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using sdlife.web.Managers;
using sdlife.web.Dtos;
using sdlife.web.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace sdlife.web.Controllers
{
    [Authorize]
    public class AccountingController : SdlifeBaseController
    {
        private readonly IAccountingManager _accounting;
        private readonly ICurrentUser _user;

        public AccountingController(
            IAccountingManager accounting,
            ICurrentUser user)
        {
            _accounting = accounting;
            _user = user;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<AccountingDto> Create([FromBody]AccountingDto dto)
        {
            return await _accounting.Create(dto, _user.UserId);
        }

        public async Task UpdateTime(int id, [FromBody]DateTime time)
        {
            await _accounting.UpdateTime(id, time);
        }

        public async Task<AccountingDto> Update([FromBody]AccountingDto dto)
        {
            return await _accounting.Update(dto);
        }

        public async Task Delete(int id)
        {
            await _accounting.Delete(id);
        }

        public async Task<IQueryable<AccountingDto>> MyAccountingInRange([FromBody]AccountingQuery query)
        {
            var data = await _accounting.UserAccountingInRange(query.From, query.To, _user.UserId);
            return data;
        }

        public async Task<IEnumerable<string>> SearchSpendingTitles([FromBody]SearchQueryDto query)
        {
            var data = await _accounting.SearchSpendingTitles(query.Query);
            return data;
        }

        public async Task<IEnumerable<string>> SearchIncomeTitles([FromBody]SearchQueryDto query)
        {
            var data = await _accounting.SearchIncomeTitles(query.Query);
            return data;
        }
    }
}
