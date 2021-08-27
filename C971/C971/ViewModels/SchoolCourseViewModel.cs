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
    public class SchoolCourseViewModel : BaseViewModel
    {
        public ObservableCollection<SchoolCourse> Courses { get; set; }
        public ObservableCollection<Instructor> Instructors { get; set; }
        public SchoolTerm CurrentTerm { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SchoolCourseViewModel()
        {
            Title = "Courses";
            Courses = new ObservableCollection<SchoolCourse>();
            Instructors = new ObservableCollection<Instructor>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Messaging to pass information back and forth
            MessagingCenter.Subscribe<NewCoursePage, SchoolCourse>(this, "AddCourse", async (obj, item) =>
            {
                var newItem = item as SchoolCourse;
                Courses.Add(newItem);
                await DataStore.AddCourseAsync(newItem);
            });

            MessagingCenter.Subscribe<CourseDetailPage, SchoolCourse>(this, "DeleteCourse", (obj, item) =>
            {
                var newItem = item as SchoolCourse;
                Courses.Remove(newItem);
            });

            MessagingCenter.Subscribe<CourseListPage, SchoolTerm>(this, "DeleteTerm", async (obj, item) =>
            {
                var newItem = item as SchoolTerm;
                await DataStore.DeleteTermAsync(newItem.Id);
            });
        }

        public SchoolCourseViewModel(SchoolTerm term) : this()
        {
            CurrentTerm = term;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Courses.Clear();
                var items = await DataStore.GetCoursesAsync(true);
                if (CurrentTerm != null)
                {
                    foreach (var item in items.Where((SchoolCourse arg) => arg.AssociatedTermId == CurrentTerm.Id))
                    {
                        Courses.Add(item);
                    }
                }
                else
                {
                    foreach (var item in items)
                    {
                        Courses.Add(item);
                    }
                }
                Instructors.Clear();
                var teachers = await DataStore.GetInstructorsAsync(true);
                foreach (var item in teachers.ToList())
                {
                    Instructors.Add(item);
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