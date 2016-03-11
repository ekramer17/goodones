using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Entities
{
    [Table("Event")]
    public class Event
    {
        public int ID { get; set; }

        [StringLength(128, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("^20(1[6-9]|[2-6][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Date must be valid and in the form yyyy-mm-dd")]
        public String Date { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Line1 { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Line2 { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Column(TypeName="money")]
        [Range(typeof(decimal), "0.0", "100000000", ErrorMessage = "Price must be a positive decimal number")]
        public decimal Price { get; set; }

        public int MaxCapacity { get; set; }

        public int MinCapacity { get; set; }
    }
}