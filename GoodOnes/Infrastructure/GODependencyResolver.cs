using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;
using Ninject.Web.Common;
using GoodOnes.Entities;
using GoodOnes.DAL;
using GoodOnes.DAL.Abstract;
using GoodOnes.DAL.Concrete;
using GoodOnes.Services;

namespace GoodOnes.Infrastructure
{
    public class GODependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public GODependencyResolver()
        {
            kernel = new StandardKernel();
            DoBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void DoBindings()
        {
            kernel.Bind<GOContext>().ToSelf().InRequestScope();

            kernel.Bind<GuestsService>().ToSelf().InRequestScope();
            kernel.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();

            kernel.Bind<EventsService>().ToSelf().InRequestScope();
            kernel.Bind<IEventRepository>().To<EventRepository>().InRequestScope();

            kernel.Bind<QuestionsService>().ToSelf().InRequestScope();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>().InRequestScope();
        }
    }
}