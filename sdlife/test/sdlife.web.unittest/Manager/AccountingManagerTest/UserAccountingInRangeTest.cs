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
            }, User.UserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1), 
                new DateTime(2016, 1, 1), user.UserId)).Single();

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
            }, User.UserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), user.UserId)).ToList().FirstOrDefault();

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
            }, User.UserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), user.UserId + 1)).ToList().FirstOrDefault();

            // Assert
            Assert.Null(real);
        }

        [Fact]
        public async Task WithAccessCanGetOthers()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var currentUserId = user.UserId;
            var otherUserId = user.UserId + 1;
            await accountingManager.Privilege.SetUserAuthroize(otherUserId, currentUserId,
                AccountingAuthorizeLevel.QueryAll | AccountingAuthorizeLevel.Modify);
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "test"
            }, otherUserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), otherUserId)).ToList().FirstOrDefault();

            // Assert
            Assert.NotNull(real);
        }

        [Fact]
        public async Task WithoutAccessCannotGetOthers()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var currentUserId = user.UserId;
            var otherUserId = user.UserId + 1;
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "test"
            }, otherUserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), otherUserId)).ToList().FirstOrDefault();

            // Assert
            Assert.Null(real);
        }

        [Fact]
        public async Task WithPartAccessCanOnlyGetPartOthers()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = ServiceProvider.GetService<ICurrentUser>();
            var currentUserId = user.UserId;
            var otherUserId = user.UserId + 1;
            await accountingManager.Privilege.SetUserAuthroize(otherUserId, currentUserId, 
                AccountingAuthorizeLevel.QueryIncomes | AccountingAuthorizeLevel.Modify);
            var income = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "income", 
                IsIncome = true, 
            }, otherUserId);
            var spending = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = new DateTime(2015, 1, 1),
                Title = "spending",
                IsIncome = false,
            }, otherUserId);

            // Action
            var real = (await accountingManager.UserAccountingInRange(
                new DateTime(2014, 1, 1),
                new DateTime(2016, 1, 1), otherUserId)).ToList();

            // Assert
            Assert.Equal(1, real.Count);
            Assert.Equal("income", real[0].Title);
        }
    }
}
