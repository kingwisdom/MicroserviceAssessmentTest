using BoardService.Data.Entities;
using BoardService.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BoardService.ServiceImplementation
{
    public class ManageMessage
    {
        IConfiguration _configuration;
        IRepository<oprBoardTemptbl> _oprBoardTemp;

        public ManageMessage(
            IConfiguration configuration,
            IRepository<oprBoardTemptbl> oprBoardTemp)
        {
            _configuration = configuration;
            _oprBoardTemp = oprBoardTemp;
        }
        public async Task<string> SendSMS(string recepientPhone, string body)
        {

            //Vanso.SXMP.Response resp = null;
            //Find your Account Sid and Token at twilio.com / console

            string accountSid = _configuration["TwilioCredentials:accountSid"];
            string authToken = _configuration["TwilioCredentials:authToken"];
            string twilPhoneNo = _configuration["TwilioCredentials:TwilPhoneNo"];



            string phone = "+234" + recepientPhone.Substring(1);
            TwilioClient.Init(accountSid, authToken);

            try
            {
                var message = MessageResource.Create(
                        body: body,
                        from: new Twilio.Types.PhoneNumber(twilPhoneNo),
                        to: new Twilio.Types.PhoneNumber(phone)

                    );
                return message.Sid;

            }
            catch (Exception ex)
            {
                return "failed " + ex.Message;
            }
        }

        public async Task<oprBoardTemptbl> VerifyOTP(string optValue)
        {
            var retify = await _oprBoardTemp.GetAsync(x => x.OTP == optValue && x.ExpiryDate > DateTime.Now);
            return retify;
        }

    }
}
