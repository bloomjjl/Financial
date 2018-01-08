using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class EditLinkedSettingTypesViewModel
    {
        public EditLinkedSettingTypesViewModel()
        {
        }

        public EditLinkedSettingTypesViewModel(Models.AssetType dtoAssetType, List<EditViewModel> vmEditList)
        {
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            EditViewModels = vmEditList;
        }

        public int AssetTypeId { get; set; }

        [Display(Name = "Asset Type")]
        public string AssetTypeName { get; set; }

        public List<EditViewModel> EditViewModels { get; set; }
    }
}
