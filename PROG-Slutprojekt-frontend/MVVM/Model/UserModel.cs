using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class UserModel
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }

        public DateTime? createdAt { get; set; }

        public UserModel(string id, string username, string email, string createdAt) 
        { 
            this.id = id;
            this.username = username;
            this.email = email;
            this.createdAt = DateTime.Parse(createdAt);
        }
    }
}
