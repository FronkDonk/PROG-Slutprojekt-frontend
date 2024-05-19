using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.ViewModel
{
    internal class ChatViewModel : ObservableObject
    {
        private readonly DataService dataService = new DataService();
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public RelayCommand SendCommand { get; set; }
        public ContactModel selectedContact { get; set; }

        public ContactModel SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Messages)); // Notify that the Messages property has changed
                if (selectedContact != null)
                {
                    GetMessages(selectedContact.chatRoomId);
                }
            }
        }

        public ObservableCollection<MessageModel> Messages
        {
            get
            {
                return SelectedContact?.Messages;
            }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        public ChatViewModel()
        {
            Contacts = new ObservableCollection<ContactModel>();


            SendCommand = new RelayCommand(o =>
            {
                if (SelectedContact != null)
                {
                    SelectedContact.Messages.Add(new MessageModel
                    {
                        username = "something",
                        message = Message,
                        FirstMessage = false,
                        sentAt = DateTime.Now,
                        IsClientMessage = false
                    });

                    Message = "";
                }
            });

            // Sample contacts and messages for initialization
            GetChatRooms();
        }

        private async void GetChatRooms()
        {
            var contacts = await dataService.GetChatRooms("d6076748-26b5-4dde-9ca7-bf3c5e144f7d");

            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }

        }
        private async void GetMessages(string roomId)
        {
            if (selectedContact != null)
            {
                selectedContact.Messages.Clear();

                var messages = await dataService.GetMessagesInRoom(roomId);

                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
        }
    }
}
