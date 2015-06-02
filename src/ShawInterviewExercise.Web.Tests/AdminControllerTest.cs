using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShawInterviewExercise.Web.Controllers;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using ShawInterviewExercise.DAL;

namespace ShawInterviewExercise.Web.Tests
{
    /// <summary>
    /// Summary description for AdminControllerTest
    /// </summary>
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void AdminController_CreatePage()
        {
            // Arrange
            AdminController controller = new AdminController();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            var result = controller.Create();
            ViewResult vResult = (ViewResult)result;

            Console.Out.Write("Result: " + jss.Serialize(result) + "\n");
            Console.Out.Write("Show Instance: " + jss.Serialize(vResult.Model) + "\n");

            Assert.IsNotNull(vResult.Model);
            Assert.IsInstanceOfType(vResult.Model, typeof(Show));
        }

        [TestMethod]
        public void AdminController_CreateCommit()
        {
            // Arrange
            AdminController controller = new AdminController();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Show newShow = new Show() { Id = 12, Name = "pilot", NameReadable = "Pilot TV Show" };

            var result = controller.Create(newShow);
            Console.Out.Write("Result: " + jss.Serialize(result) + "\n");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            RedirectToRouteResult redirResult = (RedirectToRouteResult)result; 
            Console.Out.Write("Route Values: " + jss.Serialize(redirResult.RouteValues) + "\n");

            var action = new Object();
            if (redirResult.RouteValues.TryGetValue("action", out action))
            {
                Assert.AreEqual("Index", action.ToString());
            }
        }

        [TestMethod]
        public void AdminController_EditCommit()
        {
            // Arrange
            AdminController controller = new AdminController();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Show pilot = new Show() { Id = 1, Name = "pilot", NameReadable = "Pilot show" };

            var result = controller.Edit(1,pilot);
            RedirectToRouteResult redirResult = (RedirectToRouteResult)result;

            Console.Out.Write("Route Values: " + jss.Serialize(redirResult.RouteValues) + "\n");

            var action = new Object();
            if (redirResult.RouteValues.TryGetValue("action", out action))
            {
                Assert.AreEqual("Index", action.ToString());
            }
        }

        [TestMethod]
        public void AdminController_DeleteCommit()
        {
            // Arrange
            AdminController controller = new AdminController();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            var result = controller.Delete(1);
            ViewResult vResult = (ViewResult)result;
            Show show = (Show)(vResult).Model;

            Console.Out.Write("Result: " + jss.Serialize(result) + "\n");
            Console.Out.Write("Deleted Show: " + jss.Serialize(show) + "'n");
        }
    }
}
