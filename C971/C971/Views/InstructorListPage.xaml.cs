using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class InstructorListPage : ContentPage
    {
        InstructorViewModel viewModel;

        public InstructorListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new InstructorViewModel();

            /*MessagingCenter.Subscribe<NewInstructorPage, Instructor>(this, "AddInstructor", (obj, item) =>
            {
                var newItem = item as Instructor;
                viewModel.Instructors.Add(newItem);
            });*/
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Instructor)layout.BindingContext;
            await Navigation.PushAsync(new InstructorDetailPage(new InstructorViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewInstructorPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Instructors.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}