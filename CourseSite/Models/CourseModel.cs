using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CourseSite.Models
{
    [DataContract(Name = "CourseInfo")]
    public class CourseModel
    {
        [DataMember(Name ="Id")]
        public int Index { get; set; }

        public int Id { get; set; }

        [DataMember]
        public String Num { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember(IsRequired = false)]
        public String Teacher { get; set; }

        [DataMember]
        public String Time { get; set; }

        [DataMember]
        public int Week { get; set; }

        [DataMember]
        public String Place { get; set; }

        [DataMember]
        public String WeekNum { get; set; }

        public static int GetWeekIndex(String week)
        {
            switch (week)
            {
                case "星期一":
                    return 1;
                case "星期二":
                    return 2;
                case "星期三":
                    return 3;
                case "星期四":
                    return 4;
                case "星期五":
                    return 5;
                case "星期六":
                    return 6;
                case "星期日":
                    return 7;
                case "星期天":
                    return 7;
                default:
                    return 0;

            }
        }

    }
}