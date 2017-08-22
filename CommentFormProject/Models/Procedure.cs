using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommentFormProject.Models
{
    public class Procedure
    {
        [Key]
        public int ProcedureID { get; set; }

        public int Priority { get; set; }
        public int Title { get; set; }
        public string Details { get; set; }

    }
}