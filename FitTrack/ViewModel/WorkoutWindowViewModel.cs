using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using FitTrack.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.ViewModel
{
    internal class WorkoutWindowViewModel : ViewModelBase
    {
        //Variables
        private WorkoutWindow workoutWindow = Application.Current.Windows.OfType<WorkoutWindow>().First(); // Assigns the running Workoutwindow to the variable.

        private User activeUser;
        public User ActiveUser
        {
            get { return activeUser; }
            set 
            { 
                activeUser = value;
                OnPropertyChanged(nameof(ActiveUser));
                FilterWorkouts();
            }
        }
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Workout> workoutList;
        public ObservableCollection<Workout> WorkoutList
        {
            get { return workoutList; }
            set 
            {
                workoutList = value;
                OnPropertyChanged();
                FilterWorkouts();
            }
        }

        private ObservableCollection<Workout> filteredWorkoutList;
        public ObservableCollection<Workout> FilteredWorkoutList
        {
            get { return filteredWorkoutList; }
            set
            {
                filteredWorkoutList = value;
                OnPropertyChanged(nameof(FilteredWorkoutList));
            }
        }

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }
        private int caloriesBurned;

        public int CaloriesBurned
        {
            get { return caloriesBurned; }
            set 
            { 
                caloriesBurned = value; 
                OnPropertyChanged(nameof(CaloriesBurned));
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

        // Commands
        public RelayCommand WorkoutDetailsCommand => new RelayCommand(_ => WorkoutDetails());
        public RelayCommand TogglePopupCommand => new RelayCommand(_ => TogglePopup());
        public RelayCommand UserDetailsCommand => new RelayCommand(_ => UserDetails());
        public RelayCommand SignOutCommand => new RelayCommand(_ => SignOut());
        public RelayCommand AddCommand => new RelayCommand(_ => AddWorkout());
        public RelayCommand RemoveCommand => new RelayCommand(_ => DeleteItem(), canExecute => CanRemove());

        
        public WorkoutWindowViewModel() // Contructor
        {
             
            WorkoutList = new ObservableCollection<Workout>();
            UserRepository userRepository = new UserRepository();
            ActiveUser = userRepository.GetSignedInUser(); // Assigns sign in state to user

            if (activeUser != null)
            {
                Username = ActiveUser.Username;
            }
            
            WorkoutRepository workoutRepository = new WorkoutRepository();
            WorkoutList = workoutRepository.GetWorkoutList() ?? new ObservableCollection<Workout>();
            FilterWorkouts();
        }
        
        private void AddWorkout()
        {
            AddWorkoutWindow addWorkoutwindow = new AddWorkoutWindow();
            addWorkoutwindow.Show();

            var window = Application.Current.Windows.OfType<WorkoutWindow>().First();
            window.Close();
        }
        public void DeleteItem()
        {
            WorkoutRepository workoutRepository = new WorkoutRepository();
            workoutRepository.DeleteWorkout(SelectedWorkout);
        }
        private void TogglePopup() => IsPopupOpen = !IsPopupOpen; // Toggles popup for User options

        private void UserDetails()
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
            IsPopupOpen = false;
        }
        private void SignOut()
        {
            IsPopupOpen = false;
            ActiveUser.SignOut();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            workoutWindow.Close();

        }
        public bool CanRemove() 
        {
            return SelectedWorkout != null && (ActiveUser is AdminUser || ActiveUser.UserId == SelectedWorkout.Userid);
        }
        
        public void WorkoutDetails()
        {
            if (SelectedWorkout != null)
            {
                WorkoutDetailsWindowViewModel workoutDetailsWindowViewModel = new WorkoutDetailsWindowViewModel(SelectedWorkout);

                WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
                workoutDetailsWindow.Show();
                workoutWindow.Close();
            }
            else
            {
                MessageBox.Show("Select a Workout!");
            }
        }
        public void FilterWorkouts()
        {
            if (ActiveUser != null && ActiveUser is AdminUser)
            {
                FilteredWorkoutList = new ObservableCollection<Workout>(WorkoutList);
            }
            else if (ActiveUser != null && ActiveUser.UserId != null)
            {
                FilteredWorkoutList = new ObservableCollection<Workout>(
                    WorkoutList.Where(workout => workout.Userid == ActiveUser.UserId || workout.Userid == null));
            }
            else
            {
                FilteredWorkoutList = new ObservableCollection<Workout>();
            }
        }
    }
}
