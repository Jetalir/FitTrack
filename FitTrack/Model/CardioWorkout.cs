using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class CardioWorkout : Workout
    {

        public int Distance {  get; set; }

        public CardioWorkout()
        {
            
        }
        public CardioWorkout(string type, TimeSpan duration, DateTime date, string notes, Guid? guid, int distance) : base(type, duration, date, notes, guid) 
        {
            Distance = distance;
            CaloriesBurned = CalculateCaloriesBurned();
        }
        public override int CalculateCaloriesBurned() // Fuskar lite men ändrar ifall jag har tid
        {
            return Distance * Random.Shared.Next(7, 12);
        }


        public override int TypeInfoInput
        {
            get { return Distance; }
            set
            {
                Distance = value;
            }
        }

    }
}
