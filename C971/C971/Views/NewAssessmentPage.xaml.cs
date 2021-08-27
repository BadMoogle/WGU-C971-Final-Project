using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971.Models;
using C971.ViewModels;

namespace C971.Views
{

    [DesignTimeVisible(false)]
    public partial class NewAssessmentPage : ContentPage
    {
        public Assessment assessment { get; set; } //assessment to save the data to and pass to the datstore
        public AssessmentViewModel viewModel;
        public bool IsNewAssessment {get; set; }

        //default constructor
        public NewAssessmentPage()
        {
            InitializeComponent();
            assessment = new Assessment();
            viewModel = new AssessmentViewModel();
            viewModel.LoadItemsCommand.Execute(this); //force an update
            BindingContext = viewModel;
            IsNewAssessment = true;
        }

        public NewAssessmentPage(Assessment test) : this() //Call base constructor and fill out the form since this is an existing Assessment
        {
            assessment = test;
            this.Title = assessment.Name;
            BindingContext = viewModel;
            IsNewAssessment = false;
            txtbxName.Text = assessment.Name;
            txtbxDescription.Text = assessment.Description;
            dateStart.Date = assessment.StartDate;
            dateEnd.Date = assessment.EndDate;
            cmbbxCourse.SelectedItem = test.AssociatedCourse;
            switch (assessment.AssessmentType)
            {
                case AssessmentFlags.Assessment_Performance:
                    {
                        cmbbxAsssessmentType.SelectedItem = "Performance";
                        break;
                    }
                case AssessmentFlags.Assessment_Objective:
                    {
                        cmbbxAsssessmentType.SelectedItem = "Objective";
                        break;
                    }
            }
        }

        //Save the newly created assessment
        async void Save_Clicked(object sender, EventArgs e)
        {
            if(cmbbxAsssessmentType.SelectedItem != null && cmbbxCourse.SelectedItem != null) //Check that the user has selected the comboboxes
            {
                assessment.Name = txtbxName.Text;
                assessment.Description = txtbxDescription.Text;
                assessment.StartDate = dateStart.Date;
                assessment.EndDate = dateEnd.Date;
                assessment.AssessmentType = (AssessmentFlags)cmbbxAsssessmentType.SelectedIndex;
                switch (cmbbxAsssessmentType.SelectedItem.ToString())
                {
                    case "Performance":
                    {
                            assessment.AssessmentType = AssessmentFlags.Assessment_Performance;
                            break;
                    }
                    case "Objective":
                    {
                            assessment.AssessmentType = AssessmentFlags.Assessment_Objective;
                            break;
                    }
                }
                SchoolCourse tmpCourse = (SchoolCourse)cmbbxCourse.SelectedItem;
                assessment.AssociatedCourseId = tmpCourse.Id;
                if (IsNewAssessment)
                {
                    assessment.Id = Guid.NewGuid().ToString();
                    MessagingCenter.Send(this, "AddAssessment", assessment);
                }
                else
                {
                    MessagingCenter.Send(this, "UpdateAssessment", assessment);
                }    
                
                await Navigation.PopModalAsync();
            }    
            else 
            {
                await DisplayAlert("Alert", "Please ensure all fields are filled.", "Ok");

            }    
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}