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

        //make a list of previous realtime connections

        List<string> existingChannels = new();


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
                    if(!existingChannels.Contains(selectedContact.chatRoomId))
                    {
                        existingChannels.Add(selectedContact.chatRoomId);
                        SetupRealtimeConnection(selectedContact.chatRoomId);
                    }
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

            SendCommand = new RelayCommand(async o =>
            {
                if (SelectedContact != null)
                {
                    SelectedContact.Messages.Add(new MessageModel
                    {
                        username = "asdasd",
                        message = Message,
                        FirstMessage = false,
                        sentAt = DateTime.Now,
                        IsClientMessage = false
                    });

                    Message = "";
                    var url = "https://wzqbaxbadiqwdodpcglt.supabase.co";
                    var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Ind6cWJheGJhZGlxd2RvZHBjZ2x0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTEyODI2NDAsImV4cCI6MjAyNjg1ODY0MH0.edflXOAsbKYV7nuIQaGteGsAbdFaRjB64PyP0uRKnxw";
                    var options = new Supabase.SupabaseOptions
                    {
                        AutoConnectRealtime = true
                    };

                    var supabase = new Supabase.Client(url, key, options);

                    var chatRoom = supabase.Realtime.Connect().Channel($"room-{selectedContact.chatRoomId}");

                    var broadcast = chatRoom.Register<RealtimeMessage>();

                    await chatRoom.Subscribe();


                    await broadcast.Send("new-message", new RealtimeMessage
                    {
                        Payload = new Dictionary<string, object>
                        {
                            { "message", Message },
                            { "chatRoomId", SelectedContact.chatRoomId },
                            { "username", "FronkDonk" },
                            { "userId",  "1"}
                        }
                    });

                    await dataService.SendMessage(selectedContact.chatRoomId, Message, "d6076748-26b5-4dde-9ca7-bf3c5e144f7d");
                }
            });

            GetChatRooms();
        }

        private async void SetupRealtimeConnection(string chatRoomId)
        {
            Debug.WriteLine("Setting up realtime connection");
            // Initialize the Realtime client and channel
            var url = "https://wzqbaxbadiqwdodpcglt.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Ind6cWJheGJhZGlxd2RvZHBjZ2x0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTEyODI2NDAsImV4cCI6MjAyNjg1ODY0MH0.edflXOAsbKYV7nuIQaGteGsAbdFaRjB64PyP0uRKnxw";
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(url, key, options);

            // Subscribe to the channel
            var chatRoom = supabase.Realtime.Connect().Channel($"room-{chatRoomId}");

            var broadcast = chatRoom.Register<RealtimeMessage>();
            broadcast.AddBroadcastEventHandler((sender, baseBroadcast) =>
            {
                var message = baseBroadcast.Payload["message"];
                Debug.WriteLine($"asokdasokdopkasdokpasokpdopkasdokp {message}");
                
            });

            await chatRoom.Subscribe();




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
