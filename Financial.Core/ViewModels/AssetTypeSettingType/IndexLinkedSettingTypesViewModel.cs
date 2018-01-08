using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class IndexLinkedSettingTypesViewModel
    {
        public IndexLinkedSettingTypesViewModel()
        {

        }

        public IndexLinkedSettingTypesViewModel(Models.SettingType dtoSettingType, Models.AssetTypeSettingType dtoAssetTypeSettingType)
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

        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        [Display(Name = "SettingType IsActive")]
        public bool SettingTypeIsActive { get; set; }

        [Display(Name = "AssetTypeSettingType IsActive")]
        public bool IsActive { get; set; }
    }
}
