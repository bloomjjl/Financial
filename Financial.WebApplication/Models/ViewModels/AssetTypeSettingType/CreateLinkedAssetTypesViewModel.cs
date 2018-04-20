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
            List<CreateViewModel> vmCreate)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            CreateViewModels = vmCreate;
        }


        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        public List<CreateViewModel> CreateViewModels { get; set; }
    }
}
