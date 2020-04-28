using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KofCWebSite.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KofCWebSite.UI.Controllers
{
    public class JoinController : Controller
    {
        public IActionResult WhyJoin()
        {
            var model = new BaseViewModel()
            {
                Title = "Why Join?"
            };
            return View(model);
        }

        public IActionResult HowToBecomeAKnight()
        {
            var model = new BaseViewModel()
            {
                Title = "How to Become a Knight"
            };
            return View(model);
        }
    }
}
