using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.Asset
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Core.Models.Asset dtoAsset, Core.Models.AssetType dtoAssetType)
        {
            Id = dtoAsset.Id;
            AssetName = dtoAsset.Name;
            AssetTypeName = dtoAssetType.Name;
        }


        public int Id { get; set; }
        [Display(Name = "Name")]
        public string AssetName { get; set; }
        [Display(Name = "Type")]
        public string AssetTypeName { get; set; }
    }
}
