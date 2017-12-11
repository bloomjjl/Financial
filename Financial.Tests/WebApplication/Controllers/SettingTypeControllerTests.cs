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
using Financial.Core.ViewModels.SettingType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class SettingTypeControllerTestsBase
    {
        public SettingTypeControllerTestsBase()
        {
            _settingTypes = FakeSettingTypes.InitialFakeSettingTypes().ToList();
            _repositorySettingType = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork = new InMemoryUnitOfWork();
            _unitOfWork.SettingTypes = _repositorySettingType;
            _controller = new SettingTypeController(_unitOfWork);
        }

        protected IList<SettingType> _settingTypes;
        protected InMemorySettingTypeRepository _repositorySettingType;
        protected InMemoryUnitOfWork _unitOfWork;
        protected SettingTypeController _controller;
    }

    public static class SettingTypeObjectMother
    {
    }

    [TestClass()]
    public class SettingTypeControllerTests : SettingTypeControllerTestsBase
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




        // *** GET - SUCCESS ***
        // Valid Returned Views 
        [TestMethod()]
        public void Index_Get_ReturnsIndexView_WhenNoInputValuesTest()
        {
            // Arrange
            SettingTypeController controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
        
        // Valid Returned View Models
        [TestMethod()]
        public void Index_Get_ReturnsViewModel_WhenNoImputVauesTest()
        {
            // Arrange
            SettingTypeController controller = _controller;
            List<SettingType> expectedSettingTypes = _settingTypes.AsQueryable().ToList();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
            var vmResult = result.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedSettingTypes.Count, vmResult.Count, "Number of records found in database");
        }
        
        // Valid Returned View Models (Continued)
        [TestMethod()]
        public void Index_Get_ReturnsSortedViewModel_WhenNoImputVauesTest()
        {
            // Arrange
            _settingTypes[0].Name = "Zzzz Name";
            _settingTypes[1].Name = "A Name";
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            List<SettingType> vmIndex = _settingTypes.AsQueryable().ToList();
            int lastIndex = vmIndex.Count() - 1;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
            var vmResult = result.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual("A Name", vmResult[0].Name, "Name");
            Assert.AreEqual("Zzzz Name", vmResult[lastIndex].Name, "Name");
        }
        
        // Valid Returned Success Messages
        [TestMethod()]
        public void Index_Get_ReturnsSuccessMessage_WhenTempDataIsValidTest()
        {
            // Arrange
            SettingTypeController controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Success Message";

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Test Success Message", controller.ViewData["SuccessMessage"]);
        }
        
        // *** GET - ERROR ***
        // Valid Returned Error Messages
        [TestMethod()]
        public void Index_Get_ReturnsErrorMessage_WhenTempDataIsValidTest()
        {
            // Arrange
            SettingTypeController controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }
        
        // Invalid Retrieved Database Values 
        [TestMethod()]
        public void Index_Get_ReturnsValidViewModel_WhenNoDataFoundTest()
        {
            // Arrange
            List<SettingType> entities = new List<SettingType>(); // create empty list of entities
            InMemorySettingTypeRepository repository = new InMemorySettingTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.SettingTypes = repository;
            SettingTypeController controller = new SettingTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
        }
        /*
        [TestMethod()]
        public void Index_Get_ReturnsErrorMessage_WhenNoDataFoundTest()
        {
            // Arrange
            List<SettingType> entities = new List<SettingType>(); // create empty list of entities
            InMemorySettingTypeRepository repository = new InMemorySettingTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.SettingTypes = repository;
            SettingTypeController controller = new SettingTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Unable to view information. Try again later.", controller.ViewData["ErrorMessage"]);
        }
        */
        // Invalid Retrieved Database Objects
        [TestMethod()]
        public void Index_Get_ReturnsValidViewModel_WhenEntityNotValidTest()
        {
            // Arrange
            List<SettingType> entities = null; // create invalid list of entities
            InMemorySettingTypeRepository repository = new InMemorySettingTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.SettingTypes = repository;
            SettingTypeController controller = new SettingTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<IndexViewModel>));
        }
        /*
        [TestMethod()]
        public void Index_Get_ReturnsErrorMessage_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // create invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Unable to view information at this time. Try again later.", controller.ViewData["ErrorMessage"]);
        }





        // *** GET - SUCCESS ***
        // Valid Returned Views 
        [TestMethod()]
        public void Create_Get_ReturnsCreateView_WhenNoInputValuesTest()
        {
            // Arrange
            AssetTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        // *** GET - ERROR ***
        // Valid Returned Error Messages
        [TestMethod()]
        public void Create_Get_ReturnsErrorMessage_WhenTempDataIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Create();

            // Assert
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }

        // *** POST - SUCCESS ***
        // Valid Redirected Actions 
        [TestMethod()]
        public void Create_Post_RedirectsToIndexAction_WhenNewRecordAddedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Updated Database Values
        [TestMethod()]
        public void Create_Post_DatabaseUpdated_WhenNewRecordAddedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int newId = _assetTypes.Count + 1;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            var dtoAssetType = _assetTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmCreate.Name, dtoAssetType.Name, "Name");
            Assert.AreEqual(true, dtoAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
        }

        // Valid Returned Success Messages
        [TestMethod()]
        public void Create_Post_ReturnsSuccessMessage_WhenNewRecordAddedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.AreEqual("Record created.", controller.TempData["SuccessMessage"]);
        }

        // Valid Redirected Actions 
        [TestMethod()]
        public void Create_Post_RedirectsToIndexAction_WhenExistingRecordIsActiveEqualsFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dtoAssetType.IsActive;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = dtoAssetType.Name,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Updated Database Values (Continued)
        [TestMethod()]
        public void Create_Post_DatabaseUpdated_WhenExistingRecordIsActiveEqualsFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dtoAssetType.IsActive;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = dtoAssetType.Name,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            var dbAssetTypeUpdated = _unitOfWork.AssetTypes.Find(r => r.Name == vmCreate.Name);
            Assert.AreEqual(initialCount, _assetTypes.Count(), "Number of records in database.");
            Assert.IsFalse(initialIsActive, "Intial value for record being duplicated.");
            Assert.AreEqual(vmCreate.Name, dtoAssetType.Name);
            Assert.AreEqual(true, dbAssetTypeUpdated.IsActive, "Updated value for record being duplicated.");
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction committed.");
        }

        // Valid Returned Success Messages (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsSuccessMessage_WhenExistingRecordIsActiveEqualsFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(3); // record where IsActive equals false
            bool initialIsActive = dtoAssetType.IsActive;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = dtoAssetType.Name,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.AreEqual("Record is visible.", controller.TempData["SuccessMessage"]);
        }



        // *** POST - ERROR ***
        // Invalid Input Values
        [TestMethod()]
        public void Create_Post_RedirectsToCreateAction_WhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0,
                Name = "NewAssetTypeName",
                //IsActive = true
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Create", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Invalid Input Values (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsCreateView_WhenAssetTypeNameEqualsNullTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = null,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        // Invalid Output Values
        [TestMethod()]
        public void Create_Post_ReturnsViewModel_WhenAssetTypeNameEqualsNullTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = null,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmCreate.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmCreate.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmCreate.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages
        [TestMethod()]
        public void Create_Post_ReturnsErrorMessage_WhenAssetTypeNameEqualsNullTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = null,
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.AreEqual("Name is required.", controller.ViewData["ErrorMessage"]);
        }

        // Invalid Input Values (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsCreateView_WhenAssetTypeNameDuplicatedTest()
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

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        // Invalid Output Values (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsViewModel_WhenAssetTypeNameDuplicatedTest()
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

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmCreate.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmCreate.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmCreate.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsErrorMessage_WhenAssetTypeNameDuplicatedTest()
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

            // Assert
            Assert.AreEqual("Name already exists.", controller.ViewData["ErrorMessage"]);
        }

        // Invalid Database Objects
        [TestMethod()]
        public void Create_Post_ReturnsCreateView_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // List of invalid entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = "NewAssetTypeName",
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        // Invalid Output Values (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsViewModel_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // List of invalid entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = "NewAssetTypeName",
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(vmCreate.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmCreate.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmCreate.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Create_Post_ReturnsErrorMessage_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // List of invalid entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            CreateViewModel vmCreate = new CreateViewModel()
            {
                //Id = 0, 
                Name = "NewAssetTypeName",
                //IsActive = true 
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.AreEqual("Unable to add record at this time. Try again later.", controller.ViewData["ErrorMessage"]);
        }



        // *** GET - SUCCESS ***
        // Valid Returned Views 
        [TestMethod()]
        public void Edit_Get_ReturnsEditView_WhenAssetTypeIdProvidedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        // Valid Returned View Models
        [TestMethod()]
        public void Edit_Get_ReturnsViewModel_WhenAssetTypeIdProvidedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            AssetType dtoAssetType = _unitOfWork.AssetTypes.Get(id);

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(dtoAssetType.Id, vmResult.Id, "Id");
            Assert.AreEqual(dtoAssetType.Name, vmResult.Name, "Name");
            Assert.AreEqual(dtoAssetType.IsActive, vmResult.IsActive, "IsActive");
        }


        // *** GET - ERROR ***
        // Valid Returned Error Messages
        [TestMethod()]
        public void Edit_Get_ReturnsErrorMessage_WhenTempDataIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }

        // Invalid Input Values
        [TestMethod()]
        public void Edit_Get_RedirectsToIndexAction_WhenAssetTypeIdNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsErrorMessage_WhenAssetTypeIdNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Retrieved Database Values 
        [TestMethod()]
        public void Edit_Get_RedirectsToIndexAction_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsErrorMessage_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.AreEqual("Unable to edit record. Try again later.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Retrieved Database Objects
        [TestMethod()]
        public void Edit_Get_RedirectsToIndexAction_WhenEntityNotValidTest()
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

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Edit_Get_ReturnsErrorMessage_WhenEntityNotValidTest()
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

            // Assert
            Assert.AreEqual("Unable to edit record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }

        // *** POST - SUCCESS ***
        // Valid Redirected Actions 
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenNameUpdatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "NewAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Updated Database Values
        [TestMethod()]
        public void Edit_Post_DatabaseUpdated_WhenNameUpdatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "NewAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmEdit.Id);
            Assert.AreEqual(vmEdit.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmEdit.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
        }

        // Valid Returned Success Messages
        [TestMethod()]
        public void Edit_Post_ReturnsSuccessMessage_WhenNameUpdatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "NewAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"]);
        }

        // Valid Redirected Actions (Continued)
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenIsActiveUpdatedToFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Redirected Actions (Continued)
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenIsActiveUpdatedToTrueTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 3,
                Name = "AssetTypeName3",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Updated Database Values (Continued)
        [TestMethod()]
        public void Edit_Post_DatabaseUpdated_WhenIsActiveUpdatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmEdit.Id);
            Assert.AreEqual(vmEdit.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmEdit.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
        }

        // Valid Returned Success Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsSuccessMessage_WhenIsActiveUpdatedToTrueTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 3,
                Name = "AssetTypeName3",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"]);
        }

        // Valid Returned Success Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsSuccessMessage_WhenIsActiveUpdatedToFalseTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"]);
        }

        // Valid Redirected Actions (Continued)
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenNothingChangedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Valid Updated Database Values (Continued)
        [TestMethod()]
        public void Edit_Post_DatabaseUpdated_WhenNothingChangedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var dbAssetType = _assetTypes.FirstOrDefault(r => r.Id == vmEdit.Id);
            Assert.AreEqual(vmEdit.Id, dbAssetType.Id, "Id");
            Assert.AreEqual(vmEdit.Name, dbAssetType.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, dbAssetType.IsActive, "IsActive");
            Assert.AreEqual(true, _unitOfWork.Committed, "Committed Transaction");
        }

        // Valid Returned Success Messages (Continued)
        [TestMethod()]
        public void Edit_Post_SuccessMessage_WhenNothingChangedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual(null, controller.TempData["SuccessMessage"]);
        }


        // *** POST - ERROR ***
        // Invalid Input Values
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Invalid Returned Error Messages
        [TestMethod()]
        public void Edit_Post_ReturnsErrorMessage_WhenModelStateNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Input Values (Continued)
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenAssetTypeIdNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                //Id = null,
                Name = "New Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Invalid Input Values (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsEditView_WhenAssetTypeNameNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = null,
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        // Invalid Output Values
        [TestMethod()]
        public void Edit_Post_ReturnsViewModel_WhenAssetTypeNameNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = null,
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmEdit.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmEdit.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsErrorMessage_WhenAssetTypeNameNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = null,
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Asset Type Name is required.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Input Values (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsEditView_WhenAssetTypeNameDuplicatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 1,
                Name = "AssetTypeName2", // duplicated name
                IsActive = true
            };
            AssetType dbAssetTypeExisting = _unitOfWork.AssetTypes.Find(r => r.Name == vmEdit.Name);

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        // Invalid Output Values (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsViewModel_WhenAssetTypeNameDuplicatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 1,
                Name = "AssetTypeName2", // duplicated name
                IsActive = true
            };
            AssetType dbAssetTypeExisting = _unitOfWork.AssetTypes.Find(r => r.Name == vmEdit.Name);

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmEdit.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmEdit.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsErrorMessage_WhenAssetTypeNameDuplicatedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int initialCount = _assetTypes.Count();
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 1,
                Name = "AssetTypeName2", // duplicated name
                IsActive = true
            };
            AssetType dbAssetTypeExisting = _unitOfWork.AssetTypes.Find(r => r.Name == vmEdit.Name);

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Name already exists.", controller.ViewData["ErrorMessage"]);
        }

        // Invalid Database Values 
        [TestMethod()]
        public void Edit_Post_RedirectsToIndexAction_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = _assetTypes.Count + 2, // index outside of range
                Name = "BadAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsErrorMessage_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = _assetTypes.Count + 2, // index outside of range
                Name = "BadAssetTypeName",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Unable to edit record. Try again.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Database Objects
        [TestMethod()]
        public void Edit_Post_ReturnsEditView_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        // Invalid Output Values (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsViewModel_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(vmEdit.Id, vmResult.Id, "Id");
            Assert.AreEqual(vmEdit.Name, vmResult.Name, "Name");
            Assert.AreEqual(vmEdit.IsActive, vmResult.IsActive, "IsActive");
        }

        // Invalid Returned Error Messages (Continued)
        [TestMethod()]
        public void Edit_Post_ReturnsErrorMessage_WhenEntityNotValidTest()
        {
            // Arrange
            List<AssetType> entities = null; // invalid list of entities
            InMemoryAssetTypeRepository repository = new InMemoryAssetTypeRepository(entities);
            InMemoryUnitOfWork unitOfWork = new InMemoryUnitOfWork();
            unitOfWork.AssetTypes = repository;
            AssetTypeController controller = new AssetTypeController(unitOfWork);
            EditViewModel vmEdit = new EditViewModel()
            {
                Id = 2,
                Name = "AssetTypeName2",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmEdit);

            // Assert
            Assert.AreEqual("Unable to edit record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }



        // *** GET - SUCCESS ***
        // Valid Returned Views 
        [TestMethod()]
        public void Details_Get_ReturnsDetailsView_WhenAssetTypeIdProvidedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName);
        }

        // Valid Returned View Models
        [TestMethod()]
        public void Details_Get_ReturnsViewModel_WhenAssetTypeIdProvidedTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            AssetType dbAssetType = _unitOfWork.AssetTypes.Get(id);

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel));
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(dbAssetType.Id, vmResult.Id, "Id");
            Assert.AreEqual(dbAssetType.Name, vmResult.Name, "Name");
            Assert.AreEqual(dbAssetType.IsActive, vmResult.IsActive, "IsActive");
        }

        // *** GET - ERROR ***
        // Valid Returned Error Messages
        [TestMethod()]
        public void Detials_Get_ReturnsErrorMessage_WhenTempDataIsValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = 2;
            controller.TempData["ErrorMessage"] = "Test Error Message";

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.AreEqual("Test Error Message", controller.ViewData["ErrorMessage"]);
        }

        // Invalid Input Values
        [TestMethod()]
        public void Details_Get_RedirectsToIndexAction_WhenAssetTypeIdNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Details_Get_ReturnsErrorMessage_WhenAssetTypeIdNotValidTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int? id = null;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.AreEqual("Unable to display record. Try again.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Retrieved Database Values 
        [TestMethod()]
        public void Details_Get_RedirectsToIndexAction_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Details_Get_ReturnsErrorMessage_WhenEntityNotFoundTest()
        {
            // Arrange
            AssetTypeController controller = _controller;
            int id = _assetTypes.Count + 2; // ID outside of range

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.AreEqual("Unable to display record. Try again later.", controller.TempData["ErrorMessage"]);
        }

        // Invalid Retrieved Database Objects
        [TestMethod()]
        public void Details_Get_RedirectsToIndexAction_WhenEntityNotValidTest()
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

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Details_Get_ReturnsErrorMessage_WhenEntityNotValidTest()
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

            // Assert
            Assert.AreEqual("Unable to display record at this time. Try again later.", controller.TempData["ErrorMessage"]);
        }
        */
    }
}