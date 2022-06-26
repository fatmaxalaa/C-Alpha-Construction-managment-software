using System;
using System.Collections.Generic;

namespace Resources.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Progress { get; set; }
        public int? ParentId { get; set; }
        public string? Type { get; set; }
        public int SortOrder { get; set; }
        public List<Resource> resources { get; set; }
        public Project projectsrelated { get; set; }
        
        public Type type1 { get; set; }


        /// <summary>
        /// Addtion prop
        /// </summary>
        public int TotalFloat { get; set; }
        public DateTime LateStart { get; set; }
        public DateTime LateFinish { get; set; }

        public Criticality Criticality { get; set; }

    }


    public enum Criticality
    {
        Low,
        High
    }
    public enum Type
    {
        Task,
        Project,
        Milestone
    }
}
