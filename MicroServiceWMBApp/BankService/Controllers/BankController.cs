using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        IConfiguration _configuration;
        public BankController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetBank")]
        public async Task<IActionResult> GetBank()
        {
            try
            {
                var url = "https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/GetAllBanks";

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "047f0796de0241da836c67cf8d9253b2");

                var response = await client.GetAsync(url);

                string result = response.Content.ReadAsStringAsync().Result;
                //values = JsonConvert.DeserializeObject<List<MiniStatementResp>>(result);
                //if (values.Count() > 0)
                //{
                //    return values;
                //}


            }
            catch (Exception)
            {

                throw;
            }
            

            return Ok();
        }
    }
}
