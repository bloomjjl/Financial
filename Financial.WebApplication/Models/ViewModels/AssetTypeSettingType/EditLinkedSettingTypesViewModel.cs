using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class EditLinkedSettingTypesViewModel
    {
        public EditLinkedSettingTypesViewModel()
        {
        }

        public EditLinkedSettingTypesViewModel(Business.Models.AccountType bmAssetType)
        {
            AssetTypeId = bmAssetType.AssetTypeId;
            AssetTypeName = bmAssetType.AssetTypeName;
        }


        public int AssetTypeId { get; set; }

        [Display(Name = "Name")]
        public string AssetTypeName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public List<Business.Models.AttributeType> SettingTypes { get; set; }
    }
}
