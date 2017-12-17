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
using System.Web.Mvc;

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



        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnCreateView_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_DatabaseUpdated_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            int newId = _settingTypes.Count() + 1;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                Name = "New Name",
                IsActive = true
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _settingTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmCreate.Name, dbResult.Name, "SettingType Name");
            Assert.AreEqual(vmCreate.IsActive, dbResult.IsActive, "SettingType IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            int newId = _settingTypes.Count() + 1;
            CreateViewModel vmCreate = new CreateViewModel() { Id = newId };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("CreateLinkedAssetTypes", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetTypeSettingType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(newId, routeResult.RouteValues["settingTypeId"], "AssetType Id");
        }



        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            int expectedCount = _settingTypes.Count();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
        }

        /*
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
        */

    }
}