using GBankService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankService.Controllers
{
    [Route("api/B1/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        IConfiguration _configuration;
        public BankController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetBank")]
        public async Task<IActionResult> GetBank([FromHeader]headerKeys values)
        {
            try
            {
                var url = "https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/GetAllBanks";

                var client = new HttpClient();
                string cache_Control = values.Cache_Control;
                string ocp_subscriptionKey = values.Ocp_Apim_Subscription_Key;

                if(cache_Control == null && ocp_subscriptionKey == null)
                {
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "047f0796de0241da836c67cf8d9253b2");
                }
                else
                {
                    client.DefaultRequestHeaders.Add("Cache-Control", cache_Control);
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ocp_subscriptionKey);
                }

                var response = await client.GetAsync(url);

                string result = response.Content.ReadAsStringAsync().Result;
                
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //return BadRequest();
        }
    }
}
