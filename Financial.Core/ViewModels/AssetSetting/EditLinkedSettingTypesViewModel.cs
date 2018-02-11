using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class EditLinkedSettingTypesViewModel
    {
        public EditLinkedSettingTypesViewModel() { }

        public EditLinkedSettingTypesViewModel(Models.Asset dtoAsset, Models.AssetType dtoAssetType, List<EditViewModel> vmEdit)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
            EditViewModels = vmEdit;
        }

        public int AssetId { get; set; }
        [Display(Name = "Name")]
        public string AssetName { get; set; }
        [Display(Name = "Type")]
        public string AssetTypeName { get; set; }
        public List<EditViewModel> EditViewModels { get; set; }
    }
}
