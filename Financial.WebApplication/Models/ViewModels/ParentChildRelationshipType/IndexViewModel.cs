using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.ParentChildRelationshipType
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Core.Models.ParentChildRelationshipType dtoParentChildRelationshipType, Core.Models.RelationshipType dtoSuppliedRelationshipType,
            Core.Models.RelationshipType dtoParentRelationshipType, Core.Models.RelationshipType dtoChildRelationshipType)
        {
            Id = dtoParentChildRelationshipType.Id;
            RelationshipTypeId = dtoSuppliedRelationshipType.Id;
            ParentRelationshipTypeName = dtoParentRelationshipType.Name;
            ChildRelationshipTypeName = dtoChildRelationshipType.Name;
        }

        public int Id { get; set; }
        public int RelationshipTypeId { get; set; }
        [Display(Name = "Parent")]
        public string ParentRelationshipTypeName { get; set; }
        [Display(Name = "Child")]
        public string ChildRelationshipTypeName { get; set; }
    }
}
