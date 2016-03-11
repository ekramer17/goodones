using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Entities
{
    [Table("Person")]
    public class Person
    {
        public int ID { get; set; }

        [StringLength(128, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(128, MinimumLength = 2)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(256, MinimumLength = 2)]
        public string Email { get; set; }

        [StringLength(8, MinimumLength = 1)]
        public string Gender { get; set; }

        [StringLength(64)]
        public string StripeID { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(32)]
        public string RelationshipStatus { get; set; }

        [StringLength(32)]
        public string HomeZipcode { get; set; }

        [StringLength(32)]
        public string WorkZipcode { get; set; }

        public byte[] Photo { get; set; }


        public virtual ICollection<Answer> Answers { get; set; }


        [Flags]
        public enum INCLUDES { NONE = 0, ANSWERS = 1 }
    }
}