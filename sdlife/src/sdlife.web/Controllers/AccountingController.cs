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

        public async Task<AccountingDto> Create(AccountingDto dto)
        {
            return await _accounting.Create(dto);
        }

        public IQueryable<AccountingDto> MyAccountingInRange([FromBody]JObject body)
        {
            var data = _accounting.MyAccountingInRange(body.Value<DateTime>("from"), body.Value<DateTime>("to"));
            return data;
        }
    }
}
