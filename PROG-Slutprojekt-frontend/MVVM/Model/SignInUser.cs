using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class SignInUser
    {
        public string email { get; set; }
        public string password { get; set; }

        public SignInUser(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
