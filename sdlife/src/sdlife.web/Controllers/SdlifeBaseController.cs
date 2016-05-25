using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sdlife.web.Models;
using sdlife.web.Managers;
using sdlife.web.Dtos;
using sdlife.web.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace sdlife.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SdlifeBaseController : Controller
    {
    }
}
