using System;
using System.Collections.Generic;

namespace C971.Models
{
    public class SchoolTerm
    {
        public string Id { get; set; }
        public String TermName { get; set; }
        public LinkedList<SchoolCourse> SchoolCourses;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32 NotificationID { get; set; }

        public SchoolTerm()
        {
            SchoolCourses = new LinkedList<SchoolCourse>();
        }
    }
}
