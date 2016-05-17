using Microsoft.EntityFrameworkCore;
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
    public class UpdateTest : TestBase
    {
        [Fact]
        public async Task SimpleUpdate()
        {
            // Action
            var created = await _accounting.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test"
            });

            // Action
            var title = "I love lgl";
            await _accounting.Update(new AccountingDto
            {
                Id = created.Id, 
                Amount = 500, 
                Time = _time.Now, 
                Comment = title, 
                Title = title
            });

            // Assert
            var data = await _db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(500, data.Amount);
            Assert.Equal(_time.Now, data.EventTime);
            Assert.Equal(title, data.Title.Title);
            Assert.NotNull(data.Comment);
            Assert.Equal(title, data.Comment.Comment);
        }

        [Fact]
        public async Task UpdateWillEffectShortCut()
        {
            // Action
            var created = await _accounting.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "早餐"
            });

            // Action
            var title = "晚餐";
            await _accounting.Update(new AccountingDto
            {
                Id = created.Id,
                Amount = 500,
                Time = _time.Now,
                Comment = title,
                Title = title
            });

            // Assert
            var data = await _db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(data.Title.ShortCut, "WC");
        }

        [Fact]
        public async Task UpdateTime()
        {
            // Action
            var created = await _accounting.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "早餐"
            });

            // Action
            await _accounting.UpdateTime(created.Id, _time.Now);

            // Assert
            var data = await _db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Equal(data.EventTime, _time.Now);
        }

        [Fact]
        public async Task UpdateCanDeleteComment()
        {
            // Action
            var created = await _accounting.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test", 
                Comment = "I love lgl"
            });

            // Action
            created.Comment = " ";
            await _accounting.Update(created);

            // Assert
            var data = await _db.Accounting
                .SingleAsync(x => x.Id == created.Id);
            Assert.Null(data.Comment);
        }

        [Fact]
        public async Task UpdateCanUpdateComment()
        {
            // Action
            var created = await _accounting.Create(new AccountingDto
            {
                Amount = 2,
                Time = DateTime.Now,
                Title = "test",
                Comment = "I love lgl"
            });

            // Action
            created.Comment = "Nice";
            await _accounting.Update(created);

            // Assert
            var data = await _db.AccountingComment.ToListAsync();
            Assert.Equal("Nice", data[0].Comment);
        }
    }
}
