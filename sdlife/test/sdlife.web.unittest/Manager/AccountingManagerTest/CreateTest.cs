using Microsoft.EntityFrameworkCore;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
using sdlife.web.Services.Implements;
using sdlife.web.unittest.Common;
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
            // Action
            var expectedEventTime = DateTime.Parse(timeString);
            var created = await _accounting.Create(new ViewModels.AccountingDto
            {
                Amount = (decimal)money, 
                Time = expectedEventTime,
                Title = title
            });

            // Action
            var accounting = await _db.Accounting
                .Include(x => x.Title)
                .SingleAsync(x => x.Id == created.Id);

            // Assert
            Assert.Equal(TestTimeService.StaticNow, accounting.CreateTime);
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
            var created = await _accounting.Create(new ViewModels.AccountingDto
            {
                Amount = 1,
                Time = _time.Now,
                Title = "早餐", 
                Comment = comment
            });

            // Action
            var accounting = await _db.Accounting
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
        [InlineData("阿里云", "ALY")]
        [InlineData("打车", "DC")]
        public async Task CreateWithPinYinTest(string title, string expectedShortCut)
        {
            // Arrange
            var created = await _accounting.Create(new ViewModels.AccountingDto
            {
                Amount = 1,
                Time = _time.Now,
                Title = title
            });

            // Action
            var accounting = await _db.Accounting
                .Include(x => x.Title)
                .SingleAsync(x => x.Id == created.Id);

            // Assert
            Assert.Equal(expectedShortCut, accounting.Title.ShortCut);
        }
    }
}
