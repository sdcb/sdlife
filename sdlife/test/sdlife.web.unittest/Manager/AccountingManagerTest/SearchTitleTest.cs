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
    public class SearchTitleTest : TestBase
    {
        [Fact]
        public async Task Basic()
        {
            // Action
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            var titles = new List<string> { "早餐", "零食", "阿里云" };
            var datas = titles.Select(x => new AccountingDto
            {
                Amount = 2, 
                Title = x, 
                Time = now
            });
            foreach (var data in datas)
            {
                await accountingManager.Create(data);
            }

            // Action
            var zc = await accountingManager.SearchTitles("早", 20);
            var ls = await accountingManager.SearchTitles("零", 20);
            var aly = await accountingManager.SearchTitles("阿", 20);
            var no = await accountingManager.SearchTitles("无", 20);

            // Assert
            Assert.Contains("早餐", zc);
            Assert.Contains("零食", ls);
            Assert.Contains("阿里云", aly);
            Assert.DoesNotContain("早餐", no);
        }

        [Fact]
        public async Task FindMultpleBySingleWord()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var titles = new List<string> { "电脑", "电视", "电影" };
            var datas = titles.Select(x => new AccountingDto
            {
                Amount = 2,
                Title = x,
                Time = now
            });
            foreach (var data in datas)
            {
                await accountingManager.Create(data);
            }

            // Action
            var zc = await accountingManager.SearchTitles("电", 20);
            var no = await accountingManager.SearchTitles("早", 20);

            // Assert
            Assert.Equal(3, zc.Count());
            Assert.Contains("电脑", zc);
            Assert.Contains("电视", zc);
            Assert.Contains("电影", zc);
            Assert.True(no.Count() == 0);
        }

        [Fact]
        public async Task SearchByPinYin()
        {
            // Arrange
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;

            // Action
            var titles = new List<string> { "早餐", "中餐", "晚餐" };
            var datas = titles.Select(x => new AccountingDto
            {
                Amount = 2,
                Title = x,
                Time = now
            });
            foreach (var data in datas)
            {
                await accountingManager.Create(data);
            }

            // Action
            var wz = await accountingManager.SearchTitles("Z", 20);
            var ww = await accountingManager.SearchTitles("W", 20);
            var wd = await accountingManager.SearchTitles("D", 20);

            // Assert
            Assert.Equal(2, wz.Count);
            Assert.Equal(1, ww.Count);
            Assert.Equal(0, wd.Count);
            Assert.Contains("早餐", wz);
            Assert.Contains("中餐", wz);
        }

        [Fact]
        public async Task MoreResultWillPutOnTop()
        {
            // Arrange 
            var accountingManager = ServiceProvider.GetRequiredService<IAccountingManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.Now;
            
            var titles = new List<string>
            {
                // 3个中餐（Z）
                // 2个早餐（Z）
                // 1个中央空调（Z）
                // 1个阿里云（A）
                "中餐", "阿里云", "早餐", "早餐", "中餐", "中餐", "中央空调", 
            };
            var datas = titles.Select(x => new AccountingDto
            {
                Amount = 2,
                Title = x,
                Time = now
            });
            foreach (var data in datas)
            {
                await accountingManager.Create(data);
            }

            // Action
            var wc = await accountingManager.SearchTitles("Z", 20);

            // Assert
            Assert.True(wc.Count() == 3);
            Assert.Equal("中餐", wc[0]);
            Assert.Equal("早餐", wc[1]);
            Assert.Equal("中央空调", wc[2]);
        }
    }
}
