using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{
    public class RegisterResponseModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
