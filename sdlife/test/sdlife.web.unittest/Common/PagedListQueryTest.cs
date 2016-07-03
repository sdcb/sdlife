using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Common
{
    public class PagedListQueryTest
    {
        [Fact]
        public void NormalCase()
        {
            var query = new PagedListQuery
            {
                Page = 3,
                PageSize = 12,
                Asc = true,
                OrderBy = "Title"
            };

            Assert.Equal(24, query.Skip);
            Assert.Equal(12, query.Take);
            Assert.Equal("Title ASC", query.SortString);
        }

        [Fact]
        public void WillUseDefaultPageSize()
        {
            var query = new PagedListQuery
            {
                Page = 3,
                PageSize = null,
                Asc = true,
                OrderBy = "Title"
            };

            Assert.Equal(PagedListQuery.DefaultPageSize, query.Take);
        }

        [Fact]
        public void NegativePageWillBe1()
        {
            var query = new PagedListQuery
            {
                Page = -3,
                PageSize = 12,
                Asc = true,
                OrderBy = "Title"
            };

            Assert.Equal(0, query.Skip);
        }

        [Fact]
        public void NegativePageSizeWillBe1()
        {
            var query = new PagedListQuery
            {
                Page = -3,
                PageSize = -12,
                Asc = true,
                OrderBy = "Title"
            };

            Assert.Equal(0, query.Skip);
        }

        [Fact]
        public void HugePageSizeWillBeDefaultMax()
        {
            var query = new PagedListQuery
            {
                Page = 1,
                PageSize = PagedListQuery.MaxPageSize + 1,
                Asc = true,
                OrderBy = "Title"
            };

            Assert.Equal(PagedListQuery.MaxPageSize, query.Take);
        }

        [Fact]
        public void FalseAscWillEmitDESC()
        {
            var query = new PagedListQuery
            {
                Page = 1,
                PageSize = 12,
                Asc = false,
                OrderBy = "Title"
            };

            Assert.Equal("Title DESC", query.SortString);
        }

        [Fact]
        public void NullAscWillEmitNullSortString()
        {
            var query = new PagedListQuery
            {
                Page = 3,
                PageSize = 12,
                Asc = null,
                OrderBy = "Title"
            };

            Assert.Equal(null, query.SortString);
        }

        [Fact]
        public void NullStringWillEmitNullSortString()
        {
            var query = new PagedListQuery
            {
                Page = 3,
                PageSize = 12,
                Asc = true,
                OrderBy = null
            };

            Assert.Equal(null, query.SortString);
        }
    }
}
