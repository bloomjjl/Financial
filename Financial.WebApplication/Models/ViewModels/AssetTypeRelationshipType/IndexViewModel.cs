using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeRelationshipType
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Core.Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType,
                              Core.Models.AssetType dtoSuppliedAssetType,
                              Core.Models.AssetType dtoLinkedAssetType,
                              Core.Models.RelationshipType dtoRelationshipType)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            SuppliedAssetTypeId = dtoSuppliedAssetType.Id;
            SuppliedAssetTypeName = dtoSuppliedAssetType.Name;
            LinkedAssetTypeName = dtoLinkedAssetType.Name;
            RelationshipTypeName = dtoRelationshipType.Name;
        }

        public IndexViewModel(Core.Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType,
                      Core.Models.AssetType dtoParentAssetType,
                      Core.Models.AssetType dtoChildAssetType,
                      Core.Models.RelationshipType dtoParentRelationshipType,
                      Core.Models.RelationshipType dtoChildRelationshipType)
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
