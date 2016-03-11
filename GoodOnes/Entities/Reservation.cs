using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        [Column("Event")]
        public int EventID { get; set; }
        public virtual Event Event { get; set; }

        [Column("Person")]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

    }
}