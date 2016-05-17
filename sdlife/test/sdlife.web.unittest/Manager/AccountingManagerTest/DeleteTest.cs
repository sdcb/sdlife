using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
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
    public class DeleteTest : TestBase
    {
        [Fact]
        public async Task SimpleDelete()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });

            // Action
            await accountingManager.Delete(created.Id);

            // Assert
            var data = db.Accounting.FirstOrDefault(x => x.Id == created.Id);
            Assert.Null(data);
        }

        [Fact]
        public async Task DeleteWillAlsoDeleteTitleWhenRef0()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });

            // Action
            await accountingManager.Delete(created.Id);

            // Assert
            var data = db.AccountingTitle.FirstOrDefault(x => x.Title == "test");
            Assert.Null(data);
        }

        [Fact]
        public async Task DeleteWillNotDeleteTitleWhenHasRef()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });
            var created2 = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });

            // Action
            await accountingManager.Delete(created.Id);

            // Assert
            var data = db.AccountingTitle.FirstOrDefault(x => x.Title == "test");
            Assert.NotNull(data);
        }

        [Fact]
        public async Task DeleteWillAlsoDeleteComment()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });

            // Action
            await accountingManager.Delete(created.Id);

            // Assert
            var data = db.AccountingComment.FirstOrDefault(x => x.AccountingId == created.Id);
            Assert.Null(data);
        }
    }
}
