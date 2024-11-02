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
using System.Windows.Navigation;
using System.Xml.Linq;

namespace FitTrack.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {


        //Variables

        private int code = 0;

        public int Code
        {
            get { return code; }
            set { code = value; }
        }



        private string usernameInput;
        public string UsernameInput
        {
            get{ return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
            }
        }
        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            { 
                passwordInput = value;
                OnPropertyChanged();
            }
        }
        private int codeInput;
        public int CodeInput
        {
            get { return codeInput; }
            set
            {
                codeInput = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand OpenForgotPasswordCommand => new RelayCommand(_ => OpenForgotPasswordWindow());
        public RelayCommand SignInCommand => new RelayCommand(_ => SignIn());
        public RelayCommand RegisterCommand => new RelayCommand(_ => Register());


        public MainWindowViewModel()
        {
            
        }
        private void SignIn() // Method to take the input and check if it matches an registered user
        {
            UserRepository Current = new UserRepository();

            if (Current.GetUserByName(UsernameInput) != null) // Controls if the username input is valid
            {
                User CurrentUser = new User();
                CurrentUser = Current.GetUserByName(UsernameInput);

                if (CurrentUser.Username == UsernameInput && CurrentUser.Password == PasswordInput) // When login is Successfull
                {
                    MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().First(); // Assigns the running Workoutwindow to the variable.

                    if (Code == 0)
                    {
                        Code = Random.Shared.Next(1000, 10000);
                        MessageBox.Show($"Your 2fa code is: {Code}");
                        mainWindow.CodeTextBlock.Visibility = Visibility.Visible;
                        mainWindow.CodeTextBox.Visibility = Visibility.Visible;   
                    }
                    else if (CodeInput == Code)
                    {
                        MessageBox.Show("You've Logged in!", "Logged in", MessageBoxButton.OK);
                        CurrentUser.SignIn();

                        WorkoutWindow workoutsWindow = new WorkoutWindow(); // Shows Workout window
                        workoutsWindow.Show();

                        var w = Application.Current.Windows.OfType<MainWindow>().First();
                        w.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Code!", "Incorrect Verifcation Code", MessageBoxButton.OK);
                    }
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

        private void Register() // Opens Register Window
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
        private void OpenForgotPasswordWindow() // Opens the ForgetPasswordWindow
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
        }
    }
}