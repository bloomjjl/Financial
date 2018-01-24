using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.ParentChildRelationshipType
{
    public class CreateViewModel
    {
        public CreateViewModel(){}

        public CreateViewModel(Models.RelationshipType dtoSuppliedRelationshipType, List<SelectListItem> sliRelationshipLevels, List<SelectListItem> sliLinkedRelationshipTypes)
        {
            SuppliedRelationshipTypeId = dtoSuppliedRelationshipType.Id;
            SuppliedRelationshipTypeName = dtoSuppliedRelationshipType.Name;
            RelationshipLevels = sliRelationshipLevels;
            LinkedRelationshipTypes = sliLinkedRelationshipTypes;
        }

        public int SuppliedRelationshipTypeId { get; set; }
        [Display(Name = "Relationship Type")]
        public string SuppliedRelationshipTypeName { get; set; }

        [Required]
        [Display(Name = "Relationship Level")]
        public string SelectedRelationshipLevel { get; set; }
        public IEnumerable<SelectListItem> RelationshipLevels { get; set; }

        [Required]
        [Display(Name = "Linked Relationship Type")]
        public string SelectedLinkedRelationshipType { get; set; }
        public IEnumerable<SelectListItem> LinkedRelationshipTypes { get; set; }

    }
}
