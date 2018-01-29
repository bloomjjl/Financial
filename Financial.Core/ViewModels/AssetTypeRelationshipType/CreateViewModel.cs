using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class CreateViewModel
    {
        public CreateViewModel() { }

        public CreateViewModel(Models.AssetType dtoSuppliedAssetType, List<SelectListItem> sliRelationshipLevels)
        {
            SuppliedAssetTypeId = dtoSuppliedAssetType.Id;
            SuppliedAssetTypeName = dtoSuppliedAssetType.Name;
            RelationshipLevels = sliRelationshipLevels;
        }


        public int SuppliedAssetTypeId { get; set; }
        [Display(Name = "Asset Type")]
        public string SuppliedAssetTypeName { get; set; }

        [Required]
        [Display(Name = "Relationship Level")]
        public string SelectedRelationshipLevel { get; set; }
        public IEnumerable<SelectListItem> RelationshipLevels { get; set; }

        [Required]
        [Display(Name = "Relationship Type")]
        public string SelectedParentChildRelationshipType { get; set; }
        public IEnumerable<SelectListItem> ParentChildRelationshipTypes { get; set; }

        [Required]
        [Display(Name = "Link Asset Type")]
        public string SelectedLinkAssetType { get; set; }
        public IEnumerable<SelectListItem> LinkAssetTypes { get; set; }

    }
}
