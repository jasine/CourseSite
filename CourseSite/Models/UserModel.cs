﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseSite.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string CardNum { get; set; }

        public string Name { get; set; }

        public string Organization { get; set; }


        public List<CourseDbModel> CoursesList { get; set; }
    }
}