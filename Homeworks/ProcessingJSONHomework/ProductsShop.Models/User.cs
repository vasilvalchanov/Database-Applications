using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Product> boughtProducts;

        private ICollection<Product> soldProducts;

        private ICollection<User> friends;

        public User()
        {
            this.boughtProducts = new HashSet<Product>();
            this.soldProducts = new HashSet<Product>();
            this.friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> BoughtProducts
        {
            get
            {
                return this.boughtProducts;
            }

            set
            {
                this.boughtProducts = value;
            }
        }

        public virtual ICollection<Product> SoldProducts
        {
            get
            {
                return this.soldProducts;
            }

            set
            {
                this.soldProducts = value;
            }
        }

        public virtual ICollection<User> Friends
        {
            get
            {
                return this.friends;
            }

            set
            {
                this.friends = value;
            }
        }

    }
}
