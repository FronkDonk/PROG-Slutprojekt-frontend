using PROG_Slutprojekt_frontend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class UserModel : ObservableObject
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public DateTime? createdAt { get; set; }
        public string? avatarColor1 { get; set; }
        public string? avatarColor2 { get; set; }

   
    }
}
