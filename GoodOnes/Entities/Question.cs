using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Entities
{
    /// <summary>
    /// Survey question
    /// </summary>
    [Table("Question")]
    public class Question
    {
        public int ID { get; set; }

        public bool Live { get; set; }

        [Required]
        public string Text { get; set; }

        public int Sequence { get; set; }

        [StringLength(64)]
        [RegularExpression("^(choice|textbox|dropdown)$", ErrorMessage = "Optional control type field must be either choice, textbox, dropdown")]
        public string ControlType { get; set; }

        /// <summary>
        /// Possible answers, if any, separated by pipe
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string PossibleAnswers { get; set; }

        public bool CanBeBlank { get; set; }
    }
}