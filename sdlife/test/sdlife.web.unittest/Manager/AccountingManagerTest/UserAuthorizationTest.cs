using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
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
    public class UserAuthorizationTest : TestBase
    {
        [Fact]
        public async Task CanAddUserAuthorize()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QuerySpendings);

            // Assert
            var authorize = await db.AccountingUserAuthorization
                .FirstOrDefaultAsync(x => x.UserId == 1 && x.AuthorizedUserId == 2);
            Assert.NotNull(authorize);
            Assert.Equal(AccountingAuthorizeLevel.QuerySpendings, authorize.Level);
        }

        [Fact]
        public async Task CanDeleteUserAuthorize()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QuerySpendings);
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.None);

            // Assert
            var authorize = await db.AccountingUserAuthorization
                .FirstOrDefaultAsync(x => x.UserId == 1 && x.AuthorizedUserId == 2);
            Assert.Null(authorize);
        }

        [Fact]
        public async Task CanUpdateUserAuthorize()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QuerySpendings);
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QueryAll);

            // Assert
            var authorize = await db.AccountingUserAuthorization
                .SingleAsync(x => x.UserId == 1 && x.AuthorizedUserId == 2);
            Assert.Equal(AccountingAuthorizeLevel.QueryAll, authorize.Level);
        }

        [Fact]
        public async Task UserAuthorizeWillPassCheck()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QuerySpendings);

            // Assert
            Assert.True(await accountingManager.CheckUserAuthorization(2, 1, AccountingAuthorizeLevel.QuerySpendings));
        }

        [Fact]
        public async Task UserNotAuthorizeWillNotPassCheck()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QuerySpendings);

            // Assert
            Assert.False(await accountingManager.CheckUserAuthorization(2, 1, AccountingAuthorizeLevel.QueryIncomes));
        }

        [Fact]
        public async Task UserContainsAuthorizeWillPassCheck()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            await accountingManager.SetUserAuthroize(1, 2, AccountingAuthorizeLevel.QueryAll);

            // Assert
            Assert.True(await accountingManager.CheckUserAuthorization(2, 1, AccountingAuthorizeLevel.QueryIncomes));
        }
    }
}
