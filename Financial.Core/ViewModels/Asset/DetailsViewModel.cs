using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.Asset
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public DetailsViewModel(Models.Asset dtoAsset, Models.AssetType dtoAssetType)
        {
            Id = dtoAsset.Id;
            Name = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string AssetTypeName { get; set; }
    }
}
