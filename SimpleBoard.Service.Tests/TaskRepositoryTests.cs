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
        private TaskRepository taskRepository;
        private EmbeddableDocumentStore documentStore;

        [SetUp]
        public void SetUp() 
        {
            documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };

            documentStore.Initialize();

            session = documentStore.OpenSession();
            taskRepository = new TaskRepository(session);

        }
        [Test]
        public void GetAllTasks()
        {
            Task task = new Task { Description = "kuvaus" };
            taskRepository.Save(task);

            IEnumerable<Task> result = taskRepository.Find();

            Assert.That(result.Any(p => p.Description == "kuvaus"));
        }
        [Test]
        public void DeleteTask()
        {
            Task task = new Task { Description = "kuvaus" };
            taskRepository.Save(task);

            IEnumerable<Task> result = taskRepository.Find();

            taskRepository.Delete(result.First().Id);
            result = taskRepository.Find();
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void GetTasksByStatus()
        {
            Task task = new Task { Description = "kuvaus", Status="ToDo" };
            Task task2 = new Task { Description = "kuvaus2", Status = "Doing" };

            taskRepository.Save(task);
            taskRepository.Save(task2);

            IEnumerable<Task> result = taskRepository.FindByStatus("ToDo");

            Assert.That(result.Any(p => p.Status == "ToDo"));
            Assert.False(result.Any(p => p.Status == "Doing"));
        }

        [Test]
        public void GetTaskByStatusNotCaseSensitive()
        {
            Task task = new Task { Description = "kuvaus", Status = "ToDo" };
            Task task2 = new Task { Description = "kuvaus2", Status = "todo" };

            taskRepository.Save(task);
            taskRepository.Save(task2);

            IEnumerable<Task> result = taskRepository.FindByStatus("todo");
            IEnumerable<Task> result2 = taskRepository.FindByStatus("ToDo");

            Assert.That(result, Is.EqualTo(result2));
        }

        [Test]
        public void CanUpdateTasks()
        {
            Task task = new Task { Description = "kuvaus", Status = "ToDo" };
            taskRepository.Save(task);
            session.Dispose();

            session = documentStore.OpenSession();
            taskRepository=new TaskRepository(session);
            var updateTask = taskRepository.Find().First();
            updateTask.Status = "Done";
            taskRepository.Update(updateTask);
            session.Dispose();

            session = documentStore.OpenSession();
            taskRepository = new TaskRepository(session);
            var result = taskRepository.Find().First();

            Assert.That(result.Status,Is.EqualTo("Done"));
        }

        [TearDown]
        public void Teardown()
        {
            session.Dispose();
            documentStore.Dispose();
        }

    }
}
