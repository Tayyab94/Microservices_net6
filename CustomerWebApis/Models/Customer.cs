using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerWebApis.Models
{
    [Table("Customers",Schema ="dbo")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int customerId { get; set; }
        [Column("customer_Name")]

        public string customerName { get; set; }
        [Column("phone_Number")]

        public string mobileNumber { get; set; }
        [Column("email")]

        public string email { get; set; }
    }
}
