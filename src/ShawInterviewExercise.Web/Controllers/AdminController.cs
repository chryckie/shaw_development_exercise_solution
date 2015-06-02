using ShawInterviewExercise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShawInterviewExercise.Web.Controllers
{
    /* Control what the Admins can see and do with the show repository */
    public class AdminController : Controller
    {
        /* Display a list of all the shows in repository */
        public ActionResult Index()
        {
            // Get list of shows via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show").Result;

            // Send list of shows to view
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<IEnumerable<Show>>().Result);
            }

            // On failure, assume list is empty, and redirect to Create page
            return RedirectToAction("Create", new { empty_list = true });
        }

        /* Display what the show currently looks like */
        public ActionResult Details(string showName)
        {
            // No immediate need for admin details page.
            // Redirect to the user's view
            return RedirectToAction("Details", "Shows", new {showName = showName});
        }

        /* Display a create form to the admin */
        public ActionResult Create()
        {
            // Send default show values to the view
            return View(new Show() { Name = "Empty.", NameReadable = "Empty Name." });
        }

        /* Add a new show to the repository */
        [HttpPost]
        public ActionResult Create(Show show)
        {
            try
            {
                // Add new show to repository via API
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2051");
                client.PostAsJsonAsync("api/show", show);

                // Return to the index
                return RedirectToAction("Index");
            }
            catch
            {
                // On failure, try view again
                return View(show);
            }
        }

        /* Display an edit form for the requested show */
        public ActionResult Edit(int id)
        {
            // Get show by Id via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show/" + id).Result;

            // Send show to view
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<Show>().Result);
            }

            // On failure, assume show not found, redirect to index
            return RedirectToAction("Index", "Shows", new { showId = id, found_show = false });
        }

        /* Edit the details of the show in the repository */
        [HttpPost]
        public ActionResult Edit(int id, Show show)
        {
            try
            {
                // Get show by Id via API
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2051");
                client.PutAsJsonAsync("api/show/" + id, show);

                // Return to index
                return RedirectToAction("Index");
            }
            catch
            {
                // On failure, try view again
                return View(show);
            }
        }

        /* Confirm that the admin wants to delete the record */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // Get show by Id via API
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");
            HttpResponseMessage resp = client.GetAsync("api/show/" + id).Result;

            // Send show to view
            if (resp.IsSuccessStatusCode)
            {
                return View(resp.Content.ReadAsAsync<Show>().Result);
            }

            // On failure, assume show not found, return to index
            return RedirectToAction("Index", new { showId = id, found_show = false });
        }

        /* Delete the show from the repository */
        [HttpPost]
        public ActionResult Delete(int id, Show show)
        {
            try
            {
                // Delete the show by Id via API
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2051");
                client.DeleteAsync("api/show/" + id);

                // Return to index
                return RedirectToAction("Index");
            }
            catch
            {
                // On failure, try view again
                return View(show);
            }
        }
    }
}
