using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class CreateLinkedSettingTypesViewModel
    {
        public CreateLinkedSettingTypesViewModel()
        {

        }

        public CreateLinkedSettingTypesViewModel(Models.AssetType dtoAssetType, string message, List<CreateViewModel> vmCreateList)
        {
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            Message = message;
            CreateViewModels = vmCreateList; 
        }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        public List<CreateViewModel> CreateViewModels { get; set; }

        public string Message { get; set; }
    }
}
