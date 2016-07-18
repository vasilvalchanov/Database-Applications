using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType ResourceType { get; set; }

        [Required]
        public string Url { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses
        {
            get
            {
                return this.licenses;
            }

            set
            {
                this.licenses = value;
            }
        }
    }
}
