using ShawInterviewExercise.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShawInterviewExercise.API.Controllers
{
    public class ShowController : ApiController
    {
        public ShowController()
        {
            this.ShowService = new ShowService();
        }
        public ShowService ShowService { get; set; }
        public IEnumerable<Show> Get()
        {
            return this.ShowService.GetAllShows();
        }

        // GET api/show/5
        // GET api/show/name
        /* Allow the get call to work with both show names and show Ids 
         * RETURNS: The requested show */
        public Show Get(string id)
        {
            int showId;

            // If id is a number, assume it represents a show ID
            if (int.TryParse(id, out showId)) { 
                return this.ShowService.GetById(showId) ; 
            }

            // Assume id represents a show name
            return this.ShowService.GetByName(id);
        }

        // POST api/show
        /* RETURNS: the new show's ID */
        [HttpPost]
        public int Post([FromBody]Show show)
        {
            return this.ShowService.AddShow(show);
        }

        // PUT api/show/5
        /* [DSB] I've left Id as a parameter even though the 
         * incoming Show will already have the correct Id. */
        /* RETURNS: a success/failure message */
        [HttpPut]
        public string Put(int id, [FromBody]Show show)
        {
            // Validate call
            if (show.Id != id) { return "Error: ID in request does not match Show ID.";  }
            return this.ShowService.UpdateShow(show);
        }

        // DELETE api/show/5
        // DELETE api/show/name
        /* Allow the delete call to work with both show names and show Ids 
        /* RETURNS: the deleted show */
        [HttpDelete]
        public Show Delete(string id)
        {
            int showId;

            // If id is a number, assume it represents a show ID
            if (int.TryParse(id, out showId))
            {
                return this.ShowService.DeleteShow(this.ShowService.GetById(showId)); ;
            }

            // Assume id represents a show name
            return this.ShowService.DeleteShow(this.ShowService.GetByName(id));
        }
    }

    public class ShowService
    {
        private AppDbContext db = new AppDbContext();

        public ShowService()
        {
            if (db.Shows.Count() == 0)
            {
                ShowListReset();
            }
        }

        /* Reset the list of shows in the repository */
        /* [DSB] This helps with testing, however, we'd have to be 
         * very careful not to run tests in a production environment
         * to avoid accidentally destroying production data. */
        public void ShowListReset()
        {
            foreach (Show show in this.GetAllShows())
            {
                db.Shows.Remove(show);
            }
            foreach (Show show in this.PopulateDefaultShows())
            {
                db.Shows.Add(show);
            }
            db.SaveChanges();
        }

        /* [DSB] Useful for testing purposes. */
        public int ShowCount()
        {
            return db.Shows.Count();
        }

        public List<Show> GetAllShows()
        {
            return db.Shows.ToList<Show>();
        }

        public Show GetById(int id)
        {
            return db.Shows.FirstOrDefault(x => x.Id.Equals(id));
        }

        public Show GetByName(string name)
        {
            return db.Shows.FirstOrDefault(x => x.Name.Equals(name));            
        }

        public int AddShow(Show show)
        {
            db.Shows.Add(show);
            db.SaveChanges();
            return GetByName(show.Name).Id;
        }

        /* [DSB] For consistency, we should return a Show, but at the time,
         * I wanted a success message, for testing purposes. Also, while not always
         * the best way of thinking, I felt that the user already has an instance 
         * of the show and doesn't need it returned to them. */
        public string UpdateShow(Show show)
        {
            // Validate call
            if (show == null)
            {
                throw new ArgumentNullException("show");
            }

            // Get the show via ID, then replace the values
            Show oldShow = db.Shows.FirstOrDefault(x => x.Id.Equals(show.Id));
            db.Entry(oldShow).CurrentValues.SetValues(show);
            db.SaveChanges();

            // Return success message
            return "Updated show Id(" + show.Id + ") - " + oldShow.Name + " -> " + show.Name + ".";
        }

        public Show DeleteShow(Show show)
        {
            Show oldShow = db.Shows.Remove(show);
            db.SaveChanges();
            return oldShow;
        }

        public List<Show> PopulateDefaultShows()
        {
            List<Show> shows = new List<Show>();

            shows.Add(
                new Show()
                {
                    Name = "rookieblue",
                    NameReadable = "Rookie Blue",
                    Description = "Could be the best of all time!!!",
                    ImageUrl = "~/Content/images/rookieblue_title_image.png"
                });
            shows.Add(
                new Show()
                {
                    Name = "battlecreek",
                    NameReadable = "Battle Creek",
                    Description = "Battle all the creeks",
                    ImageUrl = "~/Content/images/battlecreek_title_image.png"
                });
            shows.Add(
                new Show()
                {
                    Name = "bobsburgers",
                    NameReadable = "Bobs Burgers",
                    Description = "Tina Belcher is my hero",
                    ImageUrl = "~/Content/images/bobsburgers_title_image.png"
                });
            shows.Add(
                new Show()
                {
                    Name = "bigbrother",
                    NameReadable = "Big Brother",
                    Description = "Who's watching over you?",
                    ImageUrl = "/Content/images/bigbrother_title_image.png"
                });
            shows.Add(
                new Show()
                {
                    Name = "globalnews",
                    NameReadable = "Global News",
                    Description = "And in the news today...",
                    ImageUrl = "/Content/images/globalnews_title_image.png"
                });
            shows.Add(
                new Show()
                {
                    Name = "ncis",
                    NameReadable = "NCIS",
                    Description = "I don't watch this, hence no quip from me.",
                    ImageUrl = "/Content/images/ncis_title_image.png"
                }); 

            return shows;
        }

    }
}
