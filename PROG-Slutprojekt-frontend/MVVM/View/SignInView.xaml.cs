using FluentValidation;
using Newtonsoft.Json;
using PROG_Slutprojekt_frontend.MVVM.Model;
using PROG_Slutprojekt_frontend.MVVM.ViewModel;
using PROG_Slutprojekt_frontend.Services;
using PROG_Slutprojekt_frontend.Validators;
using Supabase.Gotrue;
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
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {

            SignInSchema validations = new SignInSchema();
            string userEmail = email.Text.Trim();
            string userPassword = password.Text.Trim();

            SignInUser signInUser = new SignInUser(userEmail, userPassword);
            var result = validations.Validate(signInUser);

            if (result.IsValid)
            {
                MessageBox.Show("User is valid");

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var json = JsonConvert.SerializeObject(new
                        {
                            email = userEmail,
                            password = userPassword,
                        });

                        var content = new StringContent(json, Encoding.UTF8, "application/json");


                        HttpResponseMessage res = await httpClient.PostAsync("https://localhost:7229/api/User/sign-in", content);

                        switch (res.StatusCode)
                        {
                            case (System.Net.HttpStatusCode)200:
                                var responseBody = await res.Content.ReadAsStringAsync();
                                UserModel user = JsonConvert.DeserializeObject<UserModel>(responseBody);
                                MessageBox.Show($"Successfully signed in as {user.email} {user.id}");
                                Messenger.Instance.RaiseNavigateRequest(new NavigateMessage { NewView = new ChatViewModel() });
                                UserService.Instance.User = user;
                                break;
                            case (System.Net.HttpStatusCode)404:
                                MessageBox.Show("invalid credentials");
                                break;
                            case (System.Net.HttpStatusCode)401:
                                MessageBox.Show("invalid credentials");
                                break;
                            case (System.Net.HttpStatusCode)500:
                                MessageBox.Show("Something went wrong. Please try again");
                                break;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("PENIS" + ex.Message);
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
 
        

        private void SignInAsGuest(object sender, RoutedEventArgs e)
        {
            Messenger.Instance.RaiseNavigateRequest(new NavigateMessage { NewView = new SignUpViewModel() });

            //mock user
            
            UserModel user = new UserModel
            {
                id = "2e41d814-ef96-4fe0-aa2d-b8e8fcbe828d",
                username = "JohnDoe",
                email = "johnDoe@gmail.com",
                createdAt = DateTime.Now,
                avatarColor1 = "#FF6347",
                avatarColor2 = "#FFA500"
            };

            UserService.Instance.User = user;
            
        }
    }
}
