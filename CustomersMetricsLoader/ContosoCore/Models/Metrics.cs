using System.ComponentModel.DataAnnotations;

namespace ContosoCore.Models
{
    public partial class Metrics
    {
        [Key]
        public int RowId { get; set; }
        public int customer_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string expression { get; set; }
    }
}
