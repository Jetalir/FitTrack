using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitTrack.Model
{
    class WorkoutRepository
    {
        static ObservableCollection<Workout> WorkoutList = new ObservableCollection<Workout>()
        {
            new CardioWorkout ( "Cardio", 400, new TimeSpan(0, 30, 0), DateTime.Now, "Morning run" , null ,20),
            new StrengthWorkout ("Strength",  400, new TimeSpan(0, 10, 0), DateTime.Now, "Pushups", null, 10 )
        };

        public void AddCardioWorkout(string type, int caloriesBurned, TimeSpan duration, DateTime date, string notes, Guid? guid, int distance)
        {
            WorkoutList.Add(new CardioWorkout(type, caloriesBurned, duration, date, notes, guid, distance));
        }
        public void AddStrengthWorkout(string type, int caloriesBurned, TimeSpan duration, DateTime date, string notes, Guid? guid, int repititions) 
        {
            WorkoutList.Add(new StrengthWorkout(type, caloriesBurned, duration, date, notes, guid, repititions));
        }
        public void DeleteWorkout(Workout workout)
        {

            WorkoutList.Remove(workout);
        }

        public ObservableCollection<Workout> GetWorkoutList()
        {
            return WorkoutList;
        }


    }
}
