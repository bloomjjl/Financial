using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.Asset
{
    public class IndexViewModel
    {
        public IndexViewModel() {}

        public IndexViewModel(Models.Asset dtoAsset, string assetNameAdditionalInformation, Models.AssetType dtoAssetType)
        {
            Id = dtoAsset.Id;
            AssetName = dtoAsset.Name + assetNameAdditionalInformation;
            AssetTypeName = dtoAssetType.Name;
        }


        public int Id { get; set; }
        [Display(Name = "Name")]
        public string AssetName { get; set; }
        [Display(Name = "Type")]
        public string AssetTypeName { get; set; }

    }
}
