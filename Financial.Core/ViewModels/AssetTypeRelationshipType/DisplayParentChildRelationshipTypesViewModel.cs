using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Core.ViewModels.AssetTypeRelationshipType
{
    public class DisplayParentChildRelationshipTypesViewModel
    {
        public DisplayParentChildRelationshipTypesViewModel() { }

        public DisplayParentChildRelationshipTypesViewModel(List<SelectListItem> sliParentChildRelationshipTypes, string selectedParentChildRelationshipTypeId)
        {
            ParentChildRelationshipTypes = sliParentChildRelationshipTypes;
            SelectedParentChildRelationshipTypeId = selectedParentChildRelationshipTypeId;
        }


        [Required]
        [Display(Name = "Relationship Type")]
        public string SelectedParentChildRelationshipTypeId { get; set; }
        public IEnumerable<SelectListItem> ParentChildRelationshipTypes { get; set; }

    }
}
