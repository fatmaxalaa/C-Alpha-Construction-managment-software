namespace Resources.Models
{
    public class Lag
    {
        public int Id { set; get; }
        public string SourceName { set; get; }
        public string TargetName { set; get; }
        public int LagValue { set; get; }
    }
}
