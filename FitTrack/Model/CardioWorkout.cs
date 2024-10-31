using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class CardioWorkout : Workout
    {

        int distance {  get; set; }


        
        public CardioWorkout(string Type,  int CaloriesBurned, TimeSpan Duration, DateTime Date, string Notes, Guid? guid, int Distance) : base(Type, CaloriesBurned, Duration, Date, Notes, guid) 
        {
            distance = Distance;
        }
        public override int CalculateCaloriesBurned() // ??
        {
            return 0;
        }
    }
}
