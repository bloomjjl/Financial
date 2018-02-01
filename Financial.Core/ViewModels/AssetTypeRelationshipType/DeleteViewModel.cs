using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType, Models.AssetType dtoSuppliedAssetType, 
            Models.AssetType dtoLinkedAssetType, Models.RelationshipType dtoRelationshipType)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            SuppliedAssetTypeId = dtoSuppliedAssetType.Id;
            SuppliedAssetTypeName = dtoSuppliedAssetType.Name;
            LinkedAssetTypeName = dtoLinkedAssetType.Name;
            RelationshipTypeName = dtoRelationshipType.Name;
        }


        public int Id { get; set; }
        public int SuppliedAssetTypeId { get; set; }
        [Display(Name = "Asset Type")]
        public string SuppliedAssetTypeName { get; set; }
        [Display(Name = "Relationship")]
        public string RelationshipTypeName { get; set; }
        [Display(Name = "Linked Asset Type")]
        public string LinkedAssetTypeName { get; set; }
    }
}
