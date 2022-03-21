using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.Entities
{
    public class oprBoard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int StateOfResidenceId { get; set; }
        [Required]
        public string LGAId { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
