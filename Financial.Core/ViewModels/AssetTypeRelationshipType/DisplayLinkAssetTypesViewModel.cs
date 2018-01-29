using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class DisplayLinkAssetTypesViewModel
    {
        public DisplayLinkAssetTypesViewModel() { }

        public DisplayLinkAssetTypesViewModel(List<SelectListItem> sliAssetTypes, string selectedAssetTypeId)
        {
            AssetTypes = sliAssetTypes;
            SelectedAssetTypeId = selectedAssetTypeId;
        }


        [Required]
        [Display(Name = "Link Asset Type")]
        public string SelectedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> AssetTypes { get; set; }
    }
}
