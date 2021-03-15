using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Store.Helpers;

namespace Store.Data
{
    public class Checkout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        public bool Deleted { get; set; } = false;

        [Required]
        public DateTime CheckedOutDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime DuoDate { get; set; } = DateTime.Now.AddWeeks(3);
    }
}
