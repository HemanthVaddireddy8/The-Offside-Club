using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingFootballDemo.DAL
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public long CreditCardNumber { get; set; }
        public string CCExpMonth { get; set; }
        public string CCExpYear { get; set; }
        public string NameOnCard { get; set; }
        public int SecurityCode { get; set; }
        public long PhoneNumber { get; set; }
    }
}
