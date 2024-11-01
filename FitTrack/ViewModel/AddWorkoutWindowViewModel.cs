using FitTrack.Model;
using FitTrack.MVVM;
using FitTrack.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.ViewModel
{
    internal class AddWorkoutWindowViewModel : ViewModelBase
    {
        //Variables
        private AddWorkoutWindow addWorkoutWindow = Application.Current.Windows.OfType<AddWorkoutWindow>().First(); // Assign the running AddWorkoutWindow to a variable
        private WorkoutRepository workoutRepository {  get; set; }
        User? ActiveUser { get; set; }

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

        private string hoursInput = "0";

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
        private string minutesInput = "0";

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
        private string secondsInput = "0";

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
            get { return selectedTimeSpan; }
            set
            {
                if (selectedTimeSpan != value)
                {
                    selectedTimeSpan = value;
                    OnPropertyChanged(nameof(SelectedTimeSpan));
                }
            }
        }

        private DateTime selectedDate = DateTime.Now;

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
            { return typeInfo; 
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
        public RelayCommand AddWorkoutCommand => new RelayCommand(_ => AddWorkout());
        //Constructor
        public AddWorkoutWindowViewModel()
        {
            UserRepository user = new UserRepository();
            ActiveUser = user.GetSignedInUser();

            workoutRepository = new WorkoutRepository();
            TypesList = workoutRepository.GetWorkoutTypes();
        }

        //Functions

        public void AddWorkout()
        {
            
            UserRepository userRepository = new UserRepository();
            addWorkoutWindow.AddWorkoutMessage.Visibility = Visibility.Visible;
            if (SelectedType == null) // Om Man inte valt någon type
            {
                SetAddWorkoutMessage("Please select a type");
            }
            else if (TypeInfoInput <= 0) // inte angett ett värde som inte är default
            {
                SetAddWorkoutMessage($"Please enter a value above 0 on {TypeInfo}");
            }
            else if (int.TryParse(HoursInput, out int hours) && int.TryParse(MinutesInput, out int minutes) && int.TryParse(SecondsInput, out int seconds))
            {
                SelectedTimeSpan = TimeSpan.Zero;
                SelectedTimeSpan = new TimeSpan(hours, minutes, seconds);

                Workout strengthWorkout = new StrengthWorkout();
                Workout cardioWorkout = new CardioWorkout();
                //Creates a workout depending on which type is selected
                if (SelectedType == TypesList[0]) 
                {
                    workoutRepository.AddCardioWorkout(
                        SelectedType, SelectedTimeSpan, SelectedDate, NotesInput, ActiveUser.UserId, TypeInfoInput);
                }
                else if (SelectedType == TypesList[1])
                {
                    workoutRepository.AddStrengthWorkout( 
                        SelectedType, SelectedTimeSpan, SelectedDate, NotesInput, ActiveUser.UserId, TypeInfoInput);

                }
                WorkoutWindow workoutWindow = new WorkoutWindow();
                workoutWindow.Show();
                addWorkoutWindow.Close();
            }
            else
            {
                SetAddWorkoutMessage("Please enter valid values for hours, minutes, and seconds.");
            }
        }

        public void SetAddWorkoutMessage(string error)
        {
            addWorkoutWindow.AddWorkoutMessage.Text = error;
        }
        private void UpdateTypeInfo()
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
