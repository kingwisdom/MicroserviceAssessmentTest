using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.Entities
{
    public class oprBoardTemptbl
    {
        [Key]
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }
        
        public int StateOfResidenceId { get; set; }
        
        public string LGAId { get; set; }
        public string Status { get; set; }
        [StringLength(6)]
        public string  OTP { get; set; }
        public string PhoneSend { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
