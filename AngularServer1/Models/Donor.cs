using AngularServer1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularServer1.Modal
{
    public class Donor
    {
        public string ImageUrl { get; set; }
        public string DonorId { get; set; }
        public string DonorName { get; set; }
        public string DonorPhone { get; set; }
        public string DonorEmail { get; set; }
        public string DonationType { get; set; }

        [NotMapped]
        public List<string> Donations { get; set; }
    }
}
