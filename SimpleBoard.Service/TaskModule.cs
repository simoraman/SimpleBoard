using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace SimpleBoard.Service
{
    public class TaskModule : NancyModule
    {
        public TaskModule() : base("/tasks")
        {
            Get["/"] = _ => {
                return Response.AsJson<List<Task>>(new List<Task>{new Task { Id=1, Description="taski" }});
            };
        }
    }
}