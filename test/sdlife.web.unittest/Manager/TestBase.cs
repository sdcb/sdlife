using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using sdlife.web.Data;
using sdlife.web.Managers;
using sdlife.web.Managers.Implements;
using sdlife.web.Models;
using sdlife.web.Services;
using sdlife.web.Services.Implements;
using sdlife.web.unittest.Common;
using sdlife.web.unittest.Mocked;
using sdlife.web.unittest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace sdlife.web.unittest.Manager
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            var service = new ServiceCollection();
            service.AddDbContext<ApplicationDbContext>(b =>
            {
                b.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
            service.AddTransient<ITimeService, TestTimeService>();
            service.AddTransient<ICurrentUser, TestCurrentUser>();
            service.AddTransient<IAccountingManager, AccountingManager>();
            service.AddTransient<IAccountingPrivilegeManager, AccountingPrivilegeManager>();
            service.AddTransient<IPinYinConverter, PinYinConverter>();
            service.AddTransient<IDiaryManager, DiaryManager>();

            ServiceProvider = service.BuildServiceProvider();
        }

        protected ICurrentUser User
        {
            get { return ServiceProvider.GetService<ICurrentUser>(); }
        }
    }
}
