using ShawInterviewExercise.API.Models;
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
        public ShowService ShowService { get; set; }
        public IEnumerable<Show> Get()
        {
            this.ShowService = new ShowService();

            return this.ShowService.GetAllShows();
        }

        // GET api/show/5
        public Show Get(int id)
        {
            this.ShowService = new ShowService();
            return this.ShowService.GetById(id);
        }
    }

    public class ShowService
    {
        public List<Show> Shows { get; set; }

        public ShowService()
        {
            if(this.Shows == null)
            {
                this.Shows = this.PopulateDefaultShows();
            }
        }

        public List<Show> GetAllShows()
        {

            return Shows;
        }

        public Show GetById(int id)
        {
            return this.Shows.FirstOrDefault(x => x.Id.Equals(id));
        }

        private List<Show> PopulateDefaultShows()
        {
            var shows = new List<Show>();

            shows.Add(
                new Show()
                {
                    Id = 1,
                    Name = "Rookie Blue",
                    Description = "Could be the best of all time!!!"
                });

            return shows;
        }

    }
}
