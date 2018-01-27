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
    public class AssetTypeSettingTypeControllerTestsBase : ControllerTestsBase
    {
        public AssetTypeSettingTypeControllerTestsBase()
        {
            _controller = new AssetTypeSettingTypeController(_unitOfWork);
        }

        protected AssetTypeSettingTypeController _controller;
    }


    [TestClass()]
    public class AssetTypeSettingTypeControllerTests : AssetTypeSettingTypeControllerTestsBase
    {
        [TestMethod()]
        public void IndexLinkedSettingTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.IndexLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedSettingTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedSettingTypesViewModel>), "View Model");
        }

        [TestMethod()]
        public void IndexLinkedSettingTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, // count
                new AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedSettingTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void IndexLinkedSettingTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, 
                new AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = true }}; 
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = false }}; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 1;

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
            var controller = _controller;
            int settingTypeId = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedAssetTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedAssetTypesViewModel>), "View Model");
        }

        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedSettingTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true }, // count
                new AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } }; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true },
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedAssetTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedSettingTypeIdIsValid_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true },
                new AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = true }}; 
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = false }}; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId =20;
            int expectedCount = 1;

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
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedSettingTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedSettingTypesViewModel), "View Model");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            controller.TempData["SuccessMessage"] = "Test Success Message";
            int assetTypeId = 1;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedSettingTypesViewModel;
            Assert.IsNotNull(controller.ViewData["SuccessMessage"], "Message");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAssetTypeValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(assetTypeId, vmResult.AssetTypeId, "Id");
            Assert.AreEqual("Name", vmResult.AssetTypeName, "Name");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = false } }; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20; 
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()            {
                AssetTypeId = assetTypeId,
                CreateViewModels = new List<CreateViewModel>()                {
                    new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 30, IsActive = true },
                    new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 31, IsActive = false } }
            };
            int expectedCount = 2;

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _dataAssetTypesSettingTypes.Where(r => r.AssetTypeId == assetTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = new List<CreateViewModel>()                {
                    new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 30, IsActive = true } }
            };

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(assetTypeId, routeResult.RouteValues["id"], "Route Id");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true }}; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 1;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = new List<CreateViewModel>()                {
                    new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 30, IsActive = true } }
            };

            // Act
            controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual("Linked setting types created.", controller.TempData["SuccessMessage"], "Success Message");
        }



        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedAssetTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedAssetTypesViewModel), "View Data");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            controller.TempData["SuccessMessage"] = "Test Success Message";
            int settingTypeId = 1;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedAssetTypesViewModel;
            Assert.IsNotNull(controller.ViewData["SuccessMessage"], "Message");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnSettingTypeValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual(settingTypeId, vmResult.SettingTypeId, "Id");
            Assert.AreEqual("SettingType Name", vmResult.SettingTypeName, "Name");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange            
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = false } }; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "ViewModel Count");

        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, 
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = true } };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;
            var vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = new List<CreateViewModel>() {
                    new CreateViewModel() { AssetTypeId = 30, SettingTypeId = settingTypeId, IsActive = true }, 
                    new CreateViewModel() { AssetTypeId = 31, SettingTypeId = settingTypeId, IsActive = false } } 
            };
            int expectedCount = 2;

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataAssetTypesSettingTypes.Where(r => r.SettingTypeId == settingTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true } };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;
            var vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = new List<CreateViewModel>() {
                    new CreateViewModel() { AssetTypeId = 30, SettingTypeId = settingTypeId, IsActive = true } }
            };

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(settingTypeId, routeResult.RouteValues["id"], "Route Id");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true } };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 20;
            var vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = new List<CreateViewModel>() {
                    new CreateViewModel() { AssetTypeId = 30, SettingTypeId = settingTypeId, IsActive = true } }
            };

            // Act
            controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual("Linked asset types created", controller.TempData["SuccessMessage"], "Message");
        }



        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("EditLinkedSettingTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditLinkedSettingTypesViewModel), "View Model");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnActiveAssetTypeValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true } }; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(assetTypeId, vmResult.AssetTypeId, "Id");
            Assert.AreEqual("AssetType Name", vmResult.AssetTypeName, "Name");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAllLinkedSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, // count
                new AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = false } }; // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnNewSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true } }; 
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "Old SettingType Name", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "New SettingType Name", IsActive = true } }; // NEW count
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true });
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 4, SettingTypeId = 6, IsActive = true });
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 4, SettingTypeId = 7, IsActive = true });
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name", IsActive = true }); // count
            _dataSettingTypes.Add(new SettingType() { Id = 6, Name = "SettingType Name", IsActive = false });
            _dataSettingTypes.Add(new SettingType() { Id = 7, Name = "SettingType Name", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 4;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "EditViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 2, SettingTypeId = 3, IsActive = true }); 
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id =2, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 3, Name = "SettingType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 2;
            List<EditViewModel> vmEditList = new List<EditViewModel>();
            vmEditList.Add(new EditViewModel() { Id = 1, AssetTypeId = assetTypeId, SettingTypeId = 3, IsActive = false }); // changed

            EditLinkedSettingTypesViewModel vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                EditViewModels = vmEditList
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTypesSettingTypes.FirstOrDefault(r => r.Id == 1);
            Assert.AreEqual(false, dtoResult.IsActive, "Record Updated");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int expectedAssetTypeId = 1; // 
            EditLinkedSettingTypesViewModel vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = expectedAssetTypeId,
                EditViewModels = new List<EditViewModel>()
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(expectedAssetTypeId, routeResult.RouteValues["id"], "AssetType Id");
        }



        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = 1;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("EditLinkedAssetTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditLinkedAssetTypesViewModel));
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnSettingTypeValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true });
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 4;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(settingTypeId, vmResult.SettingTypeId, "SettingType Id");
            Assert.AreEqual("SettingType Name", vmResult.SettingTypeName, "SettingType Name");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnLinkedAssetTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true }); // count
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = false }); // count
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true }); // count
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 4;
            int expectedCount = 3;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "EditViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true });
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = true });
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true });
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true }); // count
            _dataAssetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name", IsActive = false });
            _dataAssetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 4;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "EditViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>();
            _dataAssetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 3, SettingTypeId = 2, IsActive = true });
            IList<SettingType> _dataSettingTypes = new List<SettingType>();
            _dataSettingTypes.Add(new SettingType() { Id = 2, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 3, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 2;
            List<EditViewModel> vmEditList = new List<EditViewModel>();
            vmEditList.Add(new EditViewModel() { Id = 1, AssetTypeId = 3, SettingTypeId = settingTypeId, IsActive = false }); // changed

            EditLinkedAssetTypesViewModel vmExpected = new EditLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                EditViewModels = vmEditList
            };

            // Act
            var result = controller.EditLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTypesSettingTypes.FirstOrDefault(r => r.Id == 1);
            Assert.AreEqual(false, dtoResult.IsActive, "Record Updated");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int expectedSettingTypeId = 1; // 
            EditLinkedAssetTypesViewModel vmExpected = new EditLinkedAssetTypesViewModel()
            {
                SettingTypeId = expectedSettingTypeId,
                EditViewModels = new List<EditViewModel>()
            };

            // Act
            var result = controller.EditLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(expectedSettingTypeId, routeResult.RouteValues["id"], "SettingType Id");
        }

    }
}