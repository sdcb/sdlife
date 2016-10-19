using sdlife.web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Managers.Implements
{
    public class DiaryManager
    {
        private readonly ApplicationDbContext _db;

        public DiaryManager(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
