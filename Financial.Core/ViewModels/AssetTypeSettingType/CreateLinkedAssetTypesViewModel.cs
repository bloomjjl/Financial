using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class CreateLinkedAssetTypesViewModel
    {
        public CreateLinkedAssetTypesViewModel()
        {

        }

        public CreateLinkedAssetTypesViewModel(Models.SettingType dtoSettingType, string successMessage, List<CreateViewModel> vmCreate)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            CreateViewModels = vmCreate;
            SuccessMessage = successMessage;
        }


        public int SettingTypeId { get; set; }

        [Display(Name = "Setting Type")]
        public string SettingTypeName { get; set; }

        public List<CreateViewModel> CreateViewModels { get; set; }

        public string SuccessMessage { get; set; }

    }
}
