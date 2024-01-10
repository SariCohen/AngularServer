using AngularServer1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AngularServer1.Modal
{
    public class present
    {
        public int PresentId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Price { get; set; } = 10;
        public string ImagUrl { get; set; }
 
        public bool? isWinner { get; set; } = false;

        [NotMapped]
        public List<string> Donors { get; set; }
    }

}
