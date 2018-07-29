using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Utilities
{
    public static class AccountUtility
    {
        public static int AssetTypeIdForCreditCard = 3;

        public static int SettingTypeIdForAccountNumber = 1;

        public static string FormatAccountName(string assetName, int assetTypeId, string assetSettingValue)
        {
            // validate input
            if (string.IsNullOrEmpty(assetName) || assetTypeId <= 0)
            {
                return string.Empty;
            }

            // validate addtional information
            if(string.IsNullOrEmpty(assetSettingValue))
            {
                return assetName;
            }

            // (Credit Card)
            if (assetTypeId == AssetTypeIdForCreditCard)
            {
                return string.Format("{0} ({1})", assetName, assetSettingValue);
            }

            return assetName;
        }


    }
}
