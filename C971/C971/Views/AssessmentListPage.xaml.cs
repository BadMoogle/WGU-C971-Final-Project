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
    public partial class AssessmentListPage : ContentPage
    {
        AssessmentViewModel viewModel;

        public AssessmentListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AssessmentViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Assessment)layout.BindingContext;
            await Navigation.PushAsync(new AssessmentDetailPage(new AssessmentViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewAssessmentPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Assessments.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}