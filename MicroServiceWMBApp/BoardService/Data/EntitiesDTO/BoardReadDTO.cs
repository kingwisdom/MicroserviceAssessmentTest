using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Data.EntitiesDTO
{

    public class BoardReadDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public int StateOfResidenceId { get; set; }

        public string LGAId { get; set; }
    }
}
