
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Web.Abstractions;
using SoftwareManagement.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SoftwareManagement.Web.Controllers.Dashboard
{
    public class DashboardController : BaseController<DashboardController>
    {
     
        public IActionResult Index()
        {
            _notify.Success("Welcome");
            return View();
        }
    }
}
