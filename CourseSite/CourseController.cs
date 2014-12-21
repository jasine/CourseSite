using CourseSite.Models;
using CourseSite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CourseSite
{
    public class CourseController : ApiController
    {
        // GET api/<controller>
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        public CourseListModel Get(string username, string password)
        {
            /*return new CourseListModel {CourseInfoList=new List<CourseModel> { new CourseModel{ Id = 1, Num = "1000", Name = "Data Mining",Place="教1-101",Teacher="刘莹",Time="1-2",Week=1,WeekNum=new List<int> {2,3,4,5,6 } } ,
                new CourseModel{  Id = 1, Num = "1001", Name = "政治",Place="教1-002",Time="1-2",Week=3,WeekNum=new List<int> {2,3,4,5,6,7,8 } }}
            };*/
            
            using (var contex = new CourseDb())
            {
                //contex.Users.c
                Console.WriteLine(contex.Users.Count());
            }
            return GrabCourse.GetCourseList(username, password);
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