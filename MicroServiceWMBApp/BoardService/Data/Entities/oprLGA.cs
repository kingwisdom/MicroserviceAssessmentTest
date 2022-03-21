using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.Entities
{
    public class oprLGA
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string Status { get; set; }
        public oprState State { get; set; }
    }
}
