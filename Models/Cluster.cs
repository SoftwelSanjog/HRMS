namespace HRMS.Models
{
    public class Cluster:UserActivity
    {
        public int Id { get; set; }
        public string ClusterCode { get; set; }
        public string ClusterName { get; set; }
    }
}
