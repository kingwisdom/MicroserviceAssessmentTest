using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.EntitiesDTO
{
    public class BoardCreateDTO
    {

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string StateOfResidenceId { get; set; }

        public string LGAId { get; set; }
    }
}
