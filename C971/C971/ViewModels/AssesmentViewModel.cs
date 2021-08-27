using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using C971.Models;
using C971.Views;

namespace C971.ViewModels
{
    public class AssessmentViewModel : BaseViewModel
    {
        public ObservableCollection<Assessment> Assessments { get; set; } //For getting a list of all assessments
        public ObservableCollection<SchoolCourse> Courses { get; set; }
        public Assessment Test { get; set; } //Used by the assessment detail view of an individual assessment
        public Command LoadItemsCommand { get; set; }

        //Default constructor
        public AssessmentViewModel()
        {
            Title = "Assessment";
            Assessments = new ObservableCollection<Assessment>();
            Courses = new ObservableCollection<SchoolCourse>();
            Test = new Assessment();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewAssessmentPage, Assessment>(this, "AddAssessment", async (obj, item) =>
            {
                var newItem = item as Assessment;
                Assessments.Add(newItem);
                await DataStore.AddAssessmentAsync(newItem);
            });

            MessagingCenter.Subscribe<AssessmentDetailPage, Assessment>(this, "DeleteAssessment", async (obj, item) =>
            {
                var newItem = item as Assessment;
                Assessments.Remove(newItem);
                await DataStore.DeleteAssessmentAsync(newItem.Id);
            });
        }

        public AssessmentViewModel(Assessment assessment) : this()
        {
            Title = assessment.Name;
            Test = assessment;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Assessments.Clear();
                var tmpAssessments = await DataStore.GetAssessmentsAsync(true);
                foreach (var item in tmpAssessments)
                {
                    Assessments.Add(item);
                }
                Courses.Clear();
                var tmpCourses = await DataStore.GetCoursesAsync(true);
                foreach (var item in tmpCourses)
                {
                    Courses.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}