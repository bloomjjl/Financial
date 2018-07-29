using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Utilities
{
    public static class AccountUtilityObjectMother
    {
        public static int AssetTypeIdForCreditCard = 3;
    }


    [TestClass()]
    public class AccountUtilityTests
    {
        [TestMethod()]
        public void FormatAccountName_WhenProvidedValidInput_ReturnValue_Test()
        {
            // Arrange
            var assetName = "name";
            int assetTypeId = 1;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
        }

        [TestMethod()]
        public void FormatAccountName_WhenProvidedValidInputForCreditCard_ReturnValue_Test()
        {
            // Arrange
            var assetName = "name";
            int assetTypeId = AccountUtilityObjectMother.AssetTypeIdForCreditCard;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual("name (value)", result, "Result");
        }

        [TestMethod()]
        public void FormatAccountName_WhenProvidedValidInputNotCreditCard_ReturnValue_Test()
        {
            // Arrange
            var assetName = "name";
            int assetTypeId = 1;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual("name", result, "Result");
        }

        [TestMethod()]
        public void FormatAccountName_WhenProvidedInvalidInputAssetName_ReturnValue_Test()
        {
            // Arrange
            var assetName = string.Empty;
            int assetTypeId = 1;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual(string.Empty, result, "Result");
        }

        [TestMethod()]
        public void FormatAccountName_WhenProvidedInvalidInputAssetTypeId_ReturnValue_Test()
        {
            // Arrange
            var assetName = "name";
            int assetTypeId = 0;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual(string.Empty, result, "Result");
        }

        [TestMethod()]
        public void FormatAccountName_WhenProvidedInvalidInputAssetSettingValue_ReturnValue_Test()
        {
            // Arrange
            var assetName = "name";
            int assetTypeId = 1;
            var assetSettingValue = "value";

            // Act
            var result = AccountUtility.FormatAccountName(assetName, assetTypeId, assetSettingValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual("name", result, "Result");
        }

    }
}