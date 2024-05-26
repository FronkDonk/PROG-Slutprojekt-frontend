using PROG_Slutprojekt_frontend.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.MVVM.ViewModel
{
    internal class SignInViewModel : ObservableObject
    {
        public ChatViewModel ChatVM { get; set; }
        public SignUpViewModel SignUpVM { get; set; }

        public RelayCommand SignUpViewCommand { get; set; }

        public SignInViewModel() 
        {
            ChatVM = new ChatViewModel();
            SignUpVM = new SignUpViewModel();

            SignUpViewCommand = new RelayCommand((object o) =>
            {
                    CurrentView = SignUpVM;
            });
        }

    }
}
