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
    public class CreateTest : TestBase
    {
        [Theory]
        [InlineData(3.5, "2016/5/14 10:44", "早餐")]
        public async Task CreateWontReportError(double money, string timeString, string title)
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Action
            var expectedEventTime = DateTime.Parse(timeString);
            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = (decimal)money, 
                Time = expectedEventTime,
                Title = title
            }, User.UserId);

            // Action
            var accounting = await db.Accounting
                .Include(x => x.Title)
                .SingleAsync(x => x.Id == created.Id);

            // Assert
            Assert.Equal(expectedEventTime, accounting.EventTime);
            Assert.Equal(title, accounting.Title.Title);
            Assert.Equal((decimal)money, accounting.Amount);
        }

        [Theory]
        [InlineData(null, false, null)]
        [InlineData(" ", false, null)]
        [InlineData("这是一条注释", true, "这是一条注释")]
        public async Task CreateCommentCheck(string comment, bool hasComment, string expectedComment)
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 1,
                Time = now,
                Title = "早餐", 
                Comment = comment
            }, User.UserId);

            // Action
            var accounting = await db.Accounting
                .Include(x => x.Comment)
                .SingleAsync(x => x.Id == created.Id);

            // Assert
            if (hasComment)
            {
                Assert.NotNull(accounting.Comment);
                Assert.Equal(expectedComment, accounting.Comment.Comment);
            }
            else
            {
                Assert.Null(accounting.Comment);
            }                
        }

        [Theory]
        [InlineData("中餐", "ZC")]
        public async Task CreateWithPinYinTest(string title, string expectedShortCut)
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            var created = await accountingManager.Create(new AccountingDto
            {
                Amount = 1,
                Time = now,
                Title = title
            }, User.UserId);

            // Action
            var accounting = await db.Accounting
                .Include(x => x.Title)
                .SingleAsync(x => x.Id == created.Id);

            // Assert
            Assert.Equal(expectedShortCut, accounting.Title.ShortCut);
        }
    }
}
