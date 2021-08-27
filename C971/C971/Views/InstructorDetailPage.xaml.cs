using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

using C971.Models;
using C971.ViewModels;
using System.Diagnostics;

namespace C971.Views
{
    [DesignTimeVisible(false)]
    public partial class InstructorDetailPage : ContentPage
    {
        InstructorViewModel viewModel;

        public InstructorDetailPage(Instructor teacher)
        {
            InitializeComponent();
            viewModel = new InstructorViewModel(teacher);
            BindingContext = viewModel;
        }

        public InstructorDetailPage(InstructorViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void EditInstructor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewInstructorPage(viewModel.Teacher)));
            await Navigation.PopAsync();
        }

        async void DeleteInsctructor_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete?", "Are you sure you want to delete this", "Yes", "No");
            if(answer)
            {
                MessagingCenter.Send(this, "DeleteInstructor", viewModel.Teacher);
                await Navigation.PopAsync();
            }
        }

    }
}