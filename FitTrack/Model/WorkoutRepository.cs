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


        static List<string> Types = new List<string>()
        {
            "Cardio", 
            "Strength"
        };

        static ObservableCollection<Workout> WorkoutList = new ObservableCollection<Workout>()
        {
            new CardioWorkout ( Types[0], new TimeSpan(0, 30, 0), DateTime.Now, "Morning run" , null ,20),
            new StrengthWorkout (Types[1], new TimeSpan(0, 10, 0), DateTime.Now, "Pushups", null, 10 )
        };

        public void AddCardioWorkout(string type, TimeSpan duration, DateTime date, string notes, Guid? guid, int distance)
        {
            CardioWorkout cardioWorkout = new CardioWorkout(type, duration, date, notes, guid, distance);
            WorkoutList.Add(cardioWorkout);
        }
        public void AddStrengthWorkout(string type, TimeSpan duration, DateTime date, string notes, Guid? guid, int repititions) 
        {
            StrengthWorkout strengthWorkout = new StrengthWorkout(type, duration, date, notes, guid, repititions);
            WorkoutList.Add(strengthWorkout);
        }
        public void DeleteWorkout(Workout workout)
        {

            WorkoutList.Remove(workout);
        }

        public ObservableCollection<Workout> GetWorkoutList()
        {
            return WorkoutList;
        }

        public List<string> GetWorkoutTypes()
        {
            return Types;
        }

    }
}
