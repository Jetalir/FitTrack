using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace FitTrack.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
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
        private string passwordInput;

        public string PasswordInput
        {
            get
            {
                return passwordInput;
            }
            set { passwordInput = value; }
        }


        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());

        
        private void SignIn() // Method to take the input and check if it matches an registered user
        {
            UserRepository Current = new UserRepository();

            if (Current.GetUserByName(UsernameInput) != null)
            {
                User CurrentUser = new User();
                CurrentUser = Current.GetUserByName(UsernameInput);

                if (CurrentUser.Username == UsernameInput && CurrentUser.Password == PasswordInput)
                {
                    MessageBox.Show("You've Logged in!", "Logged in", MessageBoxButton.OK);
                    CurrentUser.SignIn();

                    WorkoutWindow workoutsWindow = new WorkoutWindow();
                    workoutsWindow.Show();

                    var w = Application.Current.MainWindow;
                    w.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect password!", "Incorrect password", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("That User Doesnt exist!", "No User Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}