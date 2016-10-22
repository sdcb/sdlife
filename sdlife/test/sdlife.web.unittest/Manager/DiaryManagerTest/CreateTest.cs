using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Dtos;
using sdlife.web.Managers;
using sdlife.web.unittest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager.DiaryManagerTest
{
    public class CreateTest : TestBase
    {
        [Fact]
        public async Task CanCreate()
        {
            var diary = ServiceProvider.GetRequiredService<IDiaryManager>();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var input = new DiaryDto
            {
                Content = "Hello World",
                Feelings = new[] { "爽", "开心" },
                Weather = "晴",
            };
            db.LogToDebug();
            var result = await diary.Create(input);

            var header = await db.DiaryHeader.SingleAsync();
            var content = await db.DiaryContent.SingleAsync();
            var feelings = await db.DiaryHeader.Include(x => x.Feelings).ToListAsync();
            var weather = await db.Weather.SingleAsync();

            Assert.True(result.Id > 0);
            Assert.True(header.WeatherId == weather.Id);
            Assert.True(feelings.Count == 2);
            Assert.True(weather.Name == input.Weather);
        }
    }
}
