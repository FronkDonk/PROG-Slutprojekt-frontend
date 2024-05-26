using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Input;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;

namespace PROG_Slutprojekt_frontend.MVVM.ViewModel
{
    internal class ChatViewModel : ObservableObject
    {
        private readonly DataService dataService = new DataService();
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public RelayCommand SendCommand { get; set; }
        public ContactModel selectedContact { get; set; }

        // The currently selected contact in the chat
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
                    // If a contact is selected, get the messages for the chat room
                    GetMessages(selectedContact.chatRoomId);        
                }
            }
        }
        // The current user
        private UserModel user;
        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
                // When the user is set, get the chat rooms for the user
                GetChatRooms(user.id);
            }
        }
        // Collection of messages in the selected chat room
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
            // Subscribe to the PropertyChanged event of the UserService
            UserService.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(UserService.User))
                {
                    User = UserService.Instance.User;
                }
            };

            user = UserService.Instance.User;
            Debug.WriteLine("CONTACTS LENGTH: ", Contacts?.Count);
            if(user != null)
            {
                GetChatRooms(user.id);
            }

            Contacts = new ObservableCollection<ContactModel>();

            // Initialize the SendCommand with a command that sends a message
            SendCommand = new RelayCommand(async o =>
            {
                if (SelectedContact != null)
                {
                    SelectedContact.Messages.Add(new MessageModel
                    {
                        username = user.username,
                        message = Message,
                        sentAt = DateTime.Now,
                        avatarColor1 = user.avatarColor1,
                        avatarColor2 = user.avatarColor2
                    });

                    string newMessage = Message;

                    Message = "";

                    await dataService.SendMessage(selectedContact.chatRoomId, newMessage, user.id);
                }
            });

        }

        // Method to get the chat rooms for a user
        private async void GetChatRooms(string userId)
        {
            var contacts = await dataService.GetChatRooms(userId);

            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }

        }
        // Method to get the messages in a chat room
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
