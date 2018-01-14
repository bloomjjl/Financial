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
            ParentAssetTypeId = dtoParentAssetType.Id;
            ParentAssetType = dtoParentAssetType.Name;
            ChildAssetTypeId = dtoChildAssetType.Id;
            ChildAssetType = dtoChildAssetType.Name;
            ParentChildRelationshipTypeId = dtoParentChildRelationshipType.Id;
            ParentChildRelationshipType = dtoParentChildRelationshipType.Name;
            ChildParentRelationshipTypeId = dtoChildParentRelationshipType.Id;
            ChildParentRelationshipType = dtoChildParentRelationshipType.Name;
        }

        public int Id { get; set; }
        public int ParentAssetTypeId { get; set; }
        [Display(Name = "Parent")]
        public string ParentAssetType { get; set; }
        public int ChildAssetTypeId { get; set; }
        [Display(Name = "Child")]
        public string ChildAssetType { get; set; }
        public int ParentChildRelationshipTypeId { get; set; }
        [Display(Name = "Parent-Child")]
        public string ParentChildRelationshipType { get; set; }
        public int ChildParentRelationshipTypeId { get; set; }
        [Display(Name = "Child-Parent")]
        public string ChildParentRelationshipType { get; set; }
    }
}
