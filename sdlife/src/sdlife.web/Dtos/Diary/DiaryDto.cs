using Microsoft.EntityFrameworkCore;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos
{
    public class DiaryDto
    {
        public int Id { get; set; }

        public string Weather { get; set; }

        public IEnumerable<string> Feelings { get; set; }

        public string Content { get; set; }

        public DateTime RecordTime { get; set; }

        public static IQueryable<DiaryDto> FromQuery(IQueryable<DiaryHeader> data)
        {
            return data
                .Select(x => new DiaryDto
                {
                    Id = x.Id, 
                    Feelings = x.Feelings.Select(v => v.Feeling.Name), 
                    Content = x.Content.Content, 
                    RecordTime = x.RecordTime, 
                    Weather = x.Weather.Name
                });
        }
    }
}
