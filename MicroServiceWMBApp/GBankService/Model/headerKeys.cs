using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBankService.Model
{
    public class headerKeys
    {
        [FromHeader]
        [Required]
        public string Cache_Control { get; set; }
        [FromHeader]
        [Required]
        public string Ocp_Apim_Subscription_Key { get; set; }
    }

    public class bankList
    {
        public string bankName { get; set; }

        public string bankCode { get; set; }
    }
}
