using Nancy;
using Nancy.ModelBinding;

namespace SimpleBoard.Service
{
    public class TaskModule : NancyModule
    {
        public TaskModule(ITaskRepository taskRepository) : base("/tasks")
        {
            Get["/"] = _ => { return Response.AsJson(taskRepository.Find()); };

            Get["/status/{Status}"] = param =>
                                          {
                                              string status = param.Status;
                                              return Response.AsJson(taskRepository.FindByStatus(status));
                                          };

            Post["/"] = task =>
                            {
                                var newTask = this.Bind<Task>();
                                taskRepository.Save(newTask);
                                return HttpStatusCode.Created;
                            };

            Put["/{Id}"] = task =>
                               {
                                   var updatedTask = this.Bind<Task>();
                                   taskRepository.Update(updatedTask);
                                   return HttpStatusCode.OK;
                               };


        }
    }
}