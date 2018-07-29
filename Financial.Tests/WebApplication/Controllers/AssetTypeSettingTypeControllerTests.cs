using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.WebApplication.Tests.Fakes.Repositories;
using Financial.Core.Models;
using System.Web.Mvc;
using Financial.WebApplication.Models.ViewModels.AssetTypeSettingType;

namespace Financial.WebApplication.Tests.WebApplication.Controllers
{
    public class AssetTypeSettingTypeControllerTestsBase : ControllerTestsBase
    {
        public AssetTypeSettingTypeControllerTestsBase()
        {
            _controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, // count
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, 
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = true }}; 
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = false }}; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true }, // count
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } }; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true },
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true },
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = true }}; 
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = false }}; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            controller.TempData["SuccessMessage"] = "Test Success Message";
            int assetTypeId = 1;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsNotNull(controller.ViewData["SuccessMessage"], "Message");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAssetTypeValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = false } }; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20; 
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()            {
                AssetTypeId = assetTypeId,
                LinkedAssetTypeSettingTypes = new List<Business.Models.AssetTypeSettingType>()                {
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = assetTypeId, SettingTypeId = 30 },
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = assetTypeId, SettingTypeId = 31 }
                }
            };
            int expectedCount = 2;

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _dataAssetTypeSettingTypes.Where(r => r.AssetTypeId == assetTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        
        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                LinkedAssetTypeSettingTypes = new List<Business.Models.AssetTypeSettingType>(){
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = assetTypeId, SettingTypeId = 30 }
                }
            };

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual("Linked setting types created.", controller.TempData["SuccessMessage"], "Success Message");
        }
        


        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = false } }; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");

        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, 
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = true } };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            var vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                LinkedAssetTypeSettingTypes = new List<Business.Models.AssetTypeSettingType>() {
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = 30, SettingTypeId = settingTypeId }, 
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = 31, SettingTypeId = settingTypeId }
                } 
            };
            int expectedCount = 2;

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataAssetTypeSettingTypes.Where(r => r.SettingTypeId == settingTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>(); // clear links
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true } };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            var vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                LinkedAssetTypeSettingTypes = new List<Business.Models.AssetTypeSettingType>() {
                    new Business.Models.AssetTypeSettingType() { AssetTypeId = 30, SettingTypeId = settingTypeId }
                }
            };

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(settingTypeId, routeResult.RouteValues["id"], "Route Id");
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true } }; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
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
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true }, // count
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = false } }; // count
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType Name 2", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            //int expectedCount = 2;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
           // Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnNewSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true } }; 
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "Old SettingType Name", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "New SettingType Name", IsActive = true } }; // NEW count
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            //int expectedCount = 2;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            //Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true },
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 20, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType Name", IsActive = false } }; // NOT Active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            //int expectedCount = 1;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            //Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 20, SettingTypeId = 30, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int assetTypeId = 20;
            var vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                /*SettingTypes = new List<Business.Models.AssetTypeSettingType>() {
                    new Business.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = assetTypeId, SettingTypeId = 30 } // changed
                } */
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTypeSettingTypes.FirstOrDefault(r => r.Id == 10);
            Assert.AreEqual(false, dtoResult.IsActive, "Record Updated");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int expectedAssetTypeId = 1; 
            var vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetTypeId = expectedAssetTypeId,
                //LinkedAssetTypeSettingTypes = new List<Financial.Business.Models.AssetTypeSettingType>()
            };

            // Act
            var result = controller.EditLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(expectedAssetTypeId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Linked setting types updated.", controller.TempData["SuccessMessage"], "Message");
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
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("EditLinkedAssetTypes", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditLinkedAssetTypesViewModel), "View Model");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnActiveSettingTypeValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true } };
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(settingTypeId, vmResult.SettingTypeId, "Id");
            Assert.AreEqual("SettingType Name", vmResult.SettingTypeName, "Name");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnAllLinkedAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true }, // count
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = false } }; // count
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true },
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnNewAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true } }; 
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "Old AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "New AssetType Name 2", IsActive = true }}; // NEW count
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true },
                new Core.Models.AssetTypeSettingType() { Id = 11, AssetTypeId = 31, SettingTypeId = 20, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name 1", IsActive = true }, // count
                new AssetType() { Id = 31, Name = "AssetType Name 2", IsActive = false }}; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.EditLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedAssetTypeSettingTypes.Count(), "ViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypeSettingTypes = new List<Core.Models.AssetTypeSettingType>() {
                new Core.Models.AssetTypeSettingType() { Id = 10, AssetTypeId = 30, SettingTypeId = 20, IsActive = true } };
            _unitOfWork.AssetTypeSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypeSettingTypes);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 20, Name = "SettingType Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType Name", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeSettingTypeController(_unitOfWork, _assetTypeSettingTypeService);
            int settingTypeId = 20;
            var vmExpected = new EditLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                /*LinkedAssetTypeSettingTypes = new List<Asset>() {
                    new Account() { Id = 10, AssetTypeId = 30, SettingTypeId = settingTypeId, IsActive = false } // changed
                }*/
            };

            // Act
            var result = controller.EditLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTypeSettingTypes.FirstOrDefault(r => r.Id == 10);
            Assert.AreEqual(false, dtoResult.IsActive, "Record Updated");
        }

        [TestMethod()]
        public void EditLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int expectedSettingTypeId = 1; 
            var vmExpected = new EditLinkedAssetTypesViewModel()
            {
                SettingTypeId = expectedSettingTypeId,
                LinkedAssetTypeSettingTypes = new List<Business.Models.AssetTypeSettingType>()
            };

            // Act
            var result = controller.EditLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(expectedSettingTypeId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Linked asset types updated.", controller.TempData["SuccessMessage"], "Message");
        }
        




    }
}