using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDB.Models
{
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string NewsContent { get; set; }
    }
}
