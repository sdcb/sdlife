using Microsoft.Data.Entity;
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
    public abstract class TestBase 
    {
        protected AccountingManager _accounting;
        protected ApplicationDbContext _db;
        protected TestTimeService _time;

        public TestBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase();

            _db = new ApplicationDbContext(optionsBuilder.Options);
            var currentUser = new TestCurrentUser();
            _time = new TestTimeService();
            var pinYin = new PinYinConverter();

            _accounting = new AccountingManager(_db, currentUser, _time, pinYin);

            _db.Accounting.RemoveRange(_db.Accounting.ToList());
            _db.AccountingTitle.RemoveRange(_db.AccountingTitle.ToList());
            _db.SaveChanges();
        }
    }
}
