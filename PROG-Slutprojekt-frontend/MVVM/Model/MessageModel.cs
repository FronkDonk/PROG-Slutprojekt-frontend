using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class MessageModel
    {
        public string? userId { get; set; }
        public string? username { get; set; }
        public string? message { get; set; }
        public DateTime sentAt { get; set; }

        public string? roomId { get; set; }

        public string? avatarColor1 { get; set; }

        public string? avatarColor2 { get; set; }
   
    }
}
