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
        public void IndexLinkedSettingTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.IndexLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedSettingTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedSettingTypesViewModel>));
        }

        [TestMethod()]
        public void IndexLinkedSettingTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = 1;
            int expectedCount = _assetTypesSettingTypes.Count(r => r.AssetTypeId == assetTypeId);

            // Act
            var result = controller.IndexLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedSettingTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }



        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedAssetTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedAssetTypesViewModel>));
        }

        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedSettingTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = 2;
            int expectedCount = _assetTypesSettingTypes.Count(r => r.SettingTypeId == settingTypeId);

            // Act
            var result = controller.IndexLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedAssetTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }



        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; 
            _assetTypes.Add(new AssetType() { Id = assetTypeId });

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedSettingTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedSettingTypesViewModel));
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int expectedCount = _settingTypes.Count();
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            _assetTypes.Add(new AssetType()
            {
                Id = assetTypeId,
                Name = "New Name",
                IsActive = true
            });

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            var dtoAssetType = _assetTypes.FirstOrDefault(r => r.Id == assetTypeId);
            Assert.AreEqual(dtoAssetType.Id, vmResult.AssetTypeId, "AssetType Id");
            Assert.AreEqual(dtoAssetType.Name, vmResult.AssetTypeName, "AssetType Name");
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "CreateViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_DatabaseUpdated_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = _assetTypesSettingTypes
                .Select(r => new CreateViewModel()
                {
                    AssetTypeId = assetTypeId,
                    SettingTypeId = r.SettingTypeId,
                    IsActive = r.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();
            Assert.AreEqual(vmExpected.CreateViewModels.Count(), dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
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

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(assetTypeId, routeResult.RouteValues["id"], "AssetType Id");
        }



        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1;
            _settingTypes.Add(new SettingType() { Id = settingTypeId });

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedAssetTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedAssetTypesViewModel));
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int expectedCount = _assetTypes.Count();
            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            _settingTypes.Add(new SettingType()
            {
                Id = settingTypeId,
                Name = "New Name",
                IsActive = true
            });

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            var dtoSettingType = _settingTypes.FirstOrDefault(r => r.Id == settingTypeId);
            Assert.AreEqual(dtoSettingType.Id, vmResult.SettingTypeId, "SettingType Id");
            Assert.AreEqual(dtoSettingType.Name, vmResult.SettingTypeName, "SettingType Name");
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "CreateViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_DatabaseUpdated_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            CreateLinkedAssetTypesViewModel vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = _assetTypesSettingTypes
                .Select(r => new CreateViewModel()
                {
                    SettingTypeId = settingTypeId,
                    AssetTypeId = r.AssetTypeId,
                    IsActive = r.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
            Assert.AreEqual(vmExpected.CreateViewModels.Count(), dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            CreateLinkedAssetTypesViewModel vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = _assetTypesSettingTypes
                .Select(vm => new CreateViewModel()
                {
                    SettingTypeId = settingTypeId,
                    AssetTypeId = vm.AssetTypeId,
                    IsActive = vm.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(settingTypeId, routeResult.RouteValues["id"], "SettingType Id");
        }



        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("EditLinkedSettingTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditLinkedSettingTypesViewModel));
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            int assetTypeId = 1;
            _assetTypes.Add(new AssetType()
            {
                Id = assetTypeId,
                Name = "New Name",
                IsActive = true
            });
            AssetTypeSettingTypeController controller = _controller;
            int expectedCount = _settingTypes.Count(r => r.IsActive);

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "EditViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_DatabaseUpdated_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            EditLinkedSettingTypesViewModel vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                EditViewModels = _assetTypesSettingTypes
                .Select(r => new EditViewModel()
                {
                    AssetTypeId = assetTypeId,
                    SettingTypeId = r.SettingTypeId,
                    IsActive = r.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();
            Assert.AreEqual(vmExpected.EditViewModels.Count(), dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            EditLinkedSettingTypesViewModel vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                EditViewModels = _assetTypesSettingTypes
                .Select(vm => new EditViewModel()
                {
                    AssetTypeId = assetTypeId,
                    SettingTypeId = vm.SettingTypeId,
                    IsActive = vm.IsActive
                })
                .ToList()
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(assetTypeId, routeResult.RouteValues["id"], "AssetType Id");
        }


    }
}