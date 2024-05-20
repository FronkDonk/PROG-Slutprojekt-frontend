using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private object currentUser;
        public object CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                CurrentView = CurrentUser == null ? (object)SignInVM : ChatVM;
                OnPropertyChanged(nameof(CurrentView));
            }
        }





        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }
        public MainViewModel()
        {
            SignInVM = new SignInViewModel();
            SignUpVM = new SignUpViewModel();
            ChatVM = new ChatViewModel();
            CurrentView = ChatVM;


        }




    }
}
