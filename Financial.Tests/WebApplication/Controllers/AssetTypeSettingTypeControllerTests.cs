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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 4, SettingTypeId = 6, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 4, SettingTypeId = 7, IsActive = true }); // count
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name 4", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name 5", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 6, Name = "SettingType Name 6", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 7, Name = "SettingType Name 7", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 4;
            int expectedCount = 3;

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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 4, SettingTypeId = 6, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 4, SettingTypeId = 7, IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name 4", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name 5", IsActive = true }); // count
            _settingTypes.Add(new SettingType() { Id = 6, Name = "SettingType Name 6", IsActive = false });
            _settingTypes.Add(new SettingType() { Id = 7, Name = "SettingType Name 7", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 4;
            int expectedCount = 2;

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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true }); // count
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name 4", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name 5", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name 6", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name 7", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 4;
            int expectedCount = 3;

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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name 4", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name 5", IsActive = true }); // count
            _assetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name 6", IsActive = false });
            _assetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name 7", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int settingTypeId = 4;
            int expectedCount = 2;

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
            _assetTypes.Add(new AssetType() { Id = assetTypeId, Name = "Name", IsActive = true });

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedSettingTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedSettingTypesViewModel));
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnPreviousSuccessMessage_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1;
            _assetTypes.Add(new AssetType() { Id = assetTypeId, Name = "Name", IsActive = true });
            controller.TempData["SuccessMessage"] = "Test Message";

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual("Test Message", vmResult.Message, "Message");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAssetTypeValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            _assetTypes.Add(new AssetType() { Id = assetTypeId, Name = "New Name", IsActive = true });

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(assetTypeId, vmResult.AssetTypeId, "AssetType Id");
            Assert.AreEqual("New Name", vmResult.AssetTypeName, "AssetType Name");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnSettingTypesFromDatabase_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true }); // count
            _settingTypes.Add(new SettingType() { Id = 2, Name = "Name 2", IsActive = true }); // count
            _settingTypes.Add(new SettingType() { Id = 3, Name = "Name 3", IsActive = true }); // count
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            _assetTypes.Add(new AssetType() { Id = assetTypeId, Name = "New Name", IsActive = true });
            int expectedCount = 3;

            // Act
            var result = controller.CreateLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "CreateViewModel Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 2, Name = "Name 2", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 3, Name = "Name 3", IsActive = true });
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            List<CreateViewModel> vmCreateList = new List<CreateViewModel>();
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 1, IsActive = true }); // count
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 2, IsActive = false }); // count
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 3, IsActive = true }); // count
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = vmCreateList
            };
            int expectedCount = 3;

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes.Where(r => r.AssetTypeId == assetTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true });
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int assetTypeId = _assetTypes.Count() + 1; // new AssetType Id
            List<CreateViewModel> vmCreateList = new List<CreateViewModel>();
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = assetTypeId, SettingTypeId = 1, IsActive = true }); // count
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = assetTypeId,
                CreateViewModels = vmCreateList
            };

            // Act
            var result = controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(assetTypeId, routeResult.RouteValues["id"], "AssetType Id");
        }

        [TestMethod()]
        public void CreateLinkedSettingTypes_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            CreateLinkedSettingTypesViewModel vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetTypeId = _assetTypes.Count() + 1, // new AssetType Id
                Message = "Previous Message",
                CreateViewModels = new List<CreateViewModel>()
            };
            controller.TempData["SuccessMessage"] = "Previous Message";

            // Act
            controller.CreateLinkedSettingTypes(vmExpected);

            // Assert
            Assert.AreEqual("Previous Message, Linked Setting Types Created", controller.TempData["SuccessMessage"].ToString(), "Success Message");
        }



        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1;
            _settingTypes.Add(new SettingType() { Id = settingTypeId, Name = "new Name", IsActive = true });

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("CreateLinkedAssetTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedAssetTypesViewModel));
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnPreviousSuccessMessage_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1;
            _settingTypes.Add(new SettingType() { Id = settingTypeId, Name = "new Name", IsActive = true });
            controller.TempData["SuccessMessage"] = "Test Message";

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual("Test Message", vmResult.Message, "Message");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnSettingTypeValuesFromDatabase_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            _settingTypes.Add(new SettingType() { Id = settingTypeId, Name = "New Name", IsActive = true });

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual(settingTypeId, vmResult.SettingTypeId, "SettingType Id");
            Assert.AreEqual("New Name", vmResult.SettingTypeName, "SettingType Name");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Get_WhenProvidedSettingTypeIdIsValid_ReturnAssetTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Name 1", IsActive = true }); // count
            _assetTypes.Add(new AssetType() { Id = 2, Name = "Name 2", IsActive = true }); // count
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Name 3", IsActive = true }); // count
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            _settingTypes.Add(new SettingType() { Id = settingTypeId, Name = "New Name", IsActive = true });
            int expectedCount = 3;

            // Act
            var result = controller.CreateLinkedAssetTypes(settingTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "CreateViewModel Count");

        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Name 1", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 2, Name = "Name 2", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Name 3", IsActive = true });
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            List<CreateViewModel> vmCreateList = new List<CreateViewModel>();
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = 1, SettingTypeId = settingTypeId, IsActive = true }); // count
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = 2, SettingTypeId = settingTypeId, IsActive = false }); // count
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = 3, SettingTypeId = settingTypeId, IsActive = true }); // count
            CreateLinkedAssetTypesViewModel vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = vmCreateList
            };
            int expectedCount = 3;

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Database Updated");
            var dbResult = _assetTypesSettingTypes.Where(r => r.SettingTypeId == settingTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count(), "New Records Added Count");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Name 1", IsActive = true });
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);

            int settingTypeId = _settingTypes.Count() + 1; // new SettingType Id
            List<CreateViewModel> vmCreateList = new List<CreateViewModel>();
            vmCreateList.Add(new CreateViewModel() { AssetTypeId = 1, SettingTypeId = settingTypeId, IsActive = true }); // count
            CreateLinkedAssetTypesViewModel vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = settingTypeId,
                CreateViewModels = vmCreateList
            };

            // Act
            var result = controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(settingTypeId, routeResult.RouteValues["id"], "SettingType Id");
        }

        [TestMethod()]
        public void CreateLinkedAssetTypes_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            AssetTypeSettingTypeController controller = _controller;
            CreateLinkedAssetTypesViewModel vmExpected = new CreateLinkedAssetTypesViewModel()
            {
                SettingTypeId = _settingTypes.Count() + 1, // new SettingType Id
                Message = "Previous Message",
                CreateViewModels = new List<CreateViewModel>()
            };

            // Act
            controller.CreateLinkedAssetTypes(vmExpected);

            // Assert
            Assert.AreEqual("Previous Message, Linked Asset Types Created", controller.TempData["SuccessMessage"].ToString(), "Success Message");
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
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnAssetTypeValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 4;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(assetTypeId, vmResult.AssetTypeId, "AssetType Id");
            Assert.AreEqual("AssetType Name", vmResult.AssetTypeName, "AssetType Name");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnLinkedSettingTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 4, SettingTypeId = 6, IsActive = false }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 4, SettingTypeId = 7, IsActive = true }); // count
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 6, Name = "SettingType Name", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 7, Name = "SettingType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            AssetTypeSettingTypeController controller = new AssetTypeSettingTypeController(_unitOfWork);
            int assetTypeId = 4;
            int expectedCount = 3;

            // Act
            var result = controller.EditLinkedSettingTypes(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "EditViewModel Count");
        }

        [TestMethod()]
        public void EditLinkedSettingTypes_Get_WhenProvidedAssetTypeIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 4, SettingTypeId = 5, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 4, SettingTypeId = 6, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 4, SettingTypeId = 7, IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 4, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 5, Name = "SettingType Name", IsActive = true }); // count
            _settingTypes.Add(new SettingType() { Id = 6, Name = "SettingType Name", IsActive = false });
            _settingTypes.Add(new SettingType() { Id = 7, Name = "SettingType Name", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 2, SettingTypeId = 3, IsActive = true }); 
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id =2, Name = "AssetType Name", IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 3, Name = "SettingType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
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
            var dtoResult = _assetTypesSettingTypes.FirstOrDefault(r => r.Id == 1);
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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = false }); // count
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true }); // count
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 5, SettingTypeId = 4, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 2, AssetTypeId = 6, SettingTypeId = 4, IsActive = true });
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 3, AssetTypeId = 7, SettingTypeId = 4, IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 4, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 5, Name = "AssetType Name", IsActive = true }); // count
            _assetTypes.Add(new AssetType() { Id = 6, Name = "AssetType Name", IsActive = false });
            _assetTypes.Add(new AssetType() { Id = 7, Name = "AssetType Name", IsActive = true }); // count
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
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
            IList<AssetTypeSettingType> _assetTypesSettingTypes = new List<AssetTypeSettingType>();
            _assetTypesSettingTypes.Add(new AssetTypeSettingType() { Id = 1, AssetTypeId = 3, SettingTypeId = 2, IsActive = true });
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 2, Name = "SettingType Name", IsActive = true });
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 3, Name = "AssetType Name", IsActive = true });
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
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
            var dtoResult = _assetTypesSettingTypes.FirstOrDefault(r => r.Id == 1);
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