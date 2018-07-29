using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System.Collections.Generic;
using System.Linq;
using Financial.Core.Models;
using Financial.WebApplication.Tests.Fakes.Repositories;
using System.Web.Mvc;
using Financial.WebApplication.Models.ViewModels.AssetType;
using Financial.Business;

namespace Financial.WebApplication.Tests.WebApplication.Controllers
{
    public class AssetTypeControllerTestsBase : ControllerTestsBase
    {
        public AssetTypeControllerTestsBase()
        {
            _controller = new AssetTypeController(_unitOfWork, _assetTypeService);
        }

        protected AssetTypeController _controller;
    }

    public static class AssetTypeObjectMother
    {
    }

    [TestClass()]
    public class AssetTypeControllerTests : AssetTypeControllerTestsBase
    {
        // *** TESTING STEPS ***

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
        public void Index_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnAllActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 10, Name = "Name 1", IsActive = true }, // count
                new AssetType() { Id = 11, Name = "Name 2", IsActive = false }}; // count
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            int expectedCount = 1;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Message";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedErrorMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Message";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["ErrorMessage"].ToString(), "Message");
        }

        /*
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
        */



        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>(); // clear table
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            var vmExpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.Name, dtoResult.Name, "Name");
            Assert.AreEqual(true, dtoResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>(); // clear table
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            var vmExpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("CreateLinkedSettingTypes", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetTypeSettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(newId, routeResult.RouteValues["assetTypeId"], "Route assetTypeId");
            Assert.AreEqual("Asset Type Created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        /*
        [TestMethod()]
        public void Create_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new CreateViewModel()
            {
                Name = "Existing Name"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Existing Name", IsActive = true }); // duplicated name
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeController controller = new AssetTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                Name = "Existing Name"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }
        
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
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 10, Name = "Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeController controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            int id = 10;

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 10, Name = "Old Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            AssetTypeController controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                Name = "Updated Name",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataAssetTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Updated Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new EditViewModel();

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action Result");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller Result");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 10, Name = "Update Name", IsActive = true }, 
                new AssetType() { Id = 11, Name = "Existing Name", IsActive = false }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                Name = "Existing Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }


        /*
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
        */



        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel), "View Model");
        }

        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 10, Name = "Name", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeController(_unitOfWork, _assetTypeService);
            int id = 10;

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Details_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Message";
            int id = 1;

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["SuccessMessage"].ToString(), "Message");
        }


        /*
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
        */
        



    }
}