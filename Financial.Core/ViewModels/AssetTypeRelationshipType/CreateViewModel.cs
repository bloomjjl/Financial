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

        public CreateViewModel(Models.AssetType dtoSuppliedAssetType, List<SelectListItem> sliRelationshipLevels, 
            List<SelectListItem> sliParentRelationshipTypes, List<SelectListItem> sliChildRelationshipTypes,
            List<SelectListItem> sliLinkAssetTypes)
        {
            SuppliedAssetTypeId = dtoSuppliedAssetType.Id;
            SuppliedAssetTypeName = dtoSuppliedAssetType.Name;
            RelationshipLevels = sliRelationshipLevels;
            ParentRelationshipTypes = sliParentRelationshipTypes;
            ChildRelationshipTypes = sliChildRelationshipTypes;
            LinkAssetTypes = sliLinkAssetTypes;
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
        public string SelectedRelationshipType { get; set; }
        public IEnumerable<SelectListItem> ParentRelationshipTypes { get; set; }
        public IEnumerable<SelectListItem> ChildRelationshipTypes { get; set; }

        [Required]
        [Display(Name = "Link Asset Type")]
        public string SelectedLinkAssetType { get; set; }
        public IEnumerable<SelectListItem> LinkAssetTypes { get; set; }

    }
}
