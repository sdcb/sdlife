﻿using sdlife.web.Models;
using sdlife.web.Models.SqlAntlr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos
{
    public class AccountingDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        public bool IsIncome { get; set; }

        public string Comment { get; set; }

        public static implicit operator AccountingDto(Accounting x)
        {
            return new AccountingDto
            {
                Id = x.Id,
                Amount = x.Amount,
                Comment = x.Comment?.Comment,
                Time = x.EventTime,
                IsIncome = x.Title.IsIncome, 
                Title = x.Title.Title
            };
        }
    }

    public class AccountingDtoWithUser
    {
        public int Id { get; set; }

        [QueryField(Name = "title")]
        public string Title { get; set; }

        [QueryField(Name = "amount")]
        public decimal Amount { get; set; }

        [QueryField(Name = "time")]
        public DateTime Time { get; set; }

        [QueryField(Name = "isIncome")]
        public bool IsIncome { get; set; }

        [QueryField(Name = "comment")]
        public string Comment { get; set; }

        [QueryField(Name = "userId")]
        public int UserId { get; set; }
    }

    public static class AccountingDtoExtensions
    {
        public static IQueryable<AccountingDto> ToDto(this IQueryable<Accounting> db)
        {
            return db.Select(x => new AccountingDto
            {
                Id = x.Id,
                Amount = x.Amount,
                Comment = x.Comment.Comment,
                Time = x.EventTime,
                IsIncome = x.Title.IsIncome, 
                Title = x.Title.Title
            });
        }
    }
}
