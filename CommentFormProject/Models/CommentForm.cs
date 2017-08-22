using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CommentFormProject.Models
{
    public class CommentForm
    {
        [Key]
        public int CommentID { get; set; }

        public string Name { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }

        [ForeignKey("Procedure")]
        public int ProcedureID { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}