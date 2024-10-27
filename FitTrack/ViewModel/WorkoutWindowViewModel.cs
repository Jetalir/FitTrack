using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.ViewModel
{
    internal class WorkoutWindowViewModel : ViewModelBase
    {

        private WorkoutWindow workoutWindow = Application.Current.Windows.OfType<WorkoutWindow>().First(); // Assigns the running Workoutwindow to the variable.

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
        private bool isPopupOpen;
        public bool IsPopupOpen
        {
            get => isPopupOpen;
            set
            {
                isPopupOpen = value;
                OnPropertyChanged();
            }
        }

       
        public RelayCommand TogglePopupCommand => new RelayCommand(_ => TogglePopup());
        public RelayCommand UserDetailsCommand => new RelayCommand(_ => UserDetails());
        public RelayCommand SignOutCommand => new RelayCommand(_ => SignOut());
        public RelayCommand AddCommand => new RelayCommand(_ => AddItem());
        public RelayCommand RemoveCommand => new RelayCommand(_ => DeleteItem(), canExecute => selectedWorkout != null);

        public ObservableCollection<Workout> WorkoutList;
        public WorkoutWindowViewModel()
        {
            UserRepository userRepository = new UserRepository();
            ActiveUser = userRepository.AssignSignedIn();

            workoutWindow.Profile.Content = ActiveUser.Username;

            WorkoutList = new ObservableCollection<Workout>();

            WorkoutList.Add(new CardioWorkout { Type = "Cardio", CaloriesBurned = 400, Duration = new TimeSpan(0, 30, 0), Date = DateTime.Now, Notes = "Morning run" });
            WorkoutList.Add(new StrengthWorkout { Type = "Strength", CaloriesBurned = 400, Duration = new TimeSpan(0, 10, 0), Date = DateTime.Now, Notes = "Pushups" });
            
        }
        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }
        private void AddItem()
        {
            AddWorkoutWindow addWorkoutwindow = new AddWorkoutWindow();
            addWorkoutwindow.Show();
        }
        private void DeleteItem()
        {
            WorkoutList.Remove(selectedWorkout);
        }

        private void TogglePopup() => IsPopupOpen = !IsPopupOpen;

        private void UserDetails()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
            IsPopupOpen = false;
        }
        private void SignOut()
        {
           ActiveUser.SignOut();

           MainWindow mainWindow = new MainWindow();
           mainWindow.Show();

           workoutWindow.Close();

        }

    }
}
