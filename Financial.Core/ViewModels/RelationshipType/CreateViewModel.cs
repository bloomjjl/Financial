using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.RelationshipType
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
