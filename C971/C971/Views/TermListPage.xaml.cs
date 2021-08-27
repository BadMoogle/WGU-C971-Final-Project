using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
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
    public partial class TermListPage : ContentPage
    {
        SchoolTermViewModel viewModel;

        public TermListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SchoolTermViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (SchoolTerm)layout.BindingContext;
            await Navigation.PushAsync(new CourseListPage(item));
            
            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewTermPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Terms.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}