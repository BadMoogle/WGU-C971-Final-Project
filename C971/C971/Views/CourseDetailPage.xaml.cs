using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

using C971.Models;
using C971.ViewModels;
using System.Diagnostics;
using Xamarin.Essentials;

namespace C971.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CourseDetailPage : ContentPage
    {
        CourseDetailViewModel viewModel;

        //Default constructor.  Would be a good candidate place for refactored code.
        protected CourseDetailPage()
        {
            InitializeComponent();
        }

        //Constructor for if a SchoolCourse is passed.  Can/should probably refactor this.
        public CourseDetailPage(SchoolCourse course) : this()
        {
            
            viewModel = new CourseDetailViewModel(course);
            BindingContext = viewModel;

            switch (course.CourseStatus)
            {
                case CourseStatusFlags.Course_In_Progress:
                    txtCourseStatus.Text = "In Progress";
                    break;
                case CourseStatusFlags.Course_Completed:
                    txtCourseStatus.Text = "Completed";
                    break;
                case CourseStatusFlags.Course_Dropped:
                    txtCourseStatus.Text = "Dropped";
                    break;
                case CourseStatusFlags.Course_Plan_To_Take:
                    txtCourseStatus.Text = "Plan To Take";
                    break;
            }
        }

        //Constructor for if a view model is passed.  Can/should probably refactor this.
        public CourseDetailPage(CourseDetailViewModel viewModel) : this()
        {
            BindingContext = this.viewModel = viewModel;
            switch (viewModel.Course.CourseStatus)
            {
                case CourseStatusFlags.Course_In_Progress:
                    txtCourseStatus.Text = "In Progress";
                    break;
                case CourseStatusFlags.Course_Completed:
                    txtCourseStatus.Text = "Completed";
                    break;
                case CourseStatusFlags.Course_Dropped:
                    txtCourseStatus.Text = "Dropped";
                    break;
                case CourseStatusFlags.Course_Plan_To_Take:
                    txtCourseStatus.Text = "Plan To Take";
                    break;
            }
        }

        //Edit course button clicked.  Display the form and send the course info to it.
        async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCoursePage(viewModel.Course)));
            await Navigation.PopAsync();
        }

        //Delete this course
        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete?", "Are you sure you want to delete this", "Yes", "No");
            if(answer)
            {
                MessagingCenter.Send(this, "DeleteCourse", viewModel.Course);
                await Navigation.PopAsync();
            }
        }

        //Display details if the user clicks on the assessment
        async void OnObjectiveAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AssessmentDetailPage(new AssessmentViewModel(viewModel.Course.ObjectiveAssessment))));
        }

        //Display details if the user clicks on the assessment
        async void OnPerformanceAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AssessmentDetailPage(new AssessmentViewModel(viewModel.Course.PerformanceAsssessment))));
        }

        //Send notes to "student"
        async void SendNotes_Clicked(object sender, EventArgs e)
        {
            try
            {
                var message = new SmsMessage(viewModel.Course.OptionalNotes, "Student");
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Alert", "SMS not supported on this device", "Ok");
            }
        }
    }
}