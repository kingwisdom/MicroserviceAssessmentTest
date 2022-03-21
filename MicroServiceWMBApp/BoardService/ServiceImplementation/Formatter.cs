﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BoardService.ServiceImplementation
{
    public class Formatter
    {
        public string FormattedAmount(decimal amount)
        {
            return amount.ToString("N2", CultureInfo.InvariantCulture);
        }
        public string FormatToDateYearMonthDay(string Param)
        {
            DateTime decChck = DateTime.Now;
            try
            {
                string con = string.Empty;
                DateTime val = Param == null ? decChck : DateTime.TryParse(Param, out decChck) ? decChck : decChck;
                if (val != null)
                {

                    con = string.Format("{0: yyyy-MM-dd}", val);
                }

                return con;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return string.Empty;
            }
        }
        public string FormatTransDate(DateTime? dt)
        {
            if (dt != null)
            {
                return string.Format("{0:dd-MMM-yyyy}", dt);
            }
            return null;
        }
        public string RemoveLast(string txt)
        {
            if (!string.IsNullOrWhiteSpace(txt))
            {
                var getLast = txt.Substring(txt.Length - 1);
                if (getLast != null)
                {
                    if (getLast == ",")
                    {
                        var rem = txt.Substring(0, txt.Length - 1);
                        return rem;
                    }
                }
            }


            return txt;
        }

        public string CheckIfSerialDate(string dateValue)
        {
            if (dateValue.Contains("/") || dateValue.Contains("-"))
            {
                return dateValue;
            }
            int serialValuDate = Convert.ToInt32(dateValue);
            var dateVal = DateTime.FromOADate(serialValuDate);

            return dateVal.ToString();
        }
        public int ValIntergers(string Param)
        {
            try
            {
                int intChck = 0;
                int val = Param == null ? 0 : int.TryParse(Param, out intChck) ? intChck : intChck;

                return val;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public bool ValBool(string Param)
        {
            try
            {
                bool intChck = false;
                bool val = Param == null ? false : bool.TryParse(Param, out intChck) ? intChck : intChck;

                return val;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return false;
            }
        }

        public decimal ValDecimal(string Param)
        {
            try
            {
                decimal decChck = 0;
                decimal val = Param == null ? 0 : decimal.TryParse(Param, out decChck) ? decChck : decChck;

                return val;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public long ValLong(string Param)
        {
            try
            {
                long decChck = 0;
                long val = Param == null ? 0 : long.TryParse(Param, out decChck) ? decChck : decChck;

                return val;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public DateTime ValidateDate(string Param)
        {
            DateTime decChck = DateTime.Now;
            try
            {
                //int vals = Convert.ToInt32(Param);
                //int vals = Convert.ToInt32("25/01/2022");
                //var OADate = DateTime.FromOADate(vals);

                //DateTime val = Param == null ? decChck : (OADate != null ? OADate : decChck);
                DateTime val = Param == null ? decChck : (DateTime.TryParse(Param, out decChck) ? decChck : decChck);

                return val;
            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return decChck;
            }
        }

        public string ValidateDateReturnString(string Param)
        {
            DateTime decChck = DateTime.Now;
            try
            {

                DateTime val = Param == null ? decChck : DateTime.TryParse(Param, out decChck) ? decChck : decChck;
                return string.Format("{0:yyyyMMdd}", val);

            }
            catch (Exception ex)
            {
                var exM = ex == null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
