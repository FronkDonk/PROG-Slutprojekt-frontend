using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class SignUpUser
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public SignUpUser(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}
