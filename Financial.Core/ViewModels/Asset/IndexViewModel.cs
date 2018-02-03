using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.Asset
{
    public class IndexViewModel
    {
        public IndexViewModel() {}

        public IndexViewModel(Models.Asset dtoAsset, Models.AssetType dtoAssetType)
        {
            Id = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
        }


        public int Id { get; set; }
        public string AssetName { get; set; }
        public string AssetTypeName { get; set; }
    }
}
