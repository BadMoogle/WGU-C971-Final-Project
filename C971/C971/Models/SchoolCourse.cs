using System;

namespace C971.Models
{
    [Flags]
    public enum CourseStatusFlags
    {
        Course_None,
        Course_In_Progress,
        Course_Completed,
        Course_Dropped,
        Course_Plan_To_Take
    }

    public class SchoolCourse
    {
        public string Id { get; set; }
        public string AssociatedTermId { get; set; }
        public Int32 NotificationID { get; set; }
        public String CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Instructor Instructor { get; set; }
        public CourseStatusFlags CourseStatus { get; set; }
        public Assessment ObjectiveAssessment { get; set; }
        public Assessment PerformanceAsssessment { get; set; }
        public String CourseDescription { get; set; }
        public String OptionalNotes { get; set; }
    }
}
