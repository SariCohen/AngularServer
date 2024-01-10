using AngularServer1.Modal;

namespace AngularServer1.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
        public Donor Donor { get; set; }
        public present Present { get; set; }

    }
}
