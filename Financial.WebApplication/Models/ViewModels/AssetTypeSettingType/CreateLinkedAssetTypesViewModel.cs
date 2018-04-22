using Financial.Business.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class CreateLinkedAssetTypesViewModel
    {
        public CreateLinkedAssetTypesViewModel()
        {

        }

        public CreateLinkedAssetTypesViewModel(Core.Models.SettingType dtoSettingType,
           List<LinkedAssetTypeSettingType> atstLinks)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            LinkedAssetTypeSettingTypes = atstLinks;
        }


        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        public List<LinkedAssetTypeSettingType> LinkedAssetTypeSettingTypes { get; set; }
    }
}
