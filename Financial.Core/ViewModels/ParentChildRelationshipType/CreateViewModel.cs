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

        public CreateViewModel(Models.RelationshipType dtoRelationshipType, List<SelectListItem> sliRelationshipLevels, List<SelectListItem> sliRelationshipTypes)
        {
            Id = dtoRelationshipType.Id;
            RelationshipTypeName = dtoRelationshipType.Name;
            RelationshipLevels = sliRelationshipLevels;
            RelationshipTypes = sliRelationshipTypes;
        }

        public int Id { get; set; }
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
