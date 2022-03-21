using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Helper
{
    public class ApiResponse
    {
        public int ResponseCode { get; set; } = -99;
        public string ResponseMessage { get; set; }
        public string UserId { get; set; }
    }
}
