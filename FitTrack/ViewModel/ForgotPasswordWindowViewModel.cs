using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.ViewModel
{
    internal class ForgotPasswordWindowViewModel : ViewModelBase
    {
        private ForgotPasswordWindow forgotPasswordWindow = Application.Current.Windows.OfType<ForgotPasswordWindow>().First(); // Assigns the running RegisterWindow to the variable.
        //Variables

        private string usernameInput;
        public string UsernameInput
        {
            get
            {
                return usernameInput;
            }
            set
            {
                usernameInput = value;
            }
        }
        private List<string> securityQuestion { get; set; }
        public List<string> SecurityQuestion
        {
            get { return securityQuestion; } 
            set
            {
                securityQuestion = value;
                OnPropertyChanged(nameof(securityQuestion));
            }
        }
        private string selectedsecurityQuestion;
        public string SelectedSecurityQuestion
        {
            get { return selectedsecurityQuestion; }
            set
            {
                if (selectedsecurityQuestion != value)
                {
                    selectedsecurityQuestion = value;
                    OnPropertyChanged(nameof(selectedsecurityQuestion));
                }
            }
        }

        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
                OnPropertyChanged(nameof(securityAnswerInput));
            }
        }

        public RelayCommand FindPasswordCommand => new RelayCommand(_ => FindPassword());
        //Constructor

        public ForgotPasswordWindowViewModel() 
        {
            UserRepository userRepository = new UserRepository();
            SecurityQuestion = userRepository.GetSecurityQuestions();
        }


        //Functions

        public void FindPassword()
        {
            forgotPasswordWindow.ForgotPasswordMessage.Visibility = Visibility.Visible;
            UserRepository userRepository = new UserRepository();
            userRepository.GetUserByName(UsernameInput);

            if (userRepository.GetUserByName(UsernameInput) != null)
            {
                User user = new User();
                user = userRepository.GetUserByName(UsernameInput);

                if (SelectedSecurityQuestion == user.SecurityQuestion)
                {
                    if (SecurityAnswerInput == user.GetSecurityAnswer())
                    {
                        SetConfirmPasswordMessage("");
                        MessageBox.Show($"Your password is {user.Password}");
                    }
                    else if (string.IsNullOrEmpty(SecurityAnswerInput))
                    {
                    SetConfirmPasswordMessage("Please enter a SecurityAnswer");
                    }
                    else
                    {
                        SetConfirmPasswordMessage("Wrong SecurityAnswer");
                    }
                }
                else if (string.IsNullOrEmpty(SelectedSecurityQuestion))
                {
                    SetConfirmPasswordMessage("Please select a SecurityQuestion");
                }
            }
            else if (string.IsNullOrEmpty(UsernameInput))
            {
                SetConfirmPasswordMessage("Please enter a username");
            }
            else
            {
                SetConfirmPasswordMessage("User does not exist!");
            }

            
        }

        public void SetConfirmPasswordMessage(string error)
        {
            forgotPasswordWindow.ForgotPasswordMessage.Text = error;
        }
    }
}
