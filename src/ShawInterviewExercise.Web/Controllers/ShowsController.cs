using ShawInterviewExercise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShawInterviewExercise.Web.Controllers
{
    /* Control what the user can see about the shows. */
    public class ShowsController : Controller
    {
        public ActionResult Index()
        {
            // Get show list via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show").Result;

            // Send show list
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<IEnumerable<Show>>().Result); 
            }

            // On failure, assume there is no list, return home
            return RedirectToAction("Index", "Home", new { empty_list = true });
        }

        public ActionResult Details(string showName)
        {
            // Validate call
            if (showName == "") return RedirectToAction("Index");

            // Get show by name via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show/" + showName).Result;

            // Send show
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<Show>().Result);
            }

            // On failure, return to index
            return RedirectToAction("Index", new { showName = showName, found_show = false });
        }

        public ActionResult Video(string showName)
        {
            // Validate call
            if (showName == "") return RedirectToAction("Index");

            // Get show by name via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show/" + showName).Result;

            // Send show to view
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<Show>().Result);
            }

            // On failure, return to index
            return RedirectToAction("Index", new { showName = showName, found_show = false });
        }
    }
}
