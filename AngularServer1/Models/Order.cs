using AngularServer1.Modal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularServer1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public present Present { get; set; }
        public int presentId { get; set; }
        public User user { get; set; }  
        public string userId { get; set; }
        public string status { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

    }
}

