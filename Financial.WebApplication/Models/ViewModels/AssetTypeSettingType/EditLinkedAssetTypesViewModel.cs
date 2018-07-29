using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class EditLinkedAssetTypesViewModel
    {
        public EditLinkedAssetTypesViewModel()
        {
        }
        /*
        public EditLinkedAssetTypesViewModel(Core.Models.SettingType dtoSettingType, 
            List<Business.Models.BusinessModels.AssetTypeSettingType> atstLinks)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            LinkedAssetTypeSettingTypes = atstLinks;
        }
        */

        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        public List<Business.Models.AssetTypeSettingType> LinkedAssetTypeSettingTypes { get; set; }
    }
}
