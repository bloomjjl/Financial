using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.Asset
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Core.Models.Asset dtoAsset, List<SelectListItem> sliAssetTypes)
        {
            Id = dtoAsset.Id;
            Name = dtoAsset.Name;
            SelectedAssetTypeId = dtoAsset.AssetTypeId.ToString();
            AssetTypes = sliAssetTypes;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string SelectedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> AssetTypes { get; set; }
    }
}
