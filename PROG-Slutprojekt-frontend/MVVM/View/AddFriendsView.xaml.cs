using PROG_Slutprojekt_frontend.MVVM.ViewModel;
using PROG_Slutprojekt_frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG_Slutprojekt_frontend.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddFriendsView.xaml
    /// </summary>
    public partial class AddFriendsView : UserControl
    {
        public AddFriendsView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Messenger.Instance.RaiseNavigateRequest(new NavigateMessage { NewView = new ChatViewModel() });
        }
    }
}
