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
    public class UpdateTest : TestBase
    {
        [Fact]
        public async Task SimpleUpdate()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            }, User.UserId);

            // Action
            var title = "I love lgl";
            await accountingManager.Update(new AccountingDto
            {
                Id = created.Id, 
                Amount = 500, 
                Time = now, 
                Comment = title, 
                Title = title
            });

            // Assert
            var data = await db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(500, data.Amount);
            Assert.Equal(now, data.EventTime);
            Assert.Equal(title, data.Title.Title);
            Assert.NotNull(data.Comment);
            Assert.Equal(title, data.Comment.Comment);
        }

        [Fact]
        public async Task UpdateWillEffectShortCut()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "早餐"
            }, User.UserId);

            // Action
            var title = "晚餐";
            await accountingManager.Update(new AccountingDto
            {
                Id = created.Id,
                Amount = 500,
                Time = now,
                Comment = title,
                Title = title
            });

            // Assert
            var data = await db.Accounting
                .Include(x => x.Title)
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(data.Title.ShortCut, "WC");
        }

        [Fact]
        public async Task UpdateTime()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "早餐"
            }, User.UserId);

            // Action
            await accountingManager.UpdateTime(created.Id, now);

            // Assert
            var data = await db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(data.EventTime, now);
        }

        [Fact]
        public async Task UpdateCanDeleteComment()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test", 
                Comment = "I love lgl"
            }, User.UserId);

            // Action
            created.Comment = " ";
            await accountingManager.Update(created);

            // Assert
            var data = await db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Null(data.Comment);
        }

        [Fact]
        public async Task UpdateCanDeleteTitle()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test",
                Comment = "I love lgl"
            }, User.UserId);

            // Action
            created.Title = "life";
            await accountingManager.Update(created);

            // Assert
            var data = await db.AccountingTitle
                .FirstOrDefaultAsync(x => x.Title == "test");
            Assert.Null(data);
        }

        [Fact]
        public async Task UpdateWontDeleteTitleWhenRef2()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test",
                Comment = "I love lgl"
            }, User.UserId);
            var create2 = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test",
                Comment = "I love lgl"
            }, User.UserId);

            // Action
            created.Title = "life";
            await accountingManager.Update(created);

            // Assert
            var data = await db.AccountingTitle
                .FirstOrDefaultAsync(x => x.Title == "test");
            Assert.NotNull(data);
        }

        [Fact]
        public async Task UpdateCanUpdateComment()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test",
                Comment = "I love lgl"
            }, User.UserId);

            // Action
            created.Comment = "Nice";
            await accountingManager.Update(created);

            // Assert
            var data = await db.AccountingComment.ToListAsync();
            Assert.Equal("Nice", data[0].Comment);
        }
    }
}
