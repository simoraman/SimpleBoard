using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nancy.Testing;
using Nancy;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace SimpleBoard.Service.Tests
{
    [TestFixture]
    public class TaskModuleTests
    {
        [Test]
        public void Get_Should_Return_Tasks()
        {
            var browser = new Browser((c) => c.Module<TaskModule>());

            BrowserResponse response = browser.Get("/tasks");

            JArray jsonArray = JArray.Parse(response.Body.AsString()) as JArray;
            dynamic json = jsonArray[0];
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(json.Description, Is.EqualTo("kuvaus"));
        }
    }
}
