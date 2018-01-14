using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.RelationshipType
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Models.RelationshipType dtoRelationshipType)
        {
            Id = dtoRelationshipType.Id;
            Name = dtoRelationshipType.Name;
            IsActive = dtoRelationshipType.IsActive;
        }


        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
