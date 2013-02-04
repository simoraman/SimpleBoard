using Nancy.Bootstrappers.Ninject;
using Ninject;
using Raven.Client;
using Raven.Client.Embedded;

namespace SimpleBoard.Service
{
    public class Bootrapper : NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            existingContainer.Bind<IDocumentStore>()
                .ToMethod(context =>
                              {
                                  var documentStore = new EmbeddableDocumentStore {DataDirectory = "App_Data"};
                                  return documentStore.Initialize();
                              })
                .InSingletonScope();

            existingContainer.Bind<IDocumentSession>().ToMethod(context => context.Kernel.Get<IDocumentStore>().OpenSession());
            existingContainer.Bind<ITaskRepository>().To<TaskRepository>();

            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}