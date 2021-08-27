using System;
using System.Collections.Generic;
using System.Text;

namespace C971.Models
{
    public enum MenuItemType
    {
        TermList,
        CourseList,
        AssessmentList,
        InstructorList
    }
    //Used to create the menu bar of the app
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
