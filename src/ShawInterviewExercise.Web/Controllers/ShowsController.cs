using ShawInterviewExercise.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShawInterviewExercise.Web.Controllers
{
    public class ShowsController : Controller
    {
        //
        // GET: /Shows/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string showName)
        {
            //TODO: Change this to look up show from API
            var show = new Show();
            show.Name = showName;
            show.Description = "I think this should be delivered from the API, and a nicer Name for this show along with it.";

            return View(show);
        }

    }
}
