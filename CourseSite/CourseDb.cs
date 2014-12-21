using CourseSite.Models;
using System;
using System.Data.Entity;

namespace CourseSite
{
    internal class CourseDb :DbContext
    {
        public DbSet<UserModel> Users { get; set; }
    }
}