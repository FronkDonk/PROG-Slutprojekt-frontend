using Newtonsoft.Json;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.MVVM.ViewModel;
using PROG_Slutprojekt_frontend.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private async void SignUp(object sender, RoutedEventArgs e)
        {
            SignUpSchema validations = new SignUpSchema();
            string userName = username.Text.Trim();
            string userEmail = email.Text.Trim();
            string userPassword = password.Text.Trim();
            MainViewModel mainViewModel = new MainViewModel();
            SignUpUser signUpUser = new SignUpUser(userName, userEmail, userPassword);
            var result = validations.Validate(signUpUser);

            if (result.IsValid)
            {
                MessageBox.Show("User is valid");

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var json = JsonConvert.SerializeObject(new
                        {
                            username = userName,
                            email = userEmail,
                            password = userPassword,
                            createdAt = DateTime.UtcNow
                        });

                        var content = new StringContent(json, Encoding.UTF8, "application/json");


                        HttpResponseMessage res = await httpClient.PostAsync("https://localhost:7229/api/User/register", content);

                        switch (res.StatusCode)
                        {
                            case (System.Net.HttpStatusCode)200:
                                var responseBody = await res.Content.ReadAsStringAsync();
                                UserModel user = JsonConvert.DeserializeObject<UserModel>(responseBody);
                                mainViewModel.CurrentUser = user;

                                MessageBox.Show("User created");
                                break;
                            case (System.Net.HttpStatusCode)409:
                                MessageBox.Show("User already exists");
                                break;
                            case (System.Net.HttpStatusCode)500:
                                MessageBox.Show("Something went wrong. Please try again");
                                break;

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                foreach (var failure in result.Errors)
                {
                    MessageBox.Show(failure.ErrorMessage);
                }
            }
        }
    }
}
