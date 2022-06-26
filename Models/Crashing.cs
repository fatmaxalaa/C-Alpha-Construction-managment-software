using System.Collections.Generic;

namespace Resources.Models
{
    public class Crashing
    {
        public int Id { get; set; }
        public double OptimisticDuration { get; set; }
        public double MostLikelyDuration { get; set; }
        public double PessimesticDuration { get; set; }
       
        public double ExpectedTime { get; set; }
          
        public double Te { get; set; }
        
        public double Segma { get; set; }
        
        public double TotalSegma { get; set; }
        
        public double RequiredTime { get; set; }
        
        public double Probability { get; set; }

        public List<Task> CriticalActivities { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}
