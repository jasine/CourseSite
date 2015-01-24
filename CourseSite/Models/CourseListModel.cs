using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CourseSite.Models
{

    [DataContract(Name = "CourseInfoList")]
    public class CourseListModel
    {
        //学期开始时间，修改这里变换学期以自动返回周数
        public readonly DateTime TermStartTime = DateTime.Parse("2015-3-1");

        public int Id { get; set; }

        [DataMember(Name = "CourseInfos")]
        public List<CourseModel> CourseInfoList { get; set; }

        [DataMember(Name = "Name")]
        public String Name { get; set; }

        [DataMember(Name = "Organization")]
        public String Organization { get; set; }


        private int thisWeek;
        [DataMember (Name="ThisWeek")]
        public int ThisWeek
        {
            get
            {
                TimeSpan ts = DateTime.Now - TermStartTime;
	            if (ts.Hours < 0)
	            {
		            return 1;
	            }
	            else
	            {
		            return (ts.Days)/7+1;
	            }
            }
            set { thisWeek = value; }
        }
    }
}