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
    public class GetAccountingSqlPagedListTest : TestBase
    {
        [Fact]
        public async Task UserIdQuery()
        {
            // Arrange 
            await ArrangeSampleData();

            // Action
            var real = await AccountingManager.GetAccountingPagedList(new SqlPagedListQuery
            {
                Sql = $"userId = {User.UserId}"
            }).Value;

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

            // Action
            var real = await AccountingManager.GetAccountingPagedList(new SqlPagedListQuery
            {
                Sql = $"userId = {User.UserId + 1}"
            }).Value;

            // Assert
            Assert.Equal(0, real.TotalCount);
        }

        [Fact]
        public async Task Parenthesis()
        {
            // Arrange 
            await ArrangeSampleData();

            // Action
            var real = await AccountingManager.GetAccountingPagedList(new SqlPagedListQuery
            {
                Sql = $"(userId = {User.UserId})"
            }).Value;

            // Assert
            Assert.Equal(SampleDataCount, real.TotalCount);
        }

        [Fact]
        public async Task UnknownFieldWillFail()
        {
            // Arrange 
            await ArrangeSampleData();

            // Action
            var real = AccountingManager.GetAccountingPagedList(new SqlPagedListQuery
            {
                Sql = $"UserIdx = {User.UserId}"
            });

            // Assert
            Assert.Equal(true, real.IsFailure);
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
