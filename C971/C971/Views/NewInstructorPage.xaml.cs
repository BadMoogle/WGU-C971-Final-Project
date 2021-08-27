using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class NewInstructorPage : ContentPage
    {
        public Instructor Teacher { get; set; }

        public bool IsNewInstructor { get; set; }

        //Default constructor
        public NewInstructorPage()
        {
            InitializeComponent();

            Teacher = new Instructor
            {
                Name = "New Insctructor",
                Id = Guid.NewGuid().ToString(),
            };

            IsNewInstructor = true;

            BindingContext = this;
        }

        public NewInstructorPage(Instructor instructor)
        {
            InitializeComponent();

            Teacher = instructor;
            IsNewInstructor = false;

            BindingContext = this;
        }


        //Saves the new/updated Intructor
        async void Save_Clicked(object sender, EventArgs e)
        {
            if(IsNewInstructor)
            {
                MessagingCenter.Send(this, "AddInstructor", Teacher);
            }
            else
            {
                MessagingCenter.Send(this, "UpdateInstructor", Teacher);
            }
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}