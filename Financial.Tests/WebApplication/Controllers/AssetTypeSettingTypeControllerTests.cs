using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Tests.Data.Fakes;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data;
using Financial.Core.Models;
using System.Web.Mvc;
using Financial.Core.ViewModels.AssetTypeSettingType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetTypeSettingTypeControllerTestsBase
    {
        public AssetTypeSettingTypeControllerTestsBase()
        {
            _assetTypes = FakeAssetTypes.InitialFakeAssetTypes().ToList();
            _assetTypesSettingTypes = FakeAssetTypesSettingTypes.InitialFakeAssetTypesSettingTypes().ToList();
            _settingTypes = FakeSettingTypes.InitialFakeSettingTypes().ToList();
            _repositoryAssetType = new InMemoryAssetTypeRepository(_assetTypes);
            _repositoryAssetTypeSettingType = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _repositorySettingType = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork = new InMemoryUnitOfWork();
            _unitOfWork.AssetTypes = _repositoryAssetType;
            _unitOfWork.AssetTypesSettingTypes = _repositoryAssetTypeSettingType;
            _unitOfWork.SettingTypes = _repositorySettingType;
            _controller = new AssetTypeSettingTypeController(_unitOfWork);
        }

        protected IList<AssetType> _assetTypes;
        protected IList<AssetTypeSettingType> _assetTypesSettingTypes;
        protected IList<SettingType> _settingTypes;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryAssetTypeSettingTypeRepository _repositoryAssetTypeSettingType;
        protected InMemorySettingTypeRepository _repositorySettingType;
        protected InMemoryUnitOfWork _unitOfWork;
        protected AssetTypeSettingTypeController _controller;
    }


    [TestClass()]
    public class AssetTypeSettingTypeControllerTests : AssetTypeSettingTypeControllerTestsBase
    {
        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnCreateLinkedSettingTypesPartialViewAndViewModel_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            _assetTypes.Add(new AssetType()
            {
                Id = assetTypeId,
                Name = "New Name",
                IsActive = true
            });
            List<SettingType> dbSettingTypes = _settingTypes.ToList();

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedSettingTypes", viewResult.ViewName);
            // Assert - DATABASE VALUES
            // Assert - RETURN VALUES
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedSettingTypesViewModel));
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            var dtoAssetType = _assetTypes.FirstOrDefault(r => r.Id == assetTypeId);
            Assert.AreEqual(dtoAssetType.Id, vmResult.AssetTypeId, "AssetType Id");
            Assert.AreEqual(dtoAssetType.Name, vmResult.AssetTypeName, "AssetType Name");
            Assert.AreEqual(dbSettingTypes.Count(), vmResult.CreateViewModels.Count(), "CreateViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnDetailsActionAndAssetTypeControllerAndAssetTypeId_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = _assetTypesSettingTypes
                .Select(vm => new CreateViewModel()
                {
                    AssetTypeId = assetTypeId,
                    SettingTypeId = vm.SettingTypeId,
                    IsActive = vm.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert - DATABASE VALUES
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes
                .Where(r => r.AssettTypeId == assetTypeId)
                .ToList();
            Assert.AreEqual(vmExpected.CreateViewModels.Count(), dbResult.Count(), "New Records Added Count");
            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            // Assert - RETURN VALUES
            Assert.AreEqual(assetTypeId, routeResult.RouteValues["assetTypeId"], "AssetType Id");
        }
    }
}