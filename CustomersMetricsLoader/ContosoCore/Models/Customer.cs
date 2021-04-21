using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoCore.Models
{
    [Table("Customers")]
    public partial class Customer
    {
        [Key]
        public int RowId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string representative { get; set; }
        public string representative_email { get; set; }
        public string representative_phone { get; set; }
    }
}