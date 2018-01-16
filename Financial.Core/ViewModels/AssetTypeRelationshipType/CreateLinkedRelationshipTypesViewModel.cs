using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class CreateLinkedRelationshipTypesViewModel
    {
        public CreateLinkedRelationshipTypesViewModel() { }

        public CreateLinkedRelationshipTypesViewModel(List<SelectListItem> sliRelationshipTypes)
        {
            RelationshipTypes = sliRelationshipTypes;
        }


        [Required]
        [Display(Name = "Relationship Type")]
        public string SelectedRelationshipType { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypes { get; set; }

    }
}
