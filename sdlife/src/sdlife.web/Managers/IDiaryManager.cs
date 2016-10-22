using sdlife.web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Managers
{
    public interface IDiaryManager
    {
        Task<DiaryDto> Create(DiaryDto dto);
    }
}
