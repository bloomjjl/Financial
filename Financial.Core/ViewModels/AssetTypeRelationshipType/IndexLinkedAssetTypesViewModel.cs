using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class IndexLinkedAssetTypesViewModel
    {
        public IndexLinkedAssetTypesViewModel() { }

        public IndexLinkedAssetTypesViewModel(Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType, Models.AssetType dtoParentAssetType, Models.AssetType dtoChildAssetType, Models.RelationshipType dtoParentChildRelationshipType, Models.RelationshipType dtoChildParentRelationshipType)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            ParentAssetType = dtoParentAssetType.Name;
            ChildAssetType = dtoChildAssetType.Name;
            ParentChildRelationshipType = dtoParentChildRelationshipType.Name;
            ChildParentRelationshipType = dtoChildParentRelationshipType.Name;
        }

        public int Id { get; set; }
        [Display(Name = "Parent")]
        public string ParentAssetType { get; set; }
        [Display(Name = "Child")]
        public string ChildAssetType { get; set; }
        [Display(Name = "Parent-Child")]
        public string ParentChildRelationshipType { get; set; }
        [Display(Name = "Child-Parent")]
        public string ChildParentRelationshipType { get; set; }
    }
}
