using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShawInterviewExercise.API.Controllers;
using ShawInterviewExercise.DAL;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace ShawInterviewExercise.API.Tests.Controllers
{
    [TestClass]
    public class ShowServiceTest
    {
        [TestMethod]
        public void ShowService_Init()
        {
            ShowService service = new ShowService();

            Assert.IsNotNull(service) ;
            Assert.AreNotEqual(0, service.ShowCount());
        }

        [TestMethod]
        public void ShowService_ShowListReset()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Console.Out.Write("Old count: " + service.ShowCount() + "\n");
            Console.Out.Write("Old show list: " + jss.Serialize(service.GetAllShows()) + "\n");

            service.ShowListReset();

            Console.Out.Write("New count: " + service.ShowCount() + "\n");
            Console.Out.Write("New show list: " + jss.Serialize(service.GetAllShows()) + "\n");
        }

        [TestMethod]
        public void ShowService_GetAllShows()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            
            List<Show> show_list = service.GetAllShows();

            Console.Out.Write("Show list: " + jss.Serialize(show_list) + "\n");

            Assert.IsNotNull(show_list);
        }

        [TestMethod]
        public void ShowService_GetByName()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            service.ShowListReset();
            Show show = service.GetByName("rookieblue");

            Console.Out.Write("Show: " + jss.Serialize(show) + "\n");

            Assert.IsNotNull(show);
        }

        [TestMethod]
        public void ShowService_AddShow()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Show pilot = new Show() { Id = 0, Name = "pilot", NameReadable = "Pilot", Description = "Might be good!" };
            Console.Out.Write("Show count before: " + jss.Serialize(service.ShowCount()) + "\n");

            service.AddShow(pilot);
            
            Console.Out.Write("Show count after: " + jss.Serialize(service.ShowCount()) + "\n");

            //Assert.IsNotNull(show_list);
        }

        [TestMethod]
        public void ShowService_UpdateShow()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            service.ShowListReset();
            Show show = service.GetByName("rookieblue");
            Console.Out.Write("Old show: " + jss.Serialize(show) + "\n");
            
            string oldName = show.Name;
            string oldNameReadable = show.NameReadable;
            
            show.Name = "newname";
            show.NameReadable = "New name";
            string resp = service.UpdateShow(show);
            
            Console.Out.Write("Response: " + resp + "\n");
            Console.Out.Write("New show: " + jss.Serialize(show) + "\n");

            Assert.AreNotEqual(oldName, show.Name);
            Assert.AreNotEqual(oldNameReadable, show.NameReadable);
        }

        [TestMethod]
        public void ShowService_DeleteShow()
        {
            ShowService service = new ShowService();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            
            Console.Out.Write("Show count before: " + jss.Serialize(service.ShowCount()) + "\n");

            service.ShowListReset();
            Show show = service.GetByName("rookieblue");
            Show oldShow = service.DeleteShow(show);

            Console.Out.Write("Deleted show: " + jss.Serialize(oldShow) + "\n");
            Console.Out.Write("Show count after: " + jss.Serialize(service.ShowCount()) + "\n");
            show = service.GetByName("rookieblue");

            Assert.IsNull(show);
        }
    }

    [TestClass]
    public class ShowControllerTest
    {
        [TestMethod]
        public void ShowController_GetAll()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            HttpResponseMessage resp = client.GetAsync("api/show").Result;
            Console.Out.Write("Response:" + jss.Serialize(resp) + "\n");
            
            Assert.IsTrue(resp.IsSuccessStatusCode);

            if (resp.IsSuccessStatusCode)
            {
                var shows = resp.Content.ReadAsAsync<IEnumerable<Show>>().Result;
                Console.Out.Write("Show list: " + jss.Serialize(shows) + "\n");
            }
        }

        [TestMethod]
        public void ShowController_GetById()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            HttpResponseMessage resp = client.GetAsync("api/show/1").Result;

            Assert.IsTrue(resp.IsSuccessStatusCode);
            if (resp.IsSuccessStatusCode)
            {
                var show = resp.Content.ReadAsAsync<Show>().Result;
                Console.Out.Write(jss.Serialize(show));
            }
            else { Console.Out.Write("Response: " + jss.Serialize(resp.Content) + "\n");  }
        }

        [TestMethod]
        public void ShowController_GetByName()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            HttpResponseMessage resp = client.GetAsync("api/show/rookieblue").Result;

            Assert.IsTrue(resp.IsSuccessStatusCode);
            if (resp.IsSuccessStatusCode)
            {
                var show = resp.Content.ReadAsAsync<Show>().Result;
                Console.Out.Write(jss.Serialize(show));
            }
        }

        [TestMethod]
        public void ShowController_Post()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            Show pilot = new Show() { Id = 4, Name = "pilot", NameReadable = "Pilot", Description = "Might be good!" };
            HttpResponseMessage postresp = client.PostAsJsonAsync("api/show", pilot).Result;

            Console.Out.Write("Pilot: " + jss.Serialize(pilot) + "\n");

            Assert.IsTrue(postresp.IsSuccessStatusCode);
            if (postresp.IsSuccessStatusCode)
            {
                Console.Out.Write("Post Response: " + postresp.Content.ReadAsAsync<string>().Result + "\n");

                HttpResponseMessage getresp = client.GetAsync("api/show/").Result;
                Assert.IsTrue(getresp.IsSuccessStatusCode);
                if (getresp.IsSuccessStatusCode)
                {
                    var shows = getresp.Content.ReadAsAsync<IEnumerable<Show>>().Result;
                    Console.Out.Write("New Show List:" + jss.Serialize(shows) + "\n");
                }
            }
        }

        [TestMethod]
        public void ShowController_Put()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            HttpResponseMessage getresp = client.GetAsync("api/show/rookieblue").Result;
            Console.Out.Write("Response: " + jss.Serialize(getresp) + "\n");

            Assert.IsTrue(getresp.IsSuccessStatusCode);
            if (getresp.IsSuccessStatusCode)
            {
                Show pilot = (Show)getresp.Content.ReadAsAsync<Show>().Result;
                Console.Out.Write("Pilot before: " + jss.Serialize(pilot) + "\n");

                pilot.Name = "newname";
                pilot.NameReadable = "New name";
                Console.Out.Write("Pilot after: " + jss.Serialize(pilot) + "\n");

                HttpResponseMessage putresp = client.PutAsJsonAsync("api/show/" + pilot.Id, pilot).Result;

                
                Assert.IsTrue(putresp.IsSuccessStatusCode);
                if (putresp.IsSuccessStatusCode)
                {
                    Console.Out.Write("Put Response: " + putresp.Content.ReadAsAsync<string>().Result + "\n");
                }
            }
        }

        [TestMethod]
        public void ShowController_Delete()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2051");

            HttpResponseMessage delresp = client.DeleteAsync("api/show/rookieblue").Result;

            Assert.IsTrue(delresp.IsSuccessStatusCode);
            if (delresp.IsSuccessStatusCode)
            {
                var shows = delresp.Content.ReadAsAsync<Show>().Result;
                Console.Out.Write("Delete Response:" + jss.Serialize(shows) + "\n");
            }
        }
    
    }
}
