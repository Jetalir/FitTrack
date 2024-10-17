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
        DateTime Date {  get; set; }
        string Type { get; set; }
        TimeSpan Duration { get; set; }
        int CaloriesBurned { get; set; }
        string Notes { get; set; }
        //Methods
        public abstract int CalculateCaloriesBurned();


    }
}
