using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.WebApplication.Models.ViewModels.ParentChildRelationshipType
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Core.Models.ParentChildRelationshipType dtoSuppliedParentChildRelationshipType,
            Core.Models.RelationshipType dtoSuppliedRelationshipType, 
            List<SelectListItem> sliRelationshipLevels, string selectedRelationshipLevelId,
            List<SelectListItem> sliRelationshipTypes, int selectedRelationshipTypeId)
        {
            Id = dtoSuppliedParentChildRelationshipType.Id;
            RelationshipTypeId = dtoSuppliedRelationshipType.Id;
            RelationshipTypeName = dtoSuppliedRelationshipType.Name;
            SelectedRelationshipLevel = selectedRelationshipLevelId;
            RelationshipLevels = sliRelationshipLevels;
            SelectedRelationshipType = selectedRelationshipTypeId.ToString();
            RelationshipTypes = sliRelationshipTypes;
        }

        public int Id { get; set; }

        public int RelationshipTypeId { get; set; }
        [Display(Name = "Relationship Type")]
        public string RelationshipTypeName { get; set; }

        [Required]
        [Display(Name = "Relationship Level")]
        public string SelectedRelationshipLevel { get; set; }
        public IEnumerable<SelectListItem> RelationshipLevels { get; set; }

        [Required]
        [Display(Name = "Linked Relationship Type")]
        public string SelectedRelationshipType { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypes { get; set; }

    }
}
