using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class IndexSettingTypesForAssetTypeViewModel
    {
        public IndexSettingTypesForAssetTypeViewModel() { }
        public IndexSettingTypesForAssetTypeViewModel(Business.Models.SettingType bmSettingType, Business.Models.AssetTypeSettingType bmAssetTypeSettingType)
        {
            AssetTypeSettingTypeId = bmAssetTypeSettingType.AssetTypeSettingTypeId;
            AssetTypeId = bmAssetTypeSettingType.AssetTypeId;
            SettingTypeId = bmSettingType.SettingTypeId;
            SettingTypeName = bmSettingType.SettingTypeName;
            IsLinked = bmAssetTypeSettingType.AssetTypeId > 0;
        }
        public IndexSettingTypesForAssetTypeViewModel(int assetTypeId, Business.Models.SettingType bmSettingTypeLinked)
        {
            AssetTypeSettingTypeId = bmSettingTypeLinked.AssetTypeSettingTypeId;
            AssetTypeId = assetTypeId;
            SettingTypeId = bmSettingTypeLinked.SettingTypeId;
            SettingTypeName = bmSettingTypeLinked.SettingTypeName;
            IsLinked = bmSettingTypeLinked.AssetTypeSettingTypeId > 0;
        }

        public int AssetTypeId { get; set; }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }
        public int AssetTypeSettingTypeId { get; set; }
        public bool IsLinked { get; set; }
    }
}