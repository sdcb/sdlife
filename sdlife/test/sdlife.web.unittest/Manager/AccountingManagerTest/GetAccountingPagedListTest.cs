using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Services.Implements;
using sdlife.web.unittest.Common;
using sdlife.web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager.AccountingManagerTest
{
    public class GetAccountingPagedListTest : TestBase
    {
        [Fact]
        public async Task UserIdQuery()
        {
            // Arrange 
            await ArrangeSampleData();

            // Action
            var real = (await AccountingManager.GetAccountingPagedList(new AccountingPagedListQuery
            {
                UserId = User.UserId
            }));

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
            Assert.Equal(PagedListQuery.DefaultPageSize, real.Items.Count);
            var data = real.Items.FirstOrDefault();
            Assert.NotNull(data);
            Assert.Equal(SampleData.Amount, data.Amount);
            Assert.Equal(SampleData.Time, data.Time);
            Assert.Equal(SampleData.Title, data.Title);
        }

        [Fact]
        public async Task WrongUserIdQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.UserId = User.UserId + 1;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task TitleQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.Title = SampleData.Title;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongTitleQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.Title = SampleData.Title + "wrong";

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task TitleStartsWithQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.Title = SampleData.Title.Substring(0, 2);

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task TitlesQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.Titles = new List<string> { SampleData.Title, "test" };

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongTitlesQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.Titles = new List<string> { SampleData.Title + "test" };

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task FromQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.From = SampleData.Time;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongFromQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.From = SampleData.Time.AddSeconds(1);

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task ToQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.To = SampleData.Time.AddSeconds(1);

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongToQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.To = SampleData.Time;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task IsIncomeQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.IsIncome = SampleData.IsIncome;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongIsIncomeQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.IsIncome = !SampleData.IsIncome;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task MinAmountQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.MinAmount = SampleData.Amount;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongMinAmountQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.MinAmount = SampleData.Amount + 1;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task MaxAmountQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.MaxAmount = SampleData.Amount + 1;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task WrongMaxAmountQuery()
        {
            // Arrange 
            await ArrangeSampleData();
            var query = GetDefaultQueryObject();
            query.MaxAmount = SampleData.Amount;

            // Action
            var real = await AccountingManager.GetAccountingPagedList(query);

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        private AccountingPagedListQuery GetDefaultQueryObject()
        {
            return new AccountingPagedListQuery
            {
                UserId = User.UserId
            };
        }

        public int SampleDataCount = 25;
        private async Task ArrangeSampleData()
        {
            for (var i = 0; i < SampleDataCount; ++i)
            {
                await AccountingManager.Create(SampleData, User.UserId);
            }
        }

        private readonly AccountingDto SampleData = new AccountingDto
        {
            Amount = 2,
            Time = new DateTime(2015, 1, 1),
            Title = "test"
        };

        public IAccountingManager AccountingManager
        {
            get
            {
                return ServiceProvider.GetRequiredService<IAccountingManager>();
            }
        }

        public ApplicationDbContext Db
        {
            get
            {
                return ServiceProvider.GetRequiredService<ApplicationDbContext>();
            }
        }
    }
}
