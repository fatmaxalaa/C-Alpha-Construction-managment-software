using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Models
{
    public class Resource
    {
        public string Title { get; set; }
        public int ResourceId { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public ResourceType ResourceType { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public Material Material { get; set; }
        public double CostPerDay { get; set; }
        public int Duration { get; set; }
        public double TotalCost { get; set; }
        public string TaskName { get; set; }
        public double UnitsPerDay { get; set; }

        public List<Task> Tasks { get; set; }
    }

    public enum ResourceType
    {
        Labor, Non_Labor, Material
    }
    public enum Material
    {
        None, Tons, LumpSum, Pounds, Cubic_yards, Linear_feet
    }
}
