using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data;
using Financial.Tests.Data.Fakes;
using System.Web.Mvc;
using Financial.Core.ViewModels.AssetSetting;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetSettingControllerTestsBase : ControllerTestsBase
    {
        public AssetSettingControllerTestsBase()
        {
            _controller = new AssetSettingController(_unitOfWork);
        }

        protected AssetSettingController _controller;
    }

    [TestClass()]
    public class AssetSettingControllerTests : AssetSettingControllerTestsBase
    {
        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnCreateViewAndViewModel_Test()
        {
            // Arrange
            AssetSettingController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert - VIEW
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            // Assert - RETURN VIEW MODEL
            // Assert - RETURNED MESSAGE
        }
    }
}