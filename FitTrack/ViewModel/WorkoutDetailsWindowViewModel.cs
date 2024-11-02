using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.ViewModel
{
    internal class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        WorkoutDetailsWindow workoutDetailsWindow;

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

        private List<string> typesList;
        public List<string> TypesList
        {
            get { return typesList; }
            set
            {
                typesList = value;
                OnPropertyChanged(nameof(TypesList));
            }
        }

        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                    UpdateTypeInfo();
                }
            }
        }

        private string hoursInput;

        public string HoursInput
        {
            get { return hoursInput; }
            set
            {
                if (hoursInput != value)
                {
                    hoursInput = value;
                    OnPropertyChanged();
                }
            }
        }
        private string minutesInput;

        public string MinutesInput
        {
            get { return minutesInput; }
            set
            {
                if (minutesInput != value)
                {
                    minutesInput = value;
                    OnPropertyChanged();
                }
            }
        }
        private string secondsInput;
        public string SecondsInput
        {
            get { return secondsInput; }
            set
            {
                if (secondsInput != value)
                {
                    secondsInput = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeSpan selectedTimeSpan;
        public TimeSpan SelectedTimeSpan
        {
            get{ return selectedTimeSpan; }
            set
            {
               if (selectedTimeSpan != value)
               {
                    selectedTimeSpan = value;
                    OnPropertyChanged(nameof(SelectedTimeSpan));
               }
            }
        }
        
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged(nameof(NotesInput));
            }
        }

        private string typeInfo;
        public string TypeInfo
        {
            get
            {
                return typeInfo;
            }
            set
            {
                if (typeInfo != value)
                {
                    typeInfo = value;
                    OnPropertyChanged(nameof(TypeInfo));
                }
            }
        }

        private int typeInfoInput;
        public int TypeInfoInput
        {
            get { return typeInfoInput; }
            set
            {
                typeInfoInput = value;
                OnPropertyChanged(nameof(TypeInfoInput));
            }
        }

        public RelayCommand EditWorkoutCommand => new RelayCommand(_ => EditWorkout());
        public RelayCommand SaveWorkoutCommand => new RelayCommand(_ => SaveWorkout());
        public WorkoutDetailsWindowViewModel() {}
        public WorkoutDetailsWindowViewModel(Workout workout)
        {

            workoutDetailsWindow = Application.Current.Windows.OfType<WorkoutDetailsWindow>().First();
            SelectedWorkout = workout;
            WorkoutRepository workoutRepository = new WorkoutRepository();
            TypesList = workoutRepository.GetWorkoutTypes();
            
            //Assigns SelectedWorkout to each binding
            TypeInfoInput = SelectedWorkout.TypeInfoInput; 
            SelectedType = SelectedWorkout.Type;
            NotesInput = SelectedWorkout.Notes;
            SelectedDate = SelectedWorkout.Date;

            SecondsInput = SelectedWorkout.Duration.Seconds.ToString();
            MinutesInput = SelectedWorkout.Duration.Minutes.ToString();
            HoursInput = SelectedWorkout.Duration.Hours.ToString();
        }

        public void SaveWorkout()
        {
            SelectedWorkout.TypeInfoInput = TypeInfoInput;
            SelectedWorkout.Type = SelectedType;
            SelectedWorkout.Notes = NotesInput;
            SelectedWorkout.Date = SelectedDate;

            // Converts the Textbox into int to then make a timespan
            int.TryParse(HoursInput, out int hours);
            int.TryParse(MinutesInput, out int minutes);
            int.TryParse(SecondsInput, out int seconds);
            selectedWorkout.Duration = new TimeSpan(hours, minutes, seconds);

            WorkoutWindow workoutWindow = new WorkoutWindow();
            workoutWindow.Show();

            workoutDetailsWindow.Close();

        }
        public void EditWorkout()
        {
            workoutDetailsWindow.TypesListBox.IsEnabled = true;
            workoutDetailsWindow.TypeInfoBox.IsReadOnly = false;
            workoutDetailsWindow.HoursBox.IsReadOnly = false;
            workoutDetailsWindow.MinutesBox.IsReadOnly = false;
            workoutDetailsWindow.SecondsBox.IsReadOnly = false;
            workoutDetailsWindow.DateBox.IsEnabled = true;
            workoutDetailsWindow.NotesBox.IsReadOnly = false;
        }
        private void UpdateTypeInfo() // Chooses a Typeinfo depending on which type the workouot is
        {
            if (SelectedType == TypesList[0])
            {
                TypeInfo = "Distance";
            }
            else if (SelectedType == TypesList[1])
            {
                TypeInfo = "Repetitions";
            }
        }
    }
}
