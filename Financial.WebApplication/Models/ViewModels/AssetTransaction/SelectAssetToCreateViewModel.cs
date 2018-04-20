using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.AssetTransaction
{
    public class SelectAssetToCreateViewModel
    {
        public SelectAssetToCreateViewModel() { }
        public SelectAssetToCreateViewModel(List<SelectListItem> sliAssets)
        {
            Assets = sliAssets;
        }


        [Required]
        [Display(Name = "Asset Name")]
        public string SelectedAssetId { get; set; }
        public IEnumerable<SelectListItem> Assets { get; set; }
    }
}
