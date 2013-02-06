using System.Collections.Generic;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace SimpleBoard.Service.Tests
{
    [TestFixture]
    public class TaskModuleTests
    {
        private Mock<ITaskRepository> taskRepoMock;
        Browser browser;
        [SetUp]
        public void Setup()
        {
            taskRepoMock = new Mock<ITaskRepository>();
            browser = new Browser((c) => c.Module<TaskModule>().Dependency<ITaskRepository>(taskRepoMock.Object));

        }
        [Test]
        public void GetShouldReturnTasks()
        {
            taskRepoMock.Setup(x => x.Find()).Returns(new List<Task> {new Task {Id = 1, Description = "taski"}});
            
            BrowserResponse response = browser.Get("/tasks");

            JArray jsonArray = JArray.Parse(response.Body.AsString());
            dynamic json = jsonArray[0];
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(json.Description.ToString(), Is.EqualTo("taski"));
        }

        [Test]
        public void CanGetWithStatus()
        {
            var taskList = new List<Task>();
            taskList.Add(new Task {Id = 1, Description = "todo", Status = "ToDo"});
            taskRepoMock.Setup(x => x.FindByStatus("todo")).Returns(taskList);

            var response=browser.Get("/tasks/status/todo");

            JArray jsonArray = JArray.Parse(response.Body.AsString());
            Assert.That(jsonArray.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanPostNewTasks()
        {
            var task = new Task {Description = "todo", Status = "ToDo"};

            var response = browser.Post("/tasks/", with =>
                                                       {
                                                           with.Body(JsonConvert.SerializeObject(task));
                                                           with.Header("content-type", "application/json");
                                                       });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            taskRepoMock.Verify(x=>x.Save(It.Is<Task>(t=>t.Description=="todo")));
        }

        [Test]
        public void CanPutUpdatedTasks()
        {
            var task = new Task { Id = 1, Description = "todo", Status = "Done" };
            var response = browser.Put("/tasks/"+task.Id, with =>
            {
                with.Body(JsonConvert.SerializeObject(task));
                with.Header("content-type", "application/json");
            });
            taskRepoMock.Verify(x => x.Update(It.Is<Task>(t => t.Status == "Done")));
        }
        [Test]
        public void CanDeleteTask()
        {
            var response = browser.Delete("/tasks/" + 1);
            taskRepoMock.Verify(x => x.Delete(1));
        }

    }
}