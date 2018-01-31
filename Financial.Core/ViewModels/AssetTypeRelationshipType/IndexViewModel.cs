using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType, 
                              Models.AssetType dtoSuppliedAssetType, 
                              Models.AssetType dtoLinkedAssetType, 
                              Models.RelationshipType dtoRelationshipType)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            SuppliedAssetTypeId = dtoSuppliedAssetType.Id;
            SuppliedAssetTypeName = dtoSuppliedAssetType.Name;
            LinkedAssetTypeName = dtoLinkedAssetType.Name;
            RelationshipTypeName = dtoRelationshipType.Name;
        }

        public IndexViewModel(Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType,
                      Models.AssetType dtoParentAssetType,
                      Models.AssetType dtoChildAssetType,
                      Models.RelationshipType dtoParentRelationshipType,
                      Models.RelationshipType dtoChildRelationshipType)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            ParentAssetTypeName = dtoParentAssetType.Name;
            ChildAssetTypeName = dtoChildAssetType.Name;
            ParentRelationshipTypeName = dtoParentRelationshipType.Name;
            ChildRelationshipTypeName = dtoChildRelationshipType.Name;
        }


        public int Id { get; set; }
        public int SuppliedAssetTypeId { get; set; }
        public string SuppliedAssetTypeName { get; set; }
        [Display(Name = "Linked Asset Type")]
        public string LinkedAssetTypeName { get; set; }
        [Display(Name = "Relationship Type")]
        public string RelationshipTypeName { get; set; }

        [Display(Name = "Parent")]
        public string ParentAssetTypeName { get; set; }
        [Display(Name = "Child")]
        public string ChildAssetTypeName { get; set; }
        [Display(Name = "Parent-Child")]
        public string ParentRelationshipTypeName { get; set; }
        [Display(Name = "Child-Parent")]
        public string ChildRelationshipTypeName { get; set; }
    }
}
