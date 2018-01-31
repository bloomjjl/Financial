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

        public DisplayLinkAssetTypesViewModel(List<SelectListItem> sliLinkAssetTypes, string selectedLinkedAssetTypeId)
        {
            LinkAssetTypes = sliLinkAssetTypes;
            SelectedLinkedAssetTypeId = selectedLinkedAssetTypeId;
        }


        [Required]
        [Display(Name = "Link Asset Type")]
        public string SelectedLinkedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> LinkAssetTypes { get; set; }
    }
}
