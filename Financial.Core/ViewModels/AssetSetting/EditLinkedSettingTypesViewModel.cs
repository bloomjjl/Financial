using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class EditLinkedSettingTypesViewModel
    {
        public EditLinkedSettingTypesViewModel() { }

        public EditLinkedSettingTypesViewModel(Models.Asset dtoAsset, List<EditViewModel> vmEdit)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            EditViewModels = vmEdit;
        }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public IEnumerable<EditViewModel> EditViewModels { get; set; }
    }
}
