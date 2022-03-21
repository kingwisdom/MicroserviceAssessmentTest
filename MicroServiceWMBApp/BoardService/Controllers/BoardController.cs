using BoardService.Data;
using BoardService.Data.Entities;
using BoardService.Data.EntitiesDTO;
using BoardService.Helper;
using BoardService.Repository.Interfaces;
using BoardService.ServiceImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BoardService.Controllers
{
    [Route("api/C1/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        DatabContext _databContext;
        IConfiguration _configuration;
        IRepository<oprBoard> _boardRepository;
        IRepository<oprBoardTemptbl> _oprBoardTemp;
        ApiResponse apiResponse = new ApiResponse();
        oprBoard _boardData = new oprBoard();
        oprBoardTemptbl _oprBoardTempData = new oprBoardTemptbl();
        ManageMessage manageMessage;
        Formatter _formatter;
        public BoardController(
            DatabContext databContext,
            IConfiguration configuration,
            IRepository<oprBoard> boardRepository,
            IRepository<oprBoardTemptbl> oprBoardTemp,
            ManageMessage _manageMessage,
            Formatter formatter)
        {
            _databContext = databContext;
            _configuration = configuration;
            _boardRepository = boardRepository;
            _oprBoardTemp = oprBoardTemp;
            manageMessage = _manageMessage;
            _formatter = formatter;
        }

        [HttpPost("OnBoardCustomer")]
        public async Task<IActionResult> OnBoardCustomer([FromBody] BoardCreateDTO boardData)
        {
            
            try
            {
                if (boardData != null)
                {
                    if (!_formatter.IsValidEmail(boardData.Email))
                    {
                        apiResponse.ResponseCode = -1;
                        apiResponse.ResponseMessage = "Invalid Email";
                        return BadRequest(apiResponse);
                    }

                    string timetoelapse = _configuration["TwilioCredentials:ElapseTimeMinutes"];
                    string sms = "";
                    //send OTP for verification
                    if (!String.IsNullOrEmpty(boardData.PhoneNumber))
                    {
                        //Generate an OTP and send SMS
                        Random rdn = new Random();
                        int password = rdn.Next(111111, 999999);
                        string otp = password.ToString();

                        sms = "This is your one time verification code: " + otp + ". Kindly enter this code to continue your board process. This code expires in " + timetoelapse + " minutes.";

                        var messageResp = await manageMessage.SendSMS(boardData.PhoneNumber, sms);

                        if (messageResp.Contains("failed"))
                        {
                            apiResponse.ResponseCode = -1;
                            apiResponse.ResponseMessage = "OPT Not sent Please try again";
                            return BadRequest(apiResponse);
                        }
                        else
                        {
                            
                            _oprBoardTempData.Email = boardData.Email;
                            _oprBoardTempData.Password = boardData.Password;
                            _oprBoardTempData.PhoneNumber = boardData.PhoneNumber;
                            _oprBoardTempData.StateOfResidenceId = Convert.ToInt16(boardData.StateOfResidenceId);
                            _oprBoardTempData.LGAId = boardData.LGAId;
                            _oprBoardTempData.Status = "Active";
                            _oprBoardTempData.OTP = otp;
                            _oprBoardTempData.PhoneSend = boardData.PhoneNumber;
                            _oprBoardTempData.ExpiryDate = DateTime.Now.AddMinutes(Convert.ToDouble(timetoelapse));
                            _oprBoardTempData.DateCreated = DateTime.Now;
                        }
                        

                        Console.WriteLine("message ", messageResp);
                        await _oprBoardTemp.AddAsync(_oprBoardTempData);
                        int rev = await _databContext.SaveChanges();

                        if (rev > 0)
                        {
                            apiResponse.ResponseCode = 0;
                            apiResponse.ResponseMessage = "Board successfully";
                            return Ok(apiResponse);
                        }
                    }

                    apiResponse.ResponseCode = -2;
                    apiResponse.ResponseMessage = "Phone Number is Empty";
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = -99;
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                apiResponse.ResponseMessage = $"Error {20011}-{ exM}";
                return BadRequest(apiResponse);
            }
            return BadRequest(apiResponse);
        }

        [HttpGet("OnBoardConfirmation")]
        public async Task<IActionResult> OnBoardConfirmation(string otpData)
        {
            try
            {
                //check from the temporary table if the OTP sent back is equal and the time expiry
                var checkTemporaData = await manageMessage.VerifyOTP(otpData);


                //get all the details from the table where OTP is equal to 

                if(checkTemporaData == null)
                {
                    apiResponse.ResponseCode = -2;
                    apiResponse.ResponseMessage = "OTP Not found or Time Expired";
                    return Ok(apiResponse);
                }

                _boardData.Email = checkTemporaData.Email;
                _boardData.Password = checkTemporaData.Password;
                _boardData.PhoneNumber = checkTemporaData.PhoneNumber;
                _boardData.StateOfResidenceId = checkTemporaData.StateOfResidenceId;
                _boardData.LGAId = checkTemporaData.LGAId;
                _boardData.Status = "Active";
                _boardData.DateCreated = DateTime.Now;
                // delete from the temporary table and save into the permanent table
                _oprBoardTemp.Delete(x => x.OTP == otpData);
                int deleteRev = await _databContext.SaveChanges();
                if(deleteRev > 0)
                    await _boardRepository.AddAsync(_boardData);


                int rev = await _databContext.SaveChanges();

                if (rev > 0)
                {
                    apiResponse.ResponseCode = 0;
                    apiResponse.ResponseMessage = "Data Saved Successfully";
                    return Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.ResponseCode = -99;
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                apiResponse.ResponseMessage = $"Error {20011}-{ exM}";
                return BadRequest(apiResponse);
            }

            return BadRequest();
        }

        [HttpGet("GetBoardListOfCustomer")]
        public async Task<IActionResult> GetBoardListOfCustomer()
        {
            try
            {
                var data = await _boardRepository.GetManyAsync(x => x.Status == "Active" && x.Id != 0);

                var resp = new
                {
                    responseData = data
                };
                return Ok(resp);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
