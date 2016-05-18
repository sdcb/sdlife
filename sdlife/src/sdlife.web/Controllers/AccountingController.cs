﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using sdlife.web.Managers;
using sdlife.web.Dtos;
using sdlife.web.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

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
        
        public async Task<AccountingDto> CreateSpend([FromBody]AccountingDto dto)
        {
            return await _accounting.CreateSpending(dto);
        }

        public async Task<AccountingDto> CreateIncome([FromBody]AccountingDto dto)
        {
            return await _accounting.CreateIncome(dto);
        }

        public async Task UpdateTime(int id, [FromBody]DateTime time)
        {
            await _accounting.UpdateTime(id, time);
        }

        public async Task<AccountingDto> Update([FromBody]AccountingDto dto)
        {
            return await _accounting.UpdateSpending(dto);
        }

        public async Task Delete(int id)
        {
            await _accounting.Delete(id);
        }

        public IQueryable<AccountingDto> MyAccountingInRange([FromBody]AccountingQuery query)
        {
            var data = _accounting.UserAccountingInRange(query.From, query.To, _user.UserId);
            return data;
        }

        public async Task<IEnumerable<string>> SearchTitle([FromBody]SearchQueryDto query)
        {
            var data = await _accounting.SearchSpendingTitles(query.Query);
            return data;
        }
    }
}
