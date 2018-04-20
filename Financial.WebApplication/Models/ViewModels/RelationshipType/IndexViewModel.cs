using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.RelationshipType
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Core.Models.RelationshipType dtoRelationshipType)
        {
            Id = dtoRelationshipType.Id;
            Name = dtoRelationshipType.Name;
            IsActive = dtoRelationshipType.IsActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name="Active")]
        public bool IsActive { get; set; }
    }
}
