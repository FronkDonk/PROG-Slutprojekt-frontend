using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG_Slutprojekt_frontend.MVVM.ViewModel
{
    internal class MainViewModel: ObservableObject
    {
        public SignInViewModel SignInVM { get; set; }

        public SignUpViewModel SignUpVM { get; set; }
        public ChatViewModel ChatVM { get; set; }
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public RelayCommand AddFriendCommand { get; set; }


        private UserModel user;
        public UserModel User
        {
            get { return user; }
            set 
            { 
                user = value; 
                OnPropertyChanged(nameof(User));
            }
        }

        public MainViewModel()
        {
            UserService.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(UserService.User))
                {
                    User = UserService.Instance.User;
                }
            };

        
            Messenger.Instance.NavigateRequested += message =>
            {
                CurrentView = message.NewView;
            };

            SignInVM = new SignInViewModel();

            CurrentView = SignInVM;

        }




    }
}
