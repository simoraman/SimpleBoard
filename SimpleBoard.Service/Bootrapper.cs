using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrappers.Ninject;
using Ninject;

namespace SimpleBoard.Service
{
    public class Bootrapper:NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}