﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace sdlife.web.Models
{
    public class PagedList<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }

    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> CreatePagedList<T>(this IQueryable<T> input, PagedListQuery query)
        {
            var itemsInput = input;
            if (query.SortString().HasValue)
            {
                itemsInput = itemsInput
                    .OrderBy(query.SortString().Value);
            }
            itemsInput = itemsInput.Skip(query.Skip).Take(query.Take);

            return new PagedList<T>
            {
                TotalCount = await input.CountAsync().ConfigureAwait(false),
                Items = await itemsInput.ToListAsync().ConfigureAwait(false)
            };
        }

        public static Task<PagedList<T>> CreateSqlPagedList<T>(this IQueryable<T> input, SqlPagedListQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
