using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class License
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
