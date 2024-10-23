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

namespace FitTrack.ViewModel
{
    internal class WorkoutWindowViewModel
    {
        User ActiveUser { get; set; }

        
        public ObservableCollection<CardioWorkout> CardioWorkouts { get; set; }
        public ObservableCollection<StrengthWorkout> StrengthWorkouts { get; set; }

        List<Workout> WorkoutList = new List<Workout>();

        private Workout selectedWorkout;

        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        public RelayCommand RemoveCommand => new RelayCommand(execute => DeleteItem(), canExecute => selectedWorkout != null);



        private void AddItem()
        {
            AddWorkoutWindow addWorkoutwindow = new AddWorkoutWindow();
            addWorkoutwindow.Show();
        }
        private void DeleteItem()
        {
            
        }

        public WorkoutWindowViewModel()
        {
            CardioWorkouts = new ObservableCollection<CardioWorkout>();
            StrengthWorkouts = new ObservableCollection<StrengthWorkout>();

            WorkoutList.Add(new CardioWorkout { Type = "Cardio", CaloriesBurned = 400, Duration = new TimeSpan(0, 30, 0), Date = DateTime.Now, Notes = "Morning run" });
            WorkoutList.Add(new StrengthWorkout { Type = "Strength", CaloriesBurned = 400, Duration = new TimeSpan(0, 10, 0), Date = DateTime.Now, Notes = "Pushups" });
        }
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                //OnPropertyChanged();
            }
        }


    }
}
