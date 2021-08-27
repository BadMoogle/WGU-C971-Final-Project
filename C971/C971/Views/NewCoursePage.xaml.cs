using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;
using C971.ViewModels;
using System.Linq;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class NewCoursePage : ContentPage
    {
        public CourseDetailViewModel viewModel;
        bool IsNewCourse { get; set; }
        public SchoolCourse NewCourse { get; set; } //The current course bwing worked with.  Should probably refactor this to the viewmodel


        public NewCoursePage()
        {
            InitializeComponent();

            viewModel = new CourseDetailViewModel();
            viewModel.LoadItemsCommand.Execute(this);  //force the viewmodel to populate.
            BindingContext = this;  //A bit sloppy.  Experimenting with hybrid binding between the model and content page.

            NewCourse = new SchoolCourse()
            {
                CourseName = "New Course",
                CourseDescription = "Course Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6)
            };
            cmbbxInstructor.BindingContext = viewModel;
            cmbbxTerm.BindingContext = viewModel;
            IsNewCourse = true;


        }

        //Constructor for if we're editing an existing course.  Also a place for refactoring into the default constructor.
        public NewCoursePage(SchoolCourse course)
        {
            InitializeComponent();

            viewModel = new CourseDetailViewModel();
            viewModel.LoadItemsCommand.Execute(this);  //force the viewmodel to populate.
            BindingContext = this;  //A bit sloppy.  Experimenting with hybrid binding between the model and content page.
            IsNewCourse = false;

            NewCourse = course;
            cmbbxInstructor.BindingContext = viewModel;
            cmbbxTerm.BindingContext = viewModel;

            txtbxName.Text = course.CourseName;
            txtbxDescription.Text = NewCourse.CourseDescription;
            dateStart.Date = NewCourse.StartDate;
            dateEnd.Date = NewCourse.EndDate;
            cmbbxInstructor.SelectedItem = NewCourse.Instructor;
            var tmpTerm = viewModel.Terms.Where((SchoolTerm arg) => arg.Id == course.AssociatedTermId).FirstOrDefault();
            cmbbxTerm.SelectedItem = tmpTerm;
            switch (NewCourse.CourseStatus)
            {
                case CourseStatusFlags.Course_In_Progress:
                    cmbbxStatus.SelectedItem = "In Progress";
                    break;
                case CourseStatusFlags.Course_Completed:
                    cmbbxStatus.SelectedItem = "Completed";
                    break;
                case CourseStatusFlags.Course_Dropped:
                    cmbbxStatus.SelectedItem = "Dropped";
                    break;
                case CourseStatusFlags.Course_Plan_To_Take:
                    cmbbxStatus.SelectedItem = "Plan To Take";
                    break;
            }

        }

        //Save the new/updated course.
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (cmbbxInstructor.SelectedItem != null && cmbbxTerm.SelectedItem != null && cmbbxStatus.SelectedItem != null) //sanity check that they have the term and instructor set
            {
                //Grab the information from all the fields.
                NewCourse.CourseName = txtbxName.Text;
                NewCourse.CourseDescription = txtbxDescription.Text;
                NewCourse.StartDate = dateStart.Date;
                NewCourse.EndDate = dateEnd.Date;
                NewCourse.Instructor = (Instructor)cmbbxInstructor.SelectedItem;
                SchoolTerm tmpTerm = (SchoolTerm)cmbbxTerm.SelectedItem;
                NewCourse.AssociatedTermId = tmpTerm.Id;
                switch(cmbbxStatus.SelectedItem.ToString())
                {
                    case "In Progress":
                        NewCourse.CourseStatus = CourseStatusFlags.Course_In_Progress;
                        break;
                    case "Completed":
                        NewCourse.CourseStatus = CourseStatusFlags.Course_Completed;
                        break;
                    case "Dropped":
                        NewCourse.CourseStatus = CourseStatusFlags.Course_Dropped;
                        break;
                    case "Plan To Take":
                        NewCourse.CourseStatus = CourseStatusFlags.Course_Plan_To_Take;
                        break;
                }
                if(IsNewCourse)
                {
                    NewCourse.Id = Guid.NewGuid().ToString();
                    MessagingCenter.Send(this, "AddCourse", NewCourse);
                }
                else
                {
                    MessagingCenter.Send(this, "UpdateCourse", NewCourse);
                }    
                
                await Navigation.PopModalAsync();
            }
            else //Display an alert if they don't at least have the dropdowns.
            {
                await DisplayAlert("Alert", "Please ensure all fields are filled.", "Ok");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}