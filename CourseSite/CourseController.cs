using CourseSite.Models;
using CourseSite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Web.Http;

namespace CourseSite
{
    public class CourseController : ApiController
    {
        // GET api/<controller>
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        public CourseListModel Get(string username, string password)
        {
            /*return new CourseListModel {CourseInfoList=new List<CourseModel> { new CourseModel{ Id = 1, Num = "1000", Name = "Data Mining",Place="教1-101",Teacher="刘莹",Time="1-2",Week=1,WeekNum=new List<int> {2,3,4,5,6 } } ,
                new CourseModel{  Id = 1, Num = "1001", Name = "政治",Place="教1-002",Time="1-2",Week=3,WeekNum=new List<int> {2,3,4,5,6,7,8 } }}
            };*/
            List<CourseDbModel> courseList;
            //using (var contex = new CourseDb())
            //{
            //    //contex.Users.c
            //    Console.WriteLine(contex.Users.Count());
            //    //if(contex.Users.Contains)
            //}
            try
            {
                var user = GrabCourse.GetUserInfo(username, password);
                using(var contex=new CourseDb())
                {
                    var users = from dbUser in contex.Users
                                where dbUser.Username == username && dbUser.Password == password
                                select new { Name = username,Password=password};
                    
                    if ( !users.Any())
                    {
                        courseList = GrabCourse.GetCourseList(username, password);
                        user.CoursesList = courseList;
                        contex.Users.Add(user);
                        contex.SaveChanges();
                        
                    }
                    else
                    {
                       // courseList = users.First().;
                    }
                }
                
                
            }
            catch (Exception)
            {
                
                return null;
                //throw;
            }
            return null;
        }

        public string Get(string s)
        {
            return s + "aaaa";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}