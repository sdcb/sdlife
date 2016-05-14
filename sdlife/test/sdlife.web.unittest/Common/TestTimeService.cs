using sdlife.web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.unittest.Common
{
    public class TestTimeService : ITimeService
    {
        public static readonly DateTime StaticNow = new DateTime(2016, 5, 14, 10, 30, 27);

        public DateTime Now
        {
            get
            {
                return StaticNow;
            }
        }
    }
}
