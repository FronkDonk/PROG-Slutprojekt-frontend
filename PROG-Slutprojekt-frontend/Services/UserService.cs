using PROG_Slutprojekt_frontend.Core;
using PROG_Slutprojekt_frontend.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_Slutprojekt_frontend.Services
{
    internal class UserService : ObservableObject
    {
        private static UserService _instance;
        public static UserService Instance => _instance ?? (_instance = new UserService());

        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
