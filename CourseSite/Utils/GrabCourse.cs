using CourseSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace CourseSite.Utils
{
    public class GrabCourse
    {
        public static UserModel GetUserInfo(string username,string password)
        {
            UserModel user = new UserModel();
            //if(cookieStore==null)
            //{
            //    cookieStore = cookieStore == null ? new CookieContainer() : cookieStore;
            //}
            var loginInfoLines = Login(username, password).Split('\n');
            for (int i = 0; i < loginInfoLines.Length; i++)
            {
                if (loginInfoLines[i].Contains("当前用户所在单位"))
                {
                    String str = loginInfoLines[i + 3].Trim();
                    user.Organization = str.Substring(2, str.Length - 2);
                    user.Name = loginInfoLines[i + 6].Trim();
                    user.Username = username;
                    user.Password = password;
                    return user;
                }
            }
            return null;
        }

        public static string Login(string username,string password,CookieContainer cookieStore=null)
        {
            string postStr = "userName=" + username + "&pwd=" + password + "&sb=sb";
            return HttpRequest.Post("http://sep.ucas.ac.cn/slogin", postStr, cookieStore, Encoding.UTF8);

        }

        public static List<CourseDbModel> GetCourseList(String userName, String passWord)
        {
            var courseList = new List<CourseDbModel>();

            CookieContainer cookieStore = new CookieContainer();

            Login(userName,passWord,cookieStore);

            string urlRedirect = "http://sep.ucas.ac.cn/portal/site/226/821";
            string respStr = HttpRequest.Get(urlRedirect, "", cookieStore, Encoding.UTF8);
            string[] temp = respStr.Split('\n');
            string queryPath = "";
            foreach (String line in temp)
            {
                if (line.Contains("url="))
                {
                    int firstPos = line.IndexOf("url=") + 4;
                    int lastPos = line.LastIndexOf(">") - 1;
                    queryPath = line.Substring(firstPos, lastPos - firstPos);
                    break;
                }
            }
            HttpRequest.Get(queryPath, "", cookieStore, Encoding.UTF8);
            string respList = HttpRequest.Get("http://jwjz.ucas.ac.cn/Student/DeskTopModules/Course/CourseManage.aspx", "", cookieStore, Encoding.UTF8);
            string[] respLines = respList.Split('\n');
            foreach (string line in respLines)
            {
                if (line.Contains("CourseTimeMsg.aspx?CourseID="))
                {
                    int firstPos = line.IndexOf("CourseTimeMsg.aspx?CourseID=") + 28;
                    int lastPos = line.LastIndexOf("target=") - 2;
                    string courseNum = line.Substring(firstPos, lastPos - firstPos);
                    CourseDbModel course = new CourseDbModel();

                    string[] courseListResp = HttpRequest.Get("http://jwjz.ucas.ac.cn/Student/DeskTopModules/Course/CourseTimeMsg.aspx?CourseID=" + courseNum,
                        "", cookieStore, Encoding.UTF8).Split('\n');
                    int j = -1;

                    var courseDetial = new CourseDetialDbModel();

                    foreach (string str in courseListResp)
                    {
                        if (str.Contains("#990000"))
                        {
                            int removePos = str.IndexOf("#990000");
                            String str2 = str.Substring(removePos, str.Length - removePos);
                            int firstPos2 = str2.IndexOf(">") + 1;
                            int lastPos2 = str2.IndexOf("<");
                            String content = str2.Substring(firstPos2, lastPos2 - firstPos2);


                            if (j == -1)
                            {
                                course.Name = content;
                                course.Num = courseNum;
                                course.Teacher = GetCourseTeacher(courseNum, cookieStore);
                                course.Detials = new List<CourseDetialDbModel>();
                            }
                            else if (j == 0)
                            {
                                courseDetial = new CourseDetialDbModel();
                                String[] grp = content.Split('：');
                                courseDetial.Week = CourseModel.GetWeekIndex(grp[0].Trim());
                                String[] temp2 = (grp[1].Trim().Split('、'));
                                courseDetial.Time = temp2[0].Substring(1, temp2[0].Length - 1) + "-" + temp2[temp2.Length - 1].Substring(0, temp2[temp2.Length - 1].Length - 1);
                            }
                            else if (j == 1)
                            {
                                courseDetial.Place = content;
                            }
                            else
                            {
                                //String[] weekTemp = content.Split('、');
                                //List<Int32> weekList = new List<Int32>();
                                //for (int m = 0; m < weekTemp.Length; m++)
                                //{
                                //    weekList.Add(Int32.Parse(weekTemp[m]));
                                //}
                                //courseModel.WeekNum = weekList;
                                courseDetial.WeekNum = content;
                                course.Detials.Add(courseDetial);
                                
                            }
                            j = (++j) % 3;

                        }
                    }
                    if (course.Detials.Count != 0)
                    {
                        //人文讲座 已经过了的英语 等课程不算入课程表
                        courseList.Add(course);
                    }

                    //string respJson=HttpRequest.Post("http://www.ishangke.net/getcourse.php", "cfgid=" + courseNum,cookieStore, Encoding.UTF8);
                    //var json = Json.Decode(respJson);
                }
            }
            return courseList;


        }

        private static string GetCourseTeacher(string courseNum, CookieContainer cookie)
        {
            string[] teacherResp = HttpRequest.Get("http://jwjz.gucas.ac.cn/coursePlan/DG_prt.asp?cfgID=" + courseNum, "",
                cookie, Encoding.GetEncoding("gb2312")).Split('\n');
            foreach (string line in teacherResp)
            {
                if (line.Contains("主讲教师"))
                {
                    int removePos = line.IndexOf("主讲教师");
                    string line2 = line.Substring(removePos, line.Length - removePos);
                    int firstPos = line2.IndexOf(">") + 1;
                    line2 = line2.Substring(firstPos, line2.Length - firstPos).Trim();
                    return line2;
                }
            }
            return "";
        }
    }
}