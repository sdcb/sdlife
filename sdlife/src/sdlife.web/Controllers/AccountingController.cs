using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using sdlife.web.Models;
using sdlife.web.Managers;
using sdlife.web.ViewModels;
using sdlife.web.Services;
using Newtonsoft.Json.Linq;

namespace sdlife.web.Controllers
{
    public class AccountingController : Controller
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
            return await _accounting.Create(dto);
        }

        public async Task UpdateTime(int id, [FromBody]DateTime time)
        {
            await _accounting.UpdateTime(id, time);
        }

        public async Task<AccountingDto> Update([FromBody]AccountingDto dto)
        {
            return await _accounting.Update(dto);
        }

        public IQueryable<AccountingDto> MyAccountingInRange([FromBody]JObject body)
        {
            var from = body.Value<DateTime>("from");
            var to = body.Value<DateTime>("to");
            var data = _accounting.MyAccountingInRange(from, to);
            return data;
        }

        public async Task<IEnumerable<string>> SearchTitle([FromBody]JObject body)
        {
            var query = body.Value<string>("query");
            var data = await _accounting.SearchTitles(query);
            return data;
        }
    }
}
