using System;
using System.Text.Encodings.Web;

namespace Resources.Models
{
    public class WebApiTask
    {

        public int id { get; set; }
        public string? Text { get; set; }
        public string? start_date { get; set; }
        public int duration { get; set; }
        public decimal progress { get; set; }
        public int? parent { get; set; }

        public string? target { get; set; }
        public string? type { get; set; }

        public bool open
        {
            get { return true; }
            set { }
        }

        public static explicit operator WebApiTask(Task task)
        {
            return new WebApiTask
            {
                id = task.Id,
                Text = HtmlEncoder.Default.Encode(task.Text != null ? task.Text : ""),

                start_date = task.StartDate.ToString("yyyy-MM-dd HH:mm"),
                duration = task.Duration,
                parent = task.ParentId,
                type = task.Type,
                progress = task.Progress

            };
        }

        public static explicit operator Task(WebApiTask task)
        {
            return new Task
            {
                Id = task.id,
                Text = task.Text,
                StartDate = task.start_date != null ? DateTime.Parse(task.start_date,
                System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
                Duration = task.duration,
                ParentId = task.parent,
                Type = task.type,
                Progress = task.progress

            };
        }
    }
}
