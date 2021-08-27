using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using C971.Models;
using C971.Views;

namespace C971.ViewModels
{
    public class InstructorViewModel : BaseViewModel
    {

        public ObservableCollection<Instructor> Instructors { get; set; }
        public Instructor Teacher { get; set; } //Used by the assessment detail view of an individual assessment
        public Command LoadItemsCommand { get; set; }

        //Default constructor
        public InstructorViewModel()
        {
            Title = "Instructors";
            Instructors = new ObservableCollection<Instructor>();
            Teacher = new Instructor();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewInstructorPage, Instructor>(this, "AddInstructor", async (obj, item) =>
            {
                var newItem = item as Instructor;
                Instructors.Add(newItem);
                await DataStore.AddInstructorAsync(newItem);
            });

            MessagingCenter.Subscribe<InstructorDetailPage, Instructor>(this, "DeleteInstructor", async (obj, item) =>
            {
                var newItem = item as Instructor;
                Instructors.Remove(newItem);
                await DataStore.DeleteInstructorAsync(newItem.Id);
            });
        }

        public InstructorViewModel(Instructor inst) : this()
        {
            
            Title = inst.Name;
            Teacher = inst;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Instructors.Clear();
                var tmpInstructors = await DataStore.GetInstructorsAsync(true);
                foreach (var item in tmpInstructors)
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