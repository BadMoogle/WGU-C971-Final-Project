using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;
using C971.ViewModels;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class TermDetailPage : ContentPage
    {
        CourseDetailViewModel viewModel;

        public TermDetailPage(CourseDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public TermDetailPage()
        {
            InitializeComponent();

            var item = new SchoolCourse
            {
               CourseName = "C971",
               CourseDescription = "A course that makes me hate mobile devices."
            };

            viewModel = new CourseDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}