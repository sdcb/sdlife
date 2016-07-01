using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class IdName<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }
    }

    public class IdName : IdName<int>
    {
        public static IdName<IdType> Create<IdType>(IdType id, string name)
        {
            return new IdName<IdType>
            {
                Id = id,
                Name = name
            };
        }
    }
}
