using Microsoft.AspNetCore.Mvc;
using sdlife.web.Dtos;
using sdlife.web.Dtos.Diary;
using sdlife.web.Managers;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Controllers
{
    public class DiaryController : SdlifeBaseController
    {
        private readonly IDiaryManager _diary;

        public DiaryController(IDiaryManager diary)
        {
            _diary = diary;
        }

        public Task<PagedList<DiaryDto>> List([FromBody]DiaryQuery query)
        {
            return _diary.PagedList(query);
        }
    }
}
