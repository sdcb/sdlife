using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Services.Implements
{
    public class TimeService : ITimeService
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
