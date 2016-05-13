using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using sdlife.web.Models;
using sdlife.web.Managers;

namespace sdlife.web.Controllers
{
    public class AccountingController : Controller
    {
        private readonly IAccountingManager _accounting;

        public AccountingController(IAccountingManager accounting)
        {
            _accounting = accounting;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Task<Accounting> Create(string title, decimal amount, string comment, DateTime time)
        {
            return _accounting.Create(title, amount, comment, time);
        }
    }
}
