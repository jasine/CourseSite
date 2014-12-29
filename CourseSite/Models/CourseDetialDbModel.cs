using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseSite.Models
{
    public class CourseDetialDbModel
    {
        public int Id { get; set; }

        public String Time { get; set; }

        public int Week { get; set; }

        public String Place { get; set; }

        public String WeekNum { get; set; }
    }
}