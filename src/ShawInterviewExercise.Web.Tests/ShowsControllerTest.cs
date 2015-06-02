using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShawInterviewExercise.Web.Controllers;
using ShawInterviewExercise.DAL;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace ShawInterviewExercise.Web.Tests
{
    [TestClass]
    public class ShowsControllerTest
    {
        [TestMethod]
        public void ShowsController_Index()
        {
            // Arrange
            ShowsController controller = new ShowsController();
            JavaScriptSerializer jss = new JavaScriptSerializer();

            // Act
            var result = controller.Index();
            ViewResult vResult = (ViewResult)result;
            List<Show> showList = (List<Show>)(vResult).Model;

            // Log
            Console.Out.Write("Result: " + jss.Serialize(result) + "\n");
            Console.Out.Write("Show List: " + jss.Serialize(showList) + "\n");

            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result is not a View Result");
            Assert.IsNotNull(showList, "Model is null.");
            Assert.IsInstanceOfType(showList, typeof(List<Show>), "Get() did not return a properly formatted list of shows");
        }

        [TestMethod]
        public void ShowsController_Details()
        {
            // Arrange
            ShowsController controller = new ShowsController();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string showName = "rookieblue";

            // Act
            var result = controller.Details(showName);
            ViewResult vResult = (ViewResult)result;
            Show show = (Show) vResult.Model;

            // Log
            Console.Out.Write("Result: " + jss.Serialize(result) + "\n");
            Console.Out.Write("Show: " + jss.Serialize(show) + "\n");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result is not a View Result");
            Assert.IsNotNull(show, "Show is null.");
            Assert.IsInstanceOfType(show, typeof(Show), "Get() did not return a properly formatted show");
        }

    }
}