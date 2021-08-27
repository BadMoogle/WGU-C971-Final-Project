using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using C971.Models;
using C971.Views;
using System.Linq;

namespace C971.ViewModels
{
    public class CourseDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Instructor> Instructors { get; set; }
        public ObservableCollection<SchoolTerm> Terms { get; set; }
        public ObservableCollection<Assessment> Assessments { get; set; }

        public bool IsNewCourse { get; set; }
        public Command LoadItemsCommand { get; set; }
        public SchoolCourse Course { get; set; }

        //Default Constructor
        public CourseDetailViewModel()
        {
            Title = "New Course";
            Course = new SchoolCourse();
            Terms = new ObservableCollection<SchoolTerm>();
            Instructors = new ObservableCollection<Instructor>();
            Assessments = new ObservableCollection<Assessment>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<CourseDetailPage, SchoolCourse>(this, "DeleteCourse", async (obj, item) =>
            {
                var newItem = item as SchoolCourse;
                await DataStore.DeleteCourseAsync(newItem.Id);
            });
        }

        //Constructor with a provided SchoolCourse
        public CourseDetailViewModel(SchoolCourse item, bool isNew = true) : this()
        {
            Title = item?.CourseName;
            Course = item;
            IsNewCourse = isNew;
            this.LoadItemsCommand.Execute(this);
        }

        //Task to refresh the information available to the view
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Instructors.Clear();
                var teachers = await DataStore.GetInstructorsAsync(true);
                foreach (var item in teachers)
                {
                    Instructors.Add(item);
                }
                Terms.Clear();
                var listterms = await DataStore.GetTermsAsync(true);
                foreach (var item in listterms)
                {
                    Terms.Add(item);
                }
                var tmpAssessments = await DataStore.GetAssessmentsAsync(true);
                foreach (var item in tmpAssessments.Where((Assessment arg) => (arg.Id == Course.ObjectiveAssessment.Id) || (arg.Id == Course.PerformanceAsssessment.Id)))
                {
                    Assessments.Add(item);
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
