using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;
using C971.ViewModels;
using System.Linq;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class AssessmentDetailPage : ContentPage
    {
        AssessmentViewModel viewModel;

        public AssessmentDetailPage(AssessmentViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            viewModel.LoadItemsCommand.Execute(this); //force an update on the viewmodel to get the courses
            lblCourse.Text = viewModel.Courses.Where((SchoolCourse arg) => arg.Id == viewModel.Test.AssociatedCourseId).FirstOrDefault().CourseName;
            switch (viewModel.Test.AssessmentType)
            {
                case AssessmentFlags.Assessment_Objective:
                {
                        lblAssessmentType.Text = "Objective";
                        break;
                }
                case AssessmentFlags.Assessment_Performance:
                {
                        lblAssessmentType.Text = "Performance";
                        break;
                }
            }
        }

        //Default constructor
        public AssessmentDetailPage()
        {
            InitializeComponent();

            var item = new Assessment
            {
               Name = "Test",
               Description = "A generic test"
            };

            viewModel = new AssessmentViewModel(item);
            BindingContext = viewModel;
        }

        async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAssessmentPage(viewModel.Test)));
            await Navigation.PopAsync();
        }

        async void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete?", "Are you sure you want to delete this assessment", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "DeleteAssessment", viewModel.Test);
                await Navigation.PopAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}