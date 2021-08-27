using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;
using C971.Views;
using C971.ViewModels;

namespace C971.Views
{
    [DesignTimeVisible(false)]
    public partial class CourseListPage : ContentPage
    {
        SchoolCourseViewModel viewModel;

        public CourseListPage()
        {
            InitializeComponent();
            btnDeleteTerm.IsEnabled = false;
            BindingContext = viewModel = new SchoolCourseViewModel();
        }

        public CourseListPage(SchoolTerm term)
        {
            InitializeComponent();

            BindingContext = viewModel = new SchoolCourseViewModel(term);
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (SchoolCourse)layout.BindingContext;
            await Navigation.PushAsync(new CourseDetailPage(item));
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCoursePage()));
        }

        async void DeleteTerm_Clicked(object sender, EventArgs e)
        {

            if (viewModel.CurrentTerm != null)
            {
                bool answer = await DisplayAlert("Delete?", "Are you sure you want to delete this term?", "Yes", "No");
                if (answer)
                {
                    SchoolTerm tmpTerm = viewModel.CurrentTerm;
                    MessagingCenter.Send(this, "DeleteTerm", tmpTerm);
                    await Navigation.PopAsync();
                }

            }    

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Courses.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}