using Microsoft.AspNetCore.Mvc;
using sdlife.web.Dtos;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Controllers
{
    public class DiaryController : SdlifeBaseController
    {
        public Task<PagedList<DiaryDto>> List([FromBody]PagedListQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
