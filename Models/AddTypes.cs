using System;
using System.ComponentModel.DataAnnotations;

namespace Resources.Models
{
    public class AddTypes
    {
            [Key]
        public int Id { set; get; }
        public string TaskName { set; get; }
        public string Type { set; get; }
        public DateTime StartTaskDate { set; get; }

    }
}
