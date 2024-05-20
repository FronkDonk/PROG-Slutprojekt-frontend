using Newtonsoft.Json;
using Supabase.Realtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class RealtimeMessage : BaseBroadcast
    {
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("chatRoomId")]
        public string chatRoomId { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("user_id")]
        public string user_id { get; set; }
    }
}
