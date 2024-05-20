using PROG_Slutprojekt_frontend.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace PROG_Slutprojekt_frontend.Services
{
    internal class DataService
    {
        private readonly HttpClient httpClient;

        public DataService()
        {
            httpClient = new HttpClient
            {
            };
        }

        

        public async Task<List<ContactModel>> GetChatRooms(string userId)
        {
            try
            {
                var response = await httpClient.GetStringAsync($"https://localhost:7229/api/chat/chatrooms/{userId}");
                Console.WriteLine("Raw JSON response:");
                Console.WriteLine(response); 

                var chatRoomsResponse = JsonSerializer.Deserialize<ChatRoomsResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                var contactModels = new List<ContactModel>();
                foreach (var chatRoomResponse in chatRoomsResponse?.chatRoomsDetails ?? Enumerable.Empty<ChatRoomResponse>())
                {
                    var participant = chatRoomResponse.otherParticipants?.FirstOrDefault();
                    if (participant != null)
                    {
                        var contactModel = new ContactModel
                        {
                            id = chatRoomResponse.chatRoomId,
                            chatRoomId = chatRoomResponse.chatRoomId,
                            username = participant.username,
                            email = participant.email,
                        };

                        contactModels.Add(contactModel);
                    }
                }

                return contactModels;
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON Deserialization Error: {jsonEx.Message}");
                return new List<ContactModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new List<ContactModel>();
            }
        }

        public async Task SendMessage(string roomId, string message, string userId)
        {
            var newMessage = new MessageModel
            {
                userId = userId,
                message = message,
                roomId = roomId  
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:7229/api/chat/chatrooms/sendMessage", newMessage);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    MessageBox.Show("Message sent successfully");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    MessageBox.Show("Message failed to send");
                    break;
                default:
                    MessageBox.Show("An error occurred while sending the message");
                    break;
            }
            
        }

        public async Task<List<MessageModel>> GetMessagesInRoom(string roomId)
        {
            var response = await httpClient.GetStringAsync($"https://localhost:7229/api/chat/chatrooms/messages/{roomId}");
            var messagesResponse = JsonSerializer.Deserialize<MessagesResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            var messageModels = new List<MessageModel>();

            foreach (var message in messagesResponse?.messages ?? Enumerable.Empty<MessageResponse>())
            {
                var messageModel = new MessageModel
                {
                    username = message.username,
                    roomId = message.chatRoomId,
                    message = message.message,
                    userId = message.userId,
                    sentAt = message.sentAt,
                    IsClientMessage = message.userId == "d6076748-26b5-4dde-9ca7-bf3c5e144f7d", // Hardcoded client id. Change later
                    FirstMessage = null
                };
                messageModels.Add(messageModel);
            }

            return messageModels;
        }
    }
}
