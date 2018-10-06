using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;

namespace Financial.Business.Utilities
{
    public static class AccountUtility
    {
        public static string FormatAccountName(string assetName, int assetTypeId, string assetAttributeValue)
        {
            // validate input
            if (string.IsNullOrEmpty(assetName))
                return string.Empty;

            // validate additional information
            if(string.IsNullOrEmpty(assetAttributeValue))
                return assetName;

            // (Credit Card)
            if (assetTypeId == AssetType.IdForCreditCard)
                return $"{assetName} ({assetAttributeValue})";

            return assetName;
        }


    }
}
