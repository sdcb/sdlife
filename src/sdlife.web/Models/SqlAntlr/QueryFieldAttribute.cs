using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models.SqlAntlr
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryFieldAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
