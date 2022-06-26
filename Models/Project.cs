using System;
using System.Collections.Generic;

namespace Resources.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string ProjectTemplate { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }

        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }

        public int ProjectDuration { get; set; }
        public List<Task> tasksrelated { get; set; }
    }
}
