using PROG_Slutprojekt_frontend.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.Model
{
    internal class ContactModel : ObservableObject
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string chatRoomId { get; set; }

        private ObservableCollection<MessageModel> _messages;
        public ObservableCollection<MessageModel> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); }
        }

        public ContactModel()
        {
            Messages = new ObservableCollection<MessageModel>();
        }
    }
}
