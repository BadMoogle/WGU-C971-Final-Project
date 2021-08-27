using System;

namespace C971.Models
{
    [Flags]
    public enum AssessmentFlags
    {
        Assessment_None,
        Assessment_Performance,
        Assessment_Objective,
    }

    public class Assessment
    {
        public string Id { get; set; }  //Unique ID for this Assessment
        public string AssociatedCourseId { get; set; } //Associated Course ID
        public SchoolCourse AssociatedCourse { get; set; } //Associated Course
        public int NotificationID { get; set; } //Notifications for this Assessement
        public String Name { get; set; } //Name of the Assessment
        public String Description { get; set; } //Description of the Asseessment
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AssessmentFlags AssessmentType { get; set; }
    }
}
