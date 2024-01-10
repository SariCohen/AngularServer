using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AngularServer1.Modal
{
    public class User
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }

        public string UserName { get; set; }

        [PasswordPropertyText]
        public string UserPassword { get; set; }

        [StringLength(30)]
        public string UserFirstName { get; set; }
        [StringLength(30)]
        public string UserLastName { get; set; }

        [Phone]
        public string UserPhone { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        
        public string UserCountry { get; set; }
        public string UserCity { get; set; }
        
        public int ? PostalCode { get; set; }=null;
        public string? role { get; set; } = null!;
    }
}
