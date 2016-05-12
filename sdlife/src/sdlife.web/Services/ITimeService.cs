using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Services
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}
