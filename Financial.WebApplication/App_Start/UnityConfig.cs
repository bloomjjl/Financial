using Financial.Business;
using Financial.Business.ServiceInterfaces;
using Financial.Business.Services;
using Financial.Data;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Financial.WebApplication
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IBusinessService, BusinessService>();

            container.RegisterType<IAssetTypeService, AssetTypeService>();
            container.RegisterType<IAssetTypeSettingTypeService, AssetTypeSettingTypeService>();
            container.RegisterType<IAssetTransactionService, AssetTransactionService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}