using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class ChatRoomResponse
    {
        public string chatRoomId { get; set; }
        public List<Member> otherParticipants { get; set; }
    }
}
