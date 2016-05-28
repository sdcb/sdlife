using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Services.Implements;
using sdlife.web.unittest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager.AccountingManagerTest
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            var service = new ServiceCollection();
            service.AddDbContext<ApplicationDbContext>(b => b.UseInMemoryDatabase());
            service.AddTransient<ITimeService, TestTimeService>();
            service.AddTransient<ICurrentUser, TestCurrentUser>();
            service.AddTransient<IAccountingManager, AccountingManager>();
            service.AddTransient<IPinYinConverter, PinYinConverter>();

            ServiceProvider = service.BuildServiceProvider();
            var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.RemoveRange(db.Accounting);
            db.RemoveRange(db.AccountingComment);
            db.RemoveRange(db.AccountingTitle);
            db.RemoveRange(db.AccountingUserAuthorization);
            db.SaveChanges();
        }

        protected ICurrentUser User
        {
            get { return ServiceProvider.GetService<ICurrentUser>(); }
        }
    }
}
