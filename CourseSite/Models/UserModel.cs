using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseSite.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public List<CourseModel> Courses { get; set; }
    }
}