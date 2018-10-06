using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core;

namespace Financial.Business.Models
{
    public class Account
    {
        public Account()
        {
            
        }

        public Account(Core.Models.Asset dtoAsset)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeId = dtoAsset.AssetTypeId;
            AssetTypeName = dtoAsset.AssetType.Name;
        }

        public Account(Core.Models.Asset dtoAsset, Core.Models.AssetType dtoAssetType)
        {
            AssetId = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
        }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

    }
}
