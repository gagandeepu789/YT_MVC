using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT_MVC.Domain.Entities
{
    public class Villa {
         public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name="price per night")]
        [Range(10,10000)]
        public double? Price { get; set; }
        public  int Sqft { get; set; }
        [Range(1,10)]
        public int Occupancy {  get; set; }
        [NotMapped]//means telling ef core that no need to add this to table
        public IFormFile? Image { get; set; }
        [Display(Name="image Url")]
        public string? ImageUrl {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
