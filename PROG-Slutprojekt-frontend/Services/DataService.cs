using PROG_Slutprojekt_frontend.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var httpResponse = await httpClient.GetAsync($"https://localhost:7229/api/chat/chatrooms/{userId}");
               
                if(httpResponse.IsSuccessStatusCode)
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
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
                                avatarColor1 = participant.avatarColor1,
                                avatarColor2 = participant.avatarColor2

                            };

                            contactModels.Add(contactModel);
                        }
                    }

                    return contactModels;
                } 

                return new List<ContactModel>();


            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"JSON Deserialization Error: {jsonEx.Message}");
                return new List<ContactModel>();
            }
        }

        public async Task AddFriend(string addedFriendId, string userId)
        {
            try
            {
              
                var response = await httpClient.PostAsJsonAsync("https://localhost:7229/api/chat/chatrooms/create", new { addedFriendId, userId });


                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        Debug.WriteLine("Message sent successfully");
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        Debug.WriteLine("Message failed to send");
                        break;
                    default:
                        Debug.WriteLine("An error occurred while sending the message");
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Request exception: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine($"Request was cancelled: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
            }


        }

        public async Task<List<FriendsModel>> GetUsersNotInChatRoom(string userId)
        {
            try
            {

                var response = await httpClient.GetStringAsync($"https://localhost:7229/api/user/usersNotInChatRoom/{userId}");

                var usersNotInChatRoom = JsonSerializer.Deserialize<List<FriendsModel>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                Debug.WriteLine("Users not in chat room:");
                foreach (var user in usersNotInChatRoom)
                {
                    Debug.WriteLine(user.email);
                }

                return usersNotInChatRoom ?? new List<FriendsModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new List<FriendsModel>();
            }
        }

        public async Task SendMessage(string roomId, string message, string userId)
        {
            try
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
                        Debug.WriteLine("Message sent successfully");
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        Debug.WriteLine("Message failed to send");
                        break;
                    default:
                        Debug.WriteLine("An error occurred while sending the message");
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Request exception: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine($"Request was cancelled: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
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
                    avatarColor1 = message.avatarColor1,
                    avatarColor2 = message.avatarColor2
                };
                messageModels.Add(messageModel);
            }

            return messageModels;
        }
    }
}
