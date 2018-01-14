using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.Asset
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }

        public CreateViewModel(List<SelectListItem> sliAssetTypes)
        {
            AssetTypes = sliAssetTypes;
        }

        public int Id { get; set; }

        [Required]
        public string AssetName { get; set; }

        [Required]
        [Display(Name = "Asset Type")]
        public string SelectedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> AssetTypes { get; set; }
    }
}
