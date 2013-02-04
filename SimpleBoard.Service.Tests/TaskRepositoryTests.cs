using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Raven.Client.Embedded;
using Raven.Client;

namespace SimpleBoard.Service.Tests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        IDocumentSession session;
        [SetUp]
        public void SetUp() 
        {
            EmbeddableDocumentStore documentStore;
            documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };

            documentStore.Initialize();

            session = documentStore.OpenSession();

        }
        [Test]
        public void GetAllTasks()
        {
            
            TaskRepository repo = new TaskRepository(session);
            Task task = new Task { Description = "kuvaus" };
            repo.Save(task);

            IEnumerable<Task> result = repo.Find();

            Assert.That(result.Any(p => p.Description == "kuvaus"));
        }
        [Test]
        public void GetTasksByStatus()
        {

            TaskRepository repo = new TaskRepository(session);
            Task task = new Task { Description = "kuvaus", Status="ToDo" };
            Task task2 = new Task { Description = "kuvaus2", Status = "Doing" };

            repo.Save(task);
            repo.Save(task2);

            IEnumerable<Task> result = repo.FindByStatus("ToDo");

            Assert.That(result.Any(p => p.Status == "ToDo"));
            Assert.False(result.Any(p => p.Status == "Doing"));

        }
    }
}
