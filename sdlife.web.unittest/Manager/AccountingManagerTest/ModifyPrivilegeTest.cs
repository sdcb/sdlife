using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Dtos;
using sdlife.web.Managers;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager.AccountingManagerTest
{
    public class ModifyPrivilegeTest : TestBase
    {
        [Fact]
        public async Task CreateOthersWithoutPrivilegeWillFail()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 1,
                Time = DateTime.Now,
                Title = "test"
            }, User.UserId + 1);

            // Assert
            Assert.True(created.IsFailure);
        }

        [Fact]
        public async Task CreateOthersWithPrivilegeIsOk()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 1,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId);

            // Assert
            Assert.True(created.IsSuccess);
        }

        [Fact]
        public async Task DeleteOthersWithoutPrivilegeWillFail()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.None);

            // Action
            var result = await accountingManager.Delete(created.Id);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task DeleteOthersWithPrivilegeIsOk()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;

            // Action
            var result = await accountingManager.Delete(created.Id);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateOthersWithoutPrivilegeWillFail()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.None);

            // Action
            var result = await accountingManager.Update(created);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task UpdateOthersWithPrivilegeIsOk()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;

            // Action
            var result = await accountingManager.Update(created);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateOthersTimeWithoutPrivilegeWillFail()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.None);

            // Action
            var result = await accountingManager.UpdateTime(created.Id, DateTime.Now);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task UpdateOthersTimeWithPrivilegeIsOk()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var otherUserId = User.UserId + 1;
            await accountingManager.Privilege.Set(otherUserId, User.UserId, AccountingAuthorizeLevel.Modify);
            var created = (await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, otherUserId)).Value;

            // Action
            var result = await accountingManager.UpdateTime(created.Id, DateTime.Now);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
