using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.Entities
{
    public class oprState
    {
        [Key]
        public int Id { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string Status { get; set; }
    }
}
