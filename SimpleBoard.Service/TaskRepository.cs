using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Embedded;
using Raven.Client;
using Raven.Abstractions.Commands;

namespace SimpleBoard.Service
{
    public interface ITaskRepository
    {
        IEnumerable<Task> Find();
        Task Save(Task task);
        IEnumerable<Task> FindByStatus(string status);
        void Update(Task updatedTask);
        void Delete(int taskId);
    }

    public class TaskRepository : ITaskRepository
    {
        private IDocumentSession session;

        public TaskRepository(IDocumentSession session) 
        {
            this.session = session;
            
        }

        public IEnumerable<Task> Find()
        {
            return session.Query<Task>().ToList();
        }

        public Task Save(Task task)
        {
            session.Store(task);
            session.SaveChanges();
            return task;
        }

        public IEnumerable<Task> FindByStatus(string status)
        {
            var results = from task in session.Query<Task>()
                          where task.Status == status
                          select task;
            return results.ToList();
        }

        public void Update(Task updatedTask)
        {
            //var result = from task in session.Query<Task>()
            //             where task.Id == updatedTask.Id
            //             select task;
            session.Store(updatedTask);
            session.SaveChanges();
        }

        public void Delete(int taskId)
        {
            session.Advanced.Defer(new DeleteCommandData { Key = "tasks/" + taskId });
            session.SaveChanges();
        }
    }
}