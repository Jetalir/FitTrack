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
        //Variables

        private WorkoutWindow workoutWindow = Application.Current.Windows.OfType<WorkoutWindow>().First(); // Assigns the running Workoutwindow to the variable.

        private ObservableCollection<Workout> workoutlist;

        public ObservableCollection<Workout> Workoutlist
        {
            get { return workoutlist; }
            set 
            { 
                workoutlist = value;
                OnPropertyChanged();
            }
        }

        private User activeUser;

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
        WorkoutRepository workoutRepository = new WorkoutRepository();

        // Commands
        public RelayCommand TogglePopupCommand => new RelayCommand(_ => TogglePopup());
        public RelayCommand UserDetailsCommand => new RelayCommand(_ => UserDetails());
        public RelayCommand SignOutCommand => new RelayCommand(_ => SignOut());
        public RelayCommand AddCommand => new RelayCommand(_ => AddItem());
        public RelayCommand RemoveCommand => new RelayCommand(_ => DeleteItem(), canExecute => CanRemove());

        
        public WorkoutWindowViewModel() // Contructor
        {
            UserRepository userRepository = new UserRepository();
            ActiveUser = userRepository.AssignSignedIn(); // Assigns sign in state to user
            workoutWindow.Profile.Content = ActiveUser.Username;

            Workoutlist = new ObservableCollection<Workout>();
            WorkoutRepository workoutRepository = new WorkoutRepository();
            Workoutlist = workoutRepository.GetWorkoutList();
            
        }
        
        private void AddItem()
        {
            AddWorkoutWindow addWorkoutwindow = new AddWorkoutWindow();
            addWorkoutwindow.Show();
        }
        public void DeleteItem()
        {
            WorkoutRepository workoutRepository = new WorkoutRepository();
            workoutRepository.DeleteWorkout(selectedWorkout);
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
            return selectedWorkout != null && (ActiveUser is AdminUser || ActiveUser.UserId == selectedWorkout.Userid);
        }
    }
}
