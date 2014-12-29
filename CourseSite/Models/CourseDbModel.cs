using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseSite.Models
{
    public class CourseDbModel
    {
        [Key]
        public String Num { get; set; }

        public String Name { get; set; }

        public String Teacher { get; set; }

        public virtual List<CourseDetialDbModel> Detials { get; set; }

        public virtual List<UserModel> Students { get; set; }
    }
}