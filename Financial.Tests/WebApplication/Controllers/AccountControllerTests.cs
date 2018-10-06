using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using Financial;
using Financial.Business;
using Financial.Business.Models;
using Financial.Core;
using Financial.Data;
using Financial.WebApplication.Controllers;
using Financial.WebApplication.Models.ViewModels.Account;

namespace Financial.Tests.WebApplication.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IBusinessService> _businessService;
        private Account _account;
        private Financial.Core.Models.Asset _asset;
        private SelectListItem _sliAsset;


        [SetUp]
        public void SetUp()
        {
            _asset = new Financial.Core.Models.Asset { Id = 1, AssetTypeId = 2, Name = "a", IsActive = true };
            _sliAsset = new SelectListItem { Value = _asset.Id.ToString(), Text = _asset.Name, Selected = false };
            _account = new Account{ AssetId = 1, AssetName = "a", AssetTypeId = 2 };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.SetupAllProperties();

            _businessService = new Mock<IBusinessService>();
            _businessService.Setup(bs => bs.AccountService.GetListOfAccounts())
                .Returns(new List<Account> { _account });

            _controller = new AccountController(_unitOfWork.Object, _businessService.Object);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void Index_WhenCalled_ReturnsIndexView_Test()
        {
            var result = _controller.Index();

            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Index_WhenCalled_ReturnsIndexViewModelList_Test()
        {
            var result = _controller.Index();

            Assert.That(result.ViewData.Model, Is.TypeOf<List<IndexViewModel>>());
        }

        [Test]
        public void Index_WhenCalled_ShouldCallOneTimeAccountServiceMethodGetListOfAccounts_Test()
        {
            _controller.Index();

            _businessService.Verify(bs => bs.AccountService.GetListOfAccounts(),
                Times.Once);
        }

        [Test]
        public void Index_WhenAccountsFound_ReturnAccountsOrderedAscendingByName_Test()
        {
            var alphaFirstAssetName = "a";
            var alphaLastAssetName = "z";
            SetUpAccountsOrderedDescendingByAssetName(alphaFirstAssetName, alphaLastAssetName);

            var result = _controller.Index();

            var vmActual = (List<IndexViewModel>) result.Model;

            Assert.Multiple(() =>
            {
                Assert.That(vmActual.Count, Is.EqualTo(2), "Count");
                Assert.That(vmActual[0].AssetName, Is.EqualTo(alphaFirstAssetName), "First Index");
                Assert.That(vmActual[1].AssetName, Is.EqualTo(alphaLastAssetName), "Second Index");
            });
        }

        [Test]
        public void Index_WhenTempDataSuccessMessageIsNotNull_ReturnViewDataSuccessMessage_Test()
        {
            var expectedMessage = "test message";
            _controller.TempData["SuccessMessage"] = expectedMessage;

            var result = _controller.Index();

            var vResult = (ViewResult) result;

            Assert.That(vResult.ViewData["SuccessMessage"].ToString(), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Index_WhenTempDataErrorMessageIsNotNull_ReturnViewDataErrorMessage_Test()
        {
            var expectedMessage = "test message";
            _controller.TempData["ErrorMessage"] = expectedMessage;

            var result = _controller.Index();

            var vResult = (ViewResult)result;

            Assert.That(vResult.ViewData["ErrorMessage"].ToString(), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Index_WhenAccountListEqualsNull_ReturnEmptyViewModelList_Test()
        {
            _businessService.Setup(bs => bs.AccountService.GetListOfAccounts());

            _controller = new AccountController(_unitOfWork.Object, _businessService.Object);

            var result = _controller.Index();

            var vmActual = (List<IndexViewModel>)result.Model;

            Assert.That(vmActual.Count, Is.EqualTo(0));
        }



        // private

        private void SetUpAccountsOrderedDescendingByAssetName(string alphaFirstAssetName, string alphaLastAssetName)
        {
            _businessService.Setup(bs => bs.AccountService.GetListOfAccounts())
                .Returns(new List<Account>
                {
                    new Account {AssetId = 1, AssetName = alphaLastAssetName, AssetTypeId = 3, AssetTypeName = "type" },
                    new Account {AssetId = 1, AssetName = alphaFirstAssetName, AssetTypeId = 3, AssetTypeName = "type" },
                });

            _controller = new AccountController(_unitOfWork.Object, _businessService.Object);

        }
    }
}
