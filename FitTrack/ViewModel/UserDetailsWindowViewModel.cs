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

namespace FitTrack.ViewModel
{
    internal class UserDetailsWindowViewModel : ViewModelBase
    {
        //Variables
        private UserDetailsWindow UserDetailsWindow = Application.Current.Windows.OfType<UserDetailsWindow>().First(); // Assigns the running UserDetailsWindow to the variable.

        private User activeUser;
        public User ActiveUser
        {
            get { return activeUser; }
            set
            {
                activeUser = value;
                OnPropertyChanged(nameof(ActiveUser));
            }
        }


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
            set 
            {
                confirmPasswordInput = value;
                OnPropertyChanged();
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
        //Commands
        public RelayCommand SaveCommand => new RelayCommand(_ => SaveDetails());
        public RelayCommand CancelCommand => new RelayCommand(_ => Cancel());


        //Constructor
        public UserDetailsWindowViewModel()
        {
            UserRepository userRepository = new UserRepository();
            ActiveUser = userRepository.GetSignedInUser(); // Assigns ActiveUser the signed in user to variable

            Country = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(culture => new RegionInfo(culture.Name).EnglishName).Distinct().OrderBy(name => name).ToList(); // Creates a list of all countries

        }


        //Functions

        public void SaveDetails()
        {
            UserRepository userRepository = new UserRepository();
            UserDetailsWindow.SaveMessage.Visibility = Visibility.Visible;

            if (string.IsNullOrEmpty(UsernameInput))
            {
                SetSaveMessage("Please enter a username");
            }
            else if (userRepository.GetUserByName(UsernameInput) != null)
            {
                SetSaveMessage("Username already exists");
            }
            else if (UsernameInput.Length < 3)
            {
                SetSaveMessage("Username must be atleast 3 characters");
            }
            else if (string.IsNullOrEmpty(PasswordInput))
            {
                SetSaveMessage("Please enter a password");
            }
            else if (PasswordInput.Length < 5)
            {
                SetSaveMessage("Password must be atleast 5 characters");
            }
            else if (!PasswordInput.Any(char.IsDigit))
            {
                SetSaveMessage("Password must contain atleast 1 number");
            }
            else if (!PasswordInput.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                SetSaveMessage("Password must contain atleast 1 special character");
            }
            else if (PasswordInput != ConfirmPasswordInput)
            {
                SetSaveMessage("The password does not match");
            }
            else if (string.IsNullOrEmpty(SelectedCountry))
            {
                SetSaveMessage("Please select a country");
            }
            else
            {
                ActiveUser.Username = UsernameInput;
                ActiveUser.Password = PasswordInput;
                ActiveUser.Country = SelectedCountry;
                MessageBox.Show("The details were successfully saved", "Save Successfull", MessageBoxButton.OK);
                WorkoutWindow workoutWindow = new WorkoutWindow();
                workoutWindow.Show();
                UserDetailsWindow.Close();
            }
        }

        public void Cancel()
        {
            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();

            UserDetailsWindow.Close();
        }

        public void SetSaveMessage(string error)
        {
            UserDetailsWindow.SaveMessage.Text = error;
        }

        
    }
}
