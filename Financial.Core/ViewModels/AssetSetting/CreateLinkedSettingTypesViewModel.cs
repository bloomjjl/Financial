using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class CreateLinkedSettingTypesViewModel
    {
        public CreateLinkedSettingTypesViewModel() { }

        public CreateLinkedSettingTypesViewModel(Models.Asset dtoAsset, Models.AssetType dtoAssetType,List<CreateViewModel> vmCreate)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
            CreateViewModels = vmCreate;
        }

        public int AssetId { get; set; }
        [Display(Name = "Name")]
        public string AssetName { get; set; }
        [Display(Name = "Type")]
        public string AssetTypeName { get; set; }
        public List<CreateViewModel> CreateViewModels { get; set; }

    }
}
