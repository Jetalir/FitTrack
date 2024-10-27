using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.ViewModel
{
    internal class RegisterWindowViewModel : ViewModelBase
    {
        private RegisterWindow registerWindow = Application.Current.Windows.OfType<RegisterWindow>().First(); // Assigns the running RegisterWindow to the variable.
        //Variables
        
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set 
            { 
                usernameInput = value; 
                OnPropertyChanged(nameof(usernameInput));
            }
        }

        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set 
            { 
                passwordInput = value; 
                OnPropertyChanged(nameof(passwordInput));
            }
        }

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set { confirmPasswordInput = value; }
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

        private List<string> country { get; set; }
        public List<string> Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged(nameof(country));
            }
        }
        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    OnPropertyChanged(nameof(selectedCountry));
                }
            }
        }
        

        public RelayCommand RegisterNewUserCommand => new RelayCommand(_ => RegisterNewUser());


        //Construktor
        public RegisterWindowViewModel()
        {
            UserRepository userRepository = new UserRepository();
            SecurityQuestion = userRepository.GetSecurityQuestions();

            Country = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(culture => new RegionInfo(culture.Name).EnglishName).Distinct().OrderBy(name => name).ToList(); // Creates a list of all countries
        }

        //Functions
        public void RegisterNewUser()
        {
            registerWindow.ConfirmPasswordMessage.Visibility = Visibility.Visible;

            if (string.IsNullOrEmpty(UsernameInput))
            {
                SetConfirmPasswordMessage("Please enter a username") ;
            }
            else if (UsernameInput.Length < 3)
            {
                SetConfirmPasswordMessage("Username must be atleast 3 characters") ;
            }
            else if (string.IsNullOrEmpty(PasswordInput))
            {
                SetConfirmPasswordMessage("Please enter a password") ;
            }
            else if (PasswordInput.Length < 8)
            {
                SetConfirmPasswordMessage("Password must be atleast 8 characters") ;
            }
            else if (!PasswordInput.Any(char.IsDigit))
            {
                SetConfirmPasswordMessage("Password must contain atleast 1 number") ;
            }
            else if (!PasswordInput.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                SetConfirmPasswordMessage("Password must contain atleast 1 special character");
            }
            else if (PasswordInput != ConfirmPasswordInput)
            {
                SetConfirmPasswordMessage("The password does not match") ;
            }
            else if (string.IsNullOrEmpty(SelectedSecurityQuestion))
            {
                SetConfirmPasswordMessage("Please select a security question");
            }
            else if (string.IsNullOrEmpty(SecurityAnswerInput))
            {
                SetConfirmPasswordMessage("Please enter a security answer");
            }
            else if (string.IsNullOrEmpty(SelectedCountry))
            {
                SetConfirmPasswordMessage("Please select a country");
            }
            else
            {
                UserRepository user = new UserRepository();
                user.AddUser(UsernameInput, PasswordInput, SelectedSecurityQuestion, SecurityAnswerInput, SelectedCountry);
                MessageBox.Show("The user was successfully created", "Registration Successfull", MessageBoxButton.OK);
                registerWindow.Close();
            }
        }
        public void SetConfirmPasswordMessage(string error)
        {
            registerWindow.ConfirmPasswordMessage.Text = error;
        }
    }
}
