using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Services.Implements;
using sdlife.web.unittest.Common;
using sdlife.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager.AccountingManagerTest
{
    public class UserAccountingInRangeTest : TestBase
    {
        [Fact]
        public async Task WillGetDataInRange()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "test"
            });

            // Action
            var real = accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1), 
                new DateTime(2016, 1, 1), user.UserId).Single();

            // Assert
            Assert.Equal(2, real.Amount);
            Assert.Equal(new DateTime(2015, 1, 1), real.Time);
            Assert.Equal("test", real.Title);
        }

        [Fact]
        public async Task WillNotGetDataOutOfRange()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2017, 1, 1),
                Title = "test"
            });

            // Action
            var real = accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), user.UserId).ToList().FirstOrDefault();

            // Assert
            Assert.Null(real);
        }

        [Fact]
        public async Task WillNotGetDataByIncorrectUser()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "test"
            });

            // Action
            var real = accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), user.UserId + 1).ToList().FirstOrDefault();

            // Assert
            Assert.Null(real);
        }
    }
}
