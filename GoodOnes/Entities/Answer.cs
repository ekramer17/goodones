using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Entities
{
    public class Answer
    {
        [Key, Column("Person", Order=1)]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        [Key, Column("Question", Order=2)]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }

        public string Text { get; set; }
    }
}