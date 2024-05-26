using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.Services
{
    internal class Messenger
    {
        private static Messenger _instance;
        public static Messenger Instance => _instance ?? (_instance = new Messenger());

        public event Action<NavigateMessage> NavigateRequested = delegate { };

        public void RaiseNavigateRequest(NavigateMessage message)
        {
            NavigateRequested(message);
        }
    }
}
