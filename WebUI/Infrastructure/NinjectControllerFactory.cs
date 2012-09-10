using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // put additional bindings here
            ninjectKernel.Bind<IClientRepository>().To<EFClientRepository>();
            ninjectKernel.Bind<ICompanyRepository>().To<EFCompanyRepository>();
            ninjectKernel.Bind<IRepository<ContactField>>().To<EFContactFieldRepository>();
            ninjectKernel.Bind<IRepository<Employee>>().To<EFEmployeeRepository>();
            ninjectKernel.Bind<IEventRepository>().To<EFEventRepository>();
            ninjectKernel.Bind<IRepository<ContactFieldType>>().To<EFContactFieldTypeRepository>();
            ninjectKernel.Bind<IRepository<ContactStage>>().To<EFContactStageRepository>();
            ninjectKernel.Bind<IEventStatusRepository>().To<EFEventStatusRepository>();
            ninjectKernel.Bind<IRepository<EventCategory>>().To<EFEventCategoryRepository>();

            //// create the email settings object
            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile
            //        = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};

            //ninjectKernel.Bind<IOrderProcessor>()
            //    .To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            //ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            
            //Mock implementation
            //Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            //mock.Setup(m => m.Persons).Returns(new List<Person>{
            //    new Person { First_name="Jan", Last_name="Kowalski", Birth_date=DateTime.Parse("1975/04/02")},
            //       new Person { First_name="Bro", Last_name="Smith", Birth_date=DateTime.Parse("1963/01/05")}
            //}.AsQueryable());

            //ninjectKernel.Bind<IPersonRepository>().ToConstant(mock.Object);
        }
    }
}