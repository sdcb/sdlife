using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class PagedListQuery
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public Maybe<string> OrderBy { get; set; }

        public bool? Asc { get; set; }

        public int Skip
        {
            get
            {
                var negociatedPage = Page ?? DefaultPage;
                var page = Math.Max(1, negociatedPage);

                return (page - 1) * Take;
            }
        }

        public int Take
        {
            get
            {
                var negociatedPageSize = PageSize ?? DefaultPageSize;

                return Clamp(
                    negociatedPageSize, 1, MaxPageSize);
            }
        }

        public Maybe<string> SortString
        {
            get
            {
                if (OrderBy.HasValue && Asc.HasValue)
                {
                    return OrderBy + " " + (Asc.Value ? "ASC" : "DESC");
                }
                else
                {
                    return null;
                }
            }
        }

        public static int Clamp(int value, int left, int right)
        {
            if (value < left)
            {
                return left;
            }
            else if (value > right)
            {
                return right;
            }
            else
            {
                return value;
            }
        }

        public const int DefaultPage = 1;
        public const int MaxPageSize = 100;
        public const int DefaultPageSize = 20;
    }
}
