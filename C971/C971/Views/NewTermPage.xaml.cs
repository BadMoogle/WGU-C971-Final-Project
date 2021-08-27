using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class NewTermPage : ContentPage
    {
        public SchoolTerm Semester { get; set; }

        public NewTermPage()
        {
            InitializeComponent();

            Semester = new SchoolTerm
            {
                TermName = "New Term",
                Id = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6)
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddTerm", Semester);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}