using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class IndexLinkedAssetTypesViewModel
    {
        public IndexLinkedAssetTypesViewModel()
        {

        }

        public IndexLinkedAssetTypesViewModel(Core.Models.AssetType dtoAssetType,
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            Id = dtoAssetTypeSettingType.Id;
            SettingTypeId = dtoAssetTypeSettingType.SettingTypeId;
            IsActive = dtoAssetTypeSettingType.IsActive;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            AssetTypeIsActive = dtoAssetType.IsActive;
        }

        public int Id { get; set; }

        public int SettingTypeId { get; set; }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        [Display(Name = "AssetType IsActive")]
        public bool AssetTypeIsActive { get; set; }

        [Display(Name = "AssetTypeSettingType IsActive")]
        public bool IsActive { get; set; }
    }
}
