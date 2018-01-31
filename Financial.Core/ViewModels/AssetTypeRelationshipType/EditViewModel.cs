﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Models.AssetTypeRelationshipType dtoAssetTypeRelationshipType, Models.AssetType dtoAssetType, 
            List<SelectListItem> sliRelationshipLevels, string selectedRelationshipLevel)
        {
            Id = dtoAssetTypeRelationshipType.Id;
            SuppliedAssetTypeId = dtoAssetType.Id;
            SuppliedAssetTypeName = dtoAssetType.Name;
            RelationshipLevels = sliRelationshipLevels;
            SelectedRelationshipLevel = selectedRelationshipLevel;
        }

        public int Id { get; set; }

        public int SuppliedAssetTypeId { get; set; }
        [Display(Name = "Asset Type")]
        public string SuppliedAssetTypeName { get; set; }

        [Required]
        [Display(Name = "Relationship Level")]
        public string SelectedRelationshipLevel { get; set; }
        public IEnumerable<SelectListItem> RelationshipLevels { get; set; }


        [Required]
        [Display(Name = "Relationship Type")]
        public string SelectedParentChildRelationshipTypeId { get; set; }
        public IEnumerable<SelectListItem> ParentChildRelationshipTypes { get; set; }

        [Required]
        [Display(Name = "Link Asset Type")]
        public string SelectedLinkedAssetTypeId { get; set; }
        public IEnumerable<SelectListItem> LinkAssetTypes { get; set; }


    }
}
