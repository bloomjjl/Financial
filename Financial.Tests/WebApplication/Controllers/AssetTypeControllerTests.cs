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
        protected AssetTypeController _controller;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryUnitOfWork _unitOfWork;
    }

    public static class AssetTypeObjectMother
    {
        public static AssetType GetValidAssetTypeViewModelToCreate()
        {
            return new AssetType()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };
        }
        public static AssetType GetValidAssetTypeViewModelToCreate(string name)
        {
            return new AssetType()
            {
                //Id = 0, 
                Name = name,
                //IsActive = true 
            };
        }
        public static AssetType GetInvalidAssetTypeViewModelWithInvalidNameToCreate()
        {
            return new AssetType()
            {
                //Id = 0, 
                Name = null,
                //IsActive = true 
            };
        }
        public static AssetType GetValidAssetTypeViewModelToEdit()
        {
            return new AssetType()
            {
                Id = 2,
                Name = "NewAssetTypeName",
                IsActive = true
            };
        }
        public static AssetType GetValidAssetTypeViewModelToEdit(AssetType assetType)
        {
            return new AssetType()
            {
                Id = assetType.Id,
                Name = assetType.Name,
                IsActive = assetType.IsActive
            };
        }
        public static AssetType GetValidAssetTypeViewModelWithSameNameToEdit()
        {
            return new AssetType()
            {
                Id = 5,
                Name = "AssetTypeName5",
                IsActive = true
            };
        }
        public static AssetType GetValidAssetTypeViewModelWithDuplicateNameToEdit()
        {
            return new AssetType()
            {
                Id = 2,
                Name = "AssetTypeName5",
                IsActive = true
            };
        }
        public static AssetType GetInvalidAssetTypeViewModelWithInvalidNameToEdit()
        {
            return new AssetType()
            {
                Id = 4,
                Name = null,
                IsActive = true
            };
        }
        public static AssetType GetInvalidAssetTypeViewModelWithInvalidIdToEdit(int id)
        {
            return new AssetType()
            {
                Id = id,
                Name = "NewAssetTypeName",
                IsActive = true
            };
        }

    }

    [TestClass()]
    public class AssetTypeControllerTests : AssetTypeControllerTestsBase
    {
        [TestMethod()]
        public void Index_Get_ReturnsIndexViewWhenNoInputValuesTest()
        {
            // Arrange
            AssetTypeController controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void Index_Get_ViewModelHasAllActiveAssetTypesTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            List<AssetType> expectedAssetTypes = _assetTypes.AsQueryable().Where(r => r.IsActive).ToList();

            // Act
            var result = controller.Index();
            var viewModel = result.ViewData.Model as List<AssetType>;

            // Assert
            Assert.AreEqual(expectedAssetTypes.Count, viewModel.Count);
        }

        [TestMethod()]
        public void Index_Get_ReturnsErrorMessageFromTempDataTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Index();
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
            Assert.AreEqual("Test Error Message", modelState.Errors[0].ErrorMessage);
        }

        [TestMethod()]
        public void Index_Get_ReturnsValidViewModelWhenNoDataFoundTest()
        {
            // Arrange
            List<AssetType> entities = new List<AssetType>(); // create empty list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);

            // Act
            var result = controller.Index();
            var viewModel = result.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(viewModel, typeof(List<AssetType>));
        }

        [TestMethod()]
        public void Index_Get_ReturnsIndexViewWithErrorMessageWhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // create invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);

            // Act
            var result = controller.Index();
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(modelState);
            Assert.IsTrue(modelState.Errors.Any());
            Assert.AreEqual("Unable to view information at this time. Try again later.", modelState.Errors[0].ErrorMessage);
        }



        [TestMethod()]
        public void Create_Get_ReturnsCreateViewWhenNoInputValuesTest()
        {
            // Arrange
            AssetTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void Create_Post_ReturnsIndexViewWhenSuccessfulTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate();

            // Act
            var result = controller.Create(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Create_Post_RecordAddedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate();
            int newId = _assetTypes.Count + 1;

            // Act
            var result = controller.Create(viewModel);
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == newId);

            // Assert
            Assert.AreEqual(viewModel.Name, dbAssetType.Name);
            Assert.AreEqual(true, dbAssetType.IsActive);
            Assert.AreEqual(true, _unitOfWork.Committed);
        }

        [TestMethod()]
        public void Create_Post_UpdateRecordWhenIsActiveEqualsFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dbAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dbAssetType.IsActive;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate(dbAssetType.Name);

            // Act
            var result = controller.Create(viewModel);
            var dbAssetTypeUpdated = _unitOfWork.AssetTypes.Find(r => r.Name == viewModel.Name);

            // Assert
            Assert.AreEqual(initialCount, _assetTypes.Count(), "Number of records in database must not change.");
            Assert.IsFalse(initialIsActive, "Record being duplicated must have IsActive = false.");
            Assert.AreEqual(viewModel.Name, dbAssetType.Name, "Names must match to create error.");
            Assert.AreEqual(true, dbAssetTypeUpdated.IsActive, "Record being duplicated must have IsActive = true.");
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction must be committed.");
        }

        [TestMethod()]
        public void Create_Post_ReturnsCreateViewWhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate();

            // Act
            var result = controller.Create(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Create", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Create_Post_ReturnsCreateViewAndViewModelWithErrorMessageWhenRequiredFieldAssetTypeNameNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetInvalidAssetTypeViewModelWithInvalidNameToCreate();

            // Act
            var result = (ViewResult)controller.Create(viewModel);
            var viewModelReturned = result.ViewData.Model;
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Create", result.ViewName);
            Assert.AreEqual(viewModel, viewModelReturned);
            Assert.AreEqual("Name is required.", modelState.Errors[0].ErrorMessage);
        }

        [TestMethod()]
        public void Create_Post_ReturnsCreateViewAndViewModelWithErrorMessageWhenRequiredFieldAssetTypeNameDuplicatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dbAssetType = _unitOfWork.AssetTypes.Get(4); // record to duplicate
            bool initialIsActive = dbAssetType.IsActive;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate(dbAssetType.Name);

            // Act
            var result = controller.Create(viewModel) as ViewResult;
            var dbAssetTypeUpdated = _unitOfWork.AssetTypes.Find(r => r.Name == viewModel.Name);
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Create", result.ViewName, "Create view must be returned.");
            Assert.AreEqual(viewModel, result.ViewData.Model, "Correct view model must be returned.");
            Assert.AreEqual("Name already exists.", modelState.Errors[0].ErrorMessage, "Error message must match.");
            Assert.AreEqual(initialCount, _assetTypes.Count(), "Number of records in database must not change.");
            Assert.AreEqual(dbAssetType, dbAssetTypeUpdated, "Record must not be updated.");
            Assert.IsTrue(initialIsActive, "Record being duplicated must have IsActive = true.");
            Assert.AreEqual(true, dbAssetTypeUpdated.IsActive, "Record being duplicated must remain IsActive = true.");
            Assert.AreEqual(false, _unitOfWork.Committed, "Transaction must not be committed.");
        }

        [TestMethod()]
        public void Create_Post_ReturnsCreateViewAndViewModelWithErrorMessageWhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // List of invalid entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToCreate();

            // Act
            var result = (ViewResult)controller.Create(viewModel);
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Create", result.ViewName, "Create view must be returned.");
            Assert.AreEqual(viewModel, result.ViewData.Model, "Correct view model must be returned.");
            Assert.AreEqual("Unable to add record at this time. Try again later.", modelState.Errors[0].ErrorMessage, "Error message must match.");
        }


        
        [TestMethod()]
        public void Edit_Get_ReturnsEditViewWhenInputValueAssetTypeIdProvidedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsViewModelWhenRecordFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            AssetType dbAssetType = _unitOfWork.AssetTypes.Get(id);

            // Act
            var result = controller.Edit(id) as ViewResult;
            var viewModel = result.ViewData.Model as AssetType;

            // Assert
            Assert.AreEqual(dbAssetType.Id, viewModel.Id);
            Assert.AreEqual(dbAssetType.Name, viewModel.Name);
            Assert.AreEqual(dbAssetType.IsActive, viewModel.IsActive);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsIndexViewWithErrorMessageWhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            int assetTypeId = 1;

            // Act
            var result = controller.Edit(assetTypeId);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            Assert.IsTrue(controller.TempData.ContainsKey("ErrorMessage"));
            Assert.AreEqual("Unable to edit record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsIndexViewWithErrorMessageWhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int badId = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Edit(badId);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_ReturnsIndexViewWhenSuccessfulTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToEdit();

            // Act
            var result = controller.Edit(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Method redirects to a differnt action.");
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Index action must be returned.");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "AssetType controller must be returned.");
        }

        [TestMethod()]
        public void Edit_Post_ReturnsIndexViewWhenNameNotChangedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelWithSameNameToEdit();

            // Act
            var result = controller.Edit(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Index action must be returned.");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "AssetType controller must be returned.");
        }

        [TestMethod()]
        public void Edit_Post_RecordUpdatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToEdit();

            // Act
            var result = controller.Edit(viewModel);
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == viewModel.Id);

            // Assert
            Assert.AreEqual(viewModel.Id, dbAssetType.Id);
            Assert.AreEqual(viewModel.Name, dbAssetType.Name);
            Assert.AreEqual(viewModel.IsActive, dbAssetType.IsActive);
            Assert.AreEqual(true, _unitOfWork.Committed);
        }

        [TestMethod()]
        public void Edit_Post_UpdateRecordIsActiveWhenIsActiveEqualsFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dbAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dbAssetType.IsActive;
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToEdit(dbAssetType);

            // Act
            var result = controller.Create(viewModel);
            var dbAssetTypeUpdated = _unitOfWork.AssetTypes.Find(r => r.Id == viewModel.Id);

            // Assert
            Assert.AreEqual(initialCount, _assetTypes.Count(), "Number of records in database must not change.");
            Assert.IsFalse(initialIsActive, "Record being updated must have IsActive = false.");
            Assert.AreEqual(viewModel.Name, dbAssetType.Name, "Names must not change.");
            Assert.AreEqual(true, dbAssetTypeUpdated.IsActive, "Record updated must have IsActive = true.");
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction must be committed.");
        }

        [TestMethod()]
        public void Edit_Post_ReturnsIndexViewWithErrorMessageWhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToEdit();

            // Act
            var result = controller.Edit(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        [TestMethod()]
        public void Edit_Post_ReturnsEditViewAndViewModelWithErrorMessageWhenRequiredFieldAssetTypeNameNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            AssetType viewModel = AssetTypeObjectMother.GetInvalidAssetTypeViewModelWithInvalidNameToEdit();

            // Act
            var result = (ViewResult)controller.Edit(viewModel);
            var viewModelReturned = result.ViewData.Model;
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(viewModel, viewModelReturned);
            Assert.AreEqual("Asset Type Name is required.", modelState.Errors[0].ErrorMessage);
        }

        [TestMethod()]
        public void Edit_Post_ReturnsEditViewAndViewModelWithErrorMessageWhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelToEdit();

            // Act
            var result = (ViewResult)controller.Edit(viewModel);
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(viewModel, result.ViewData.Model);
            Assert.AreEqual("Unable to edit record at this time. Try again later.", modelState.Errors[0].ErrorMessage);
        }

        [TestMethod()]
        public void Edit_Post_ReturnsIndexViewWithErrorMessageWhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int badId = _assetTypes.Count + 2; // index outside of range
            AssetType viewModel = AssetTypeObjectMother.GetInvalidAssetTypeViewModelWithInvalidIdToEdit(badId);

            // Act
            var result = controller.Edit(viewModel);
            var routeResult = result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Index view must be returned.");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "AssetType controller must be returned.");
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"], "Error message must match.");
        }

        [TestMethod()]
        public void Edit_Post_ReturnsEditViewAndViewModelWithErrorMessageWhenRequiredFieldAssetTypeNameDuplicatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType viewModel = AssetTypeObjectMother.GetValidAssetTypeViewModelWithDuplicateNameToEdit();
            AssetType dbAssetTypeDuplicated = _unitOfWork.AssetTypes.Find(r => r.Name == viewModel.Name);

            // Act
            var result = controller.Edit(viewModel) as ViewResult;
            var dbAssetTypeUpdated = _unitOfWork.AssetTypes.Find(r => r.Name == viewModel.Name);
            var modelState = result.ViewData.ModelState[""];

            // Assert
            Assert.AreEqual("Edit", result.ViewName, "Edit view must be returned.");
            Assert.AreEqual(viewModel, result.ViewData.Model, "Correct view model must be returned.");
            Assert.AreEqual("Name already exists.", modelState.Errors[0].ErrorMessage, "Error message must match.");
            Assert.AreEqual(initialCount, _assetTypes.Count(), "Number of records in database must not change.");
            Assert.AreEqual(dbAssetTypeDuplicated, dbAssetTypeUpdated, "Duplicated record must not be updated.");
            Assert.AreEqual(false, _unitOfWork.Committed, "Transaction must not be committed.");
        }

    }
}