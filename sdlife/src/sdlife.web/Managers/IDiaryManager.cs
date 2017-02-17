using sdlife.web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Dtos.Diary;
using sdlife.web.Models;

namespace sdlife.web.Managers
{
    public interface IDiaryManager
    {
        Task<DiaryDto> Create(DiaryDto dto);
        Task<PagedList<DiaryDto>> PagedList(DiaryQuery query);
    }
}
