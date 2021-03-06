﻿using sdlife.web.Data;
using sdlife.web.Dtos;
using sdlife.web.Dtos.Diary;
using sdlife.web.Models;
using sdlife.web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Managers.Implements
{
    public class DiaryManager : IDiaryManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ICurrentUser _user;

        public DiaryManager(
            ApplicationDbContext db,
            ICurrentUser user)
        {
            _db = db;
            _user = user;
        }

        public async Task<DiaryDto> Create(DiaryDto dto)
        {
            var result = new DiaryHeader
            {
                Content = new DiaryContent { Content = dto.Content },
                UserId = _user.UserId,
                Weather = new Weather { Name = dto.Weather },
                Feelings = dto.Feelings.Select(x => new DiaryFeeling
                    {
                        Feeling = new Feeling
                        {
                            Name = x
                        }
                    }).ToList()
            };

            _db.DiaryHeader.Add(result);
            await _db.SaveChangesAsync();

            return (DiaryDto)result;
        }

        public async Task<PagedList<DiaryDto>> PagedList(DiaryQuery query)
        {
            return await _db.DiaryHeader
                .Where(x => x.UserId == query.UserId)
                .Select(x => new DiaryDto
                {
                    Content = x.Content.Content, 
                    Feelings = x.Feelings.Select(v => v.Feeling.Name), 
                    RecordTime = x.RecordTime, 
                    Weather = x.Weather.Name
                })
                .CreatePagedList(query);
        }
    }
}
