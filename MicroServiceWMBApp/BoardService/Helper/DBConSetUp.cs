using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.Helper
{
    public class DBConSetUp
    {
        IConfiguration _IConfiguration;
        //LogManager _LogManager;
        public DBConSetUp(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
            //_LogManager = LogManager;
        }
        public string Encrypt(string value)
        {
            var _LogManager = new LogManager(_IConfiguration);
            try
            {
                string EncyValue = Cryptors.EncryptNoKey(value);
                return EncyValue;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                _LogManager.SaveLog($"Internal Error When getting Ip to Post Transaction Error: {exM}");
                return string.Empty;
            }
        }

        public string Decrypt(string value)
        {
            var _LogManager = new LogManager(_IConfiguration);
            try
            {
                string DecryptValue = Cryptors.DecryptNoKey(value);
                return DecryptValue;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                _LogManager.SaveLog($"Internal Error When getting Ip to Post Transaction Error: {exM}");
                return string.Empty;
            }
        }
    }
}
