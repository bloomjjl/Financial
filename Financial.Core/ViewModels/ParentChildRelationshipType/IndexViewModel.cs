using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.ParentChildRelationshipType
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Models.ParentChildRelationshipType dtoParentChildRelationshipType, Models.RelationshipType dtoSuppliedRelationshipType,
            Models.RelationshipType dtoParentRelationshipType, Models.RelationshipType dtoChildRelationshipType)
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
