using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.Services;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.ViewModel
{
    internal class AddFriendsViewModel : ObservableObject
    {
        private readonly DataService dataService = new DataService();
        public ObservableCollection<FriendsModel> UsersNotInChatRoom { get; set; }

        public RelayCommand AddFriendCommand { get; set; }

        public AddFriendsViewModel ListViewDataContext
        {
            get { return this; }
        }

        private UserModel user;


        public AddFriendsViewModel()
        {
            AddFriendCommand = new RelayCommand(o =>
            {
                string addedFriendId = (string)o;
                //send request to add friend
                dataService.AddFriend(addedFriendId, user.id);
                int index = UsersNotInChatRoom.IndexOf(UsersNotInChatRoom.First(i => i.id == addedFriendId));
                UsersNotInChatRoom.RemoveAt(index);
            });
            // Remove the added friend from the UsersNotInChatRoom collection
            UsersNotInChatRoom = new ObservableCollection<FriendsModel>();

            user = UserService.Instance.User; 

            getUsersNotInChatRoom(user.id);
        }

        

        private async void getUsersNotInChatRoom(string userId)
        {
            var users = await dataService.GetUsersNotInChatRoom(userId);

            foreach (var user in users)
            {
                UsersNotInChatRoom.Add(user);
            }
        }

    }
}
