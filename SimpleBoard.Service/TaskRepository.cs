using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Embedded;
using Raven.Client;

namespace SimpleBoard.Service
{
    public class TaskRepository
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

        public void Save(Task task)
        {
            session.Store(task);
            session.SaveChanges();
        }

        public IEnumerable<Task> FindByStatus(string status)
        {
            var results = from task in session.Query<Task>()
                          where task.Status == status
                          select task;
            return results.ToList();
        }
    }
}