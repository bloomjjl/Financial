using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class IndexLinkedSettingTypesViewModel
    {
        public IndexLinkedSettingTypesViewModel()
        {

        }
        public IndexLinkedSettingTypesViewModel(Business.Models.AttributeType bmSettingType)
        {
            Id = bmSettingType.AssetTypeSettingTypeId;
            //AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            //IsActive = dtoAssetTypeSettingType.IsActive;
            SettingTypeId = bmSettingType.SettingTypeId;
            SettingTypeName = bmSettingType.SettingTypeName;
            //SettingTypeIsActive = dtoSettingType.IsActive;
        }
        public IndexLinkedSettingTypesViewModel(Business.Models.AccountType bmAssetType)
        {
            Id = bmAssetType.AssetTypeSettingTypeId;
            //AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            //IsActive = dtoAssetTypeSettingType.IsActive;
            AssetTypeId = bmAssetType.AssetTypeId;
            AssetTypeName = bmAssetType.AssetTypeName;
            //SettingTypeIsActive = dtoSettingType.IsActive;
        }
        public IndexLinkedSettingTypesViewModel(Core.Models.SettingType dtoSettingType,
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            Id = dtoAssetTypeSettingType.Id;
            AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            IsActive = dtoAssetTypeSettingType.IsActive;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            SettingTypeIsActive = dtoSettingType.IsActive;
        }

        public int Id { get; set; }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        [Display(Name = "SettingType IsActive")]
        public bool SettingTypeIsActive { get; set; }

        [Display(Name = "AssetTypeSettingType IsActive")]
        public bool IsActive { get; set; }
    }
}
