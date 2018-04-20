using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeRelationshipType
{
    public class DeleteViewModel
    {
        public DeleteViewModel() { }

        public DeleteViewModel(Core.Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType,
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
