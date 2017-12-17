using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using Financial.Tests.Data;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data.Fakes;
using System.Web.Mvc;
using Financial.Core.ViewModels.AssetType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetTypeControllerTestsBase
    {
        public AssetTypeControllerTestsBase()
        {
            _assetTypes = FakeAssetTypes.InitialFakeAssetTypes().ToList();
            _repositoryAssetType = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork = new InMemoryUnitOfWork();
            _unitOfWork.AssetTypes = _repositoryAssetType;
            _controller = new AssetTypeController(_unitOfWork);
        }

        protected IList<AssetType> _assetTypes;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryUnitOfWork _unitOfWork;
        protected AssetTypeController _controller;
    }

    public static class AssetTypeObjectMother
    {
    }

    [TestClass()]
    public class AssetTypeControllerTests : AssetTypeControllerTestsBase
    {
        // *** GET - SUCCESS ***
        // Valid Returned Views 
        // Valid Returned View Models
        // Valid Returned Success Messages

        // *** GET - ERROR ***
        // Invalid Returned Error Messages
        // Invalid Input Values
        // Invalid Returned View Models
        // Invalid Retrieved Database Values 
        // Invalid Retrieved Database Objects

        // *** POST - SUCCESS ***
        // Valid Redirected Actions 
        // Valid Output Values
        // Valid Updated Database Values
        // Valid Returned Success Messages

        // *** POST - ERROR ***
        // Invalid Input Values
        // Invalid Database Values 
        // Invalid Database Objects
        // Invalid Output Values
        // Invalid Returned Error Messages


        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnCreateView_Test()
        {
            // Arrange
            AssetTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - RETURNED VALUES
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnCreateLinkedSettingTypesActionAndAssetTypeSettingTypeControllerAndAssetTypeId_Test()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int newId = _assetTypes.Count() + 1;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                Name = "New Name"
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("CreateLinkedSettingTypes", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetTypeSettingType", routeResult.RouteValues["controller"]);
            // Assert - DATABASE VALUES
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var actualAssetType = _assetTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmCreate.Name, actualAssetType.Name, "AssetType Name");
            // Assert - RETURNED VALUES
            Assert.AreEqual(newId, routeResult.RouteValues["assetTypeId"], "AssetType Id");
        }
        /*
        [TestMethod()]
        public void Create_Get_WhenNoInputVauesProvided_ReturnCreateViewTest()
        {
            // Arrange
            AssetTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsNull(viewResult.ViewData.Model, "View Model Returned.");
            // Assert - MESSAGE
        }

        [TestMethod()]
        public void Create_Get_WhenTempDataErrorMessageProvidedIsValid_ReturnCreateViewAndErrorMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            List<AssetType> vmExpected = _assetTypes.AsQueryable().ToList();
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Create();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsNull(viewResult.ViewData.Model, "View Model Returned.");
            // Assert - MESSAGE
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Create_Post_WhenValidRecordAddedToDatabase_UpdateDatabaseAndReturnIndexActionAndSuccessMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int newId = _assetTypes.Count + 1;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - DATABASE VALUES
            var dtoAssetType = _assetTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.Name, dtoAssetType.Name, "Name");
            Assert.AreEqual(true, dtoAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
            // Assert - MESSAGE
            Assert.AreEqual("Record created.", controller.TempData["SuccessMessage"]);
        }

        [TestMethod()]
        public void Create_Post_WhenDuplicateRecordFoundAndIsActiveEqualsFalse_UpdateDatabaseAndReturnIndexActionAndSuccessMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dtoAssetType.IsActive;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                //Id = 0, 
                Name = dtoAssetType.Name,
                //IsActive = false 
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - DATABASE VALUES
            var dbResult = _assetTypes.Where(r => r.Name == vmExpected.Name).ToList();
            Assert.AreEqual(1, dbResult.Count(), "Number of records with same name in database.");
            Assert.IsFalse(initialIsActive, "Intial IsActive value for record with matching name.");
            Assert.AreEqual(true, dbResult[0].IsActive, "Updated IsActive value for record with matching name.");
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction committed.");
            // Assert - MESSAGE
            Assert.AreEqual("Record is visible.", controller.TempData["SuccessMessage"]);
        }

        [TestMethod()]
        public void Create_Post_WhenModelStateNotValid_ReturnCreateActionTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            CreateViewModel vmExpected = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Create", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
        }

        [TestMethod()]
        public void Create_Post_WhenNameProvidedIsNull_ReturnCreateViewAndViewModelAndErrorMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                //Id = 0, 
                Name = null,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Name is required.", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Create_Post_WhenNameProvidedIsADuplicate_ReturnCreateViewAndViewModelAndErrorMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(4); // record to duplicate
            bool initialIsActive = dtoAssetType.IsActive;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = dtoAssetType.Name,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmCreate.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmCreate.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmCreate.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Name already exists.", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Create_Post_WhenEntitiesFromDatabaseNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // List of invalid entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            CreateViewModel vmExpected = new CreateViewModel()
            {
                //Id = 0, 
                Name = "NewAssetTypeName",
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Unable to add record at this time. Try again later.", controller.ViewData["ErrorMessage"]);
        }
        */


        [TestMethod()]
        public void Index_Get_WhenNoInputVauesProvided_ReturnIndexViewAndSortedViewModelTest()
        {
            // Arrange
            _assetTypes[0].Name = "Zzzz AssetType";
            _assetTypes[1].Name = "A AssetType";
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeController controller = new AssetTypeController(_unitOfWork);
            List<AssetType> vmExpected = _assetTypes.AsQueryable().ToList();
            int lastIndex = vmExpected.Count() - 1;

            // Act
            var result = controller.Index();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            // Assert - VIEW Model
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(vmExpected.Count, vmResult.Count, "Number of Records.");
            Assert.AreEqual("A AssetType", vmResult[0].Name, "Name");
            Assert.AreEqual("Zzzz AssetType", vmResult[lastIndex].Name, "Name");
            // Assert - MESSAGE
        }

        [TestMethod()]
        public void Index_Get_WhenTempDataSuccessMessageProvidedIsValid_ReturnIndexViewAndViewModelAndSuccessMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            List<AssetType> vmExpected = _assetTypes.AsQueryable().ToList();
            controller.TempData["SuccessMessage"] = "Test Success Message";

            // Act
            var result = controller.Index();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            // Assert - VIEW Model
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(vmExpected.Count, vmResult.Count, "Number of Records.");
            // Assert - MESSAGE
            Assert.AreEqual("Test Success Message", controller.ViewData["SuccessMessage"]);
        }

        [TestMethod()]
        public void Index_Get_WhenTempDataErrorMessageProvidedIsValid_ReturnIndexViewAndViewModelAndErrorMessageTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            List<AssetType> vmExpected = _assetTypes.AsQueryable().ToList();
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Index();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            // Assert - VIEW Model
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(vmExpected.Count, vmResult.Count, "Number of Records.");
            // Assert - MESSAGE
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Index_Get_WhenNoRecordsFoundInDatabase_ReturnIndexViewAndViewModelAndErrorMessageTest()
        {
            // Arrange
            List<AssetType> entities = new List<AssetType>(); // create empty list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            // Assert - VIEW Model
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
            // Assert - MESSAGE
            Assert.AreEqual("Unable to view information. Try again later.", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Index_Get_WhenEntitiesFromDatabaseNotValid_ReturnIndexViewAndViewModelAndErrorMessageTest()
        {
            // Arrange
            List<AssetType> entities = null; // create invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            // Assert - VIEW Model
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
            // Assert - MESSAGE
            Assert.AreEqual("Unable to view information at this time. Try again later.", controller.ViewData["ErrorMessage"]);
        }



        [TestMethod()]
        public void Edit_Get_WhenIdProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(id);

            // Act
            var result = controller.Edit(id);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(dtoAssetType.Id, vmResult.Id, "Id");
            Assert.AreEqual(dtoAssetType.Name, vmResult.Name, "Name");
            Assert.AreEqual(dtoAssetType.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
        }

        [TestMethod()]
        public void Edit_Get_WhenTempDataErrorMessageProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Edit(id);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            // Assert - MESSAGE
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }
        
        [TestMethod()]
        public void Edit_Get_WhenIdProvidedIsNullTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Edit(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Get_WhenIdProvidedIsOutOfRangeTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Edit(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record. Try again later.", controller.TempData["ErrorMessage"]);
        }
        
        [TestMethod()]
        public void Edit_Get_WhenEntitiesFromDatabaseNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            int id = 1;

            // Act
            var result = controller.Edit(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenNameProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "NewAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
            // Assert - MESSAGE
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenIsActiveProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
            // Assert - MESSAGE
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenAllValuesProvidedHaveNotChangedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
            // Assert - MESSAGE
            Assert.AreEqual(null, controller.TempData["SuccessMessage"]);
        }
        
        [TestMethod()]
        public void Edit_Post_WhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenIdProvidedIsNullTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                //Id = null,
                Name = "New Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
        }
        
        [TestMethod()]
        public void Edit_Post_WhenNameProvidedNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = null,
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Asset Type Name is required.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenNameProvidedIsADuplicateTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "AssetTypeName2", // duplicated name
                IsActive = true
            };
            AssetType dbAssetTypeExisting = _unitOfWork.AssetTypes.Find(r => r.Name == vmExpected.Name);

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Name already exists.", controller.ViewData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenIdProvidedIsOutOfRangeTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = _assetTypes.Count + 2, // index outside of range
                Name = "BadAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VIEW MODEL
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenNoRecordsFoundInDatabaseTest()
        {
            // Arrange
            List<AssetType> entities = new List<AssetType>(); // create empty list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            int id = 1;

            // Act
            var result = controller.Edit(id);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VIEW MODEL
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record. Try again later.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenEntitiesFromDatabaseNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, vmResult.IsActive, "IsActive");
            // Assert - MESSAGE
            Assert.AreEqual("Unable to edit record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }



        [TestMethod()]
        public void Details_Get_WhenIdProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            AssetType dbExpected = _unitOfWork.AssetTypes.Get(id);

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName);
            // Assert
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel));
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(dbExpected.Id, vmResult.Id, "Id");
            Assert.AreEqual(dbExpected.Name, vmResult.Name, "Name");
            Assert.AreEqual(dbExpected.IsActive, vmResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Detials_Get_WhenTempDataErrorMessageProvidedIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Details(id);

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName);
            // Assert - VIEW MODEL
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel));
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            // Assert - MESSAGE
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);

        }

        [TestMethod()]
        public void Details_Get_WhenIdProvidedNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Details(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to display record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Details_Get_WhenIdProvidedIsOutsideOfRangeTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Details(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to display record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Details_Post_WhenNoRecordsFoundInDatabaseTest()
        {
            // Arrange
            List<AssetType> entities = new List<AssetType>(); // create empty list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to display record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Details_Get_WhenEntitiesFromDatabaseNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            int id = 1;

            // Act
            var result = controller.Details(id);

            // Assert - ACTION
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            // Assert - VALUES
            // Assert - MESSAGE
            Assert.AreEqual("Unable to display record. Try again later.", controller.TempData["ErrorMessage"]);
        }


    }
}