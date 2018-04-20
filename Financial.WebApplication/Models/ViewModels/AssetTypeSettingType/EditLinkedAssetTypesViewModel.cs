using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class EditLinkedAssetTypesViewModel
    {
        public EditLinkedAssetTypesViewModel()
        {
        }

        public EditLinkedAssetTypesViewModel(Core.Models.SettingType dtoSettingType, 
            List<EditViewModel> vmEditList)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            EditViewModels = vmEditList;
        }

        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        public List<EditViewModel> EditViewModels { get; set; }
    }
}
