using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EverythingFootballDemo.BLL
{
    public class Extensions
    {
        public bool isInt(string str) {
            int n;
            if (string.IsNullOrEmpty(str)) return false;
            return int.TryParse(str, out n);
        }
        public bool isLong(string str)
        {
            long n;
            if (string.IsNullOrEmpty(str)) return false;
            return long.TryParse(str, out n);
        }
        public bool isValidEmailID(string str) {
            bool isEmail = Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }
        public bool isValidExpDate(string dateString)
        {
            DateTime dateValue;
            if (DateTime.TryParse(dateString, out dateValue))
            {
                if (dateValue < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
