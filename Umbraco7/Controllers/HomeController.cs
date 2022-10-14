using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Umbraco7.Controllers
{
    public class HomeController : SurfaceController
    {
        // GET: Home
        [ChildActionOnly]
        public ActionResult _Header()
        {
            var get = Umbraco.AssignedContentItem;
            return View("~/Views/Partials/Header.cshtml", get);
        }
    }
}