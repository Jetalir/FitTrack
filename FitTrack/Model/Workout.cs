using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public abstract class Workout
    {
        //Variables
        public string Type { get; set; }
        public int CaloriesBurned { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date {  get; set; }
        public string Notes { get; set; }

        //Constructor
        protected Workout()
        {
            
        }
        public Workout(string type, int caloriesBurned, TimeSpan duration, DateTime date, string notes)
        {
            Type = type;
            CaloriesBurned = caloriesBurned;
            Duration = duration;
            Date = date;
            Notes = notes;
        }

        //Methods

        public abstract int CalculateCaloriesBurned(); // Calculate amounts of calories burned


    }
}
