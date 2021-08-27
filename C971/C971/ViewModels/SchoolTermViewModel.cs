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
    public class SchoolTermViewModel : BaseViewModel
    {
        public ObservableCollection<SchoolTerm> Terms { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SchoolTermViewModel()
        {
            Title = "Terms";
            Terms = new ObservableCollection<SchoolTerm>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //Messaging to pass information back and forth
            MessagingCenter.Subscribe<NewTermPage, SchoolTerm>(this, "AddTerm", async (obj, item) =>
            {
                var newItem = item as SchoolTerm;
                Terms.Add(newItem);
                await DataStore.AddTermAsync(newItem);
                
            });

            MessagingCenter.Subscribe<CourseListPage, SchoolTerm>(this, "DeleteTerm", (obj, item) =>
            {
                var newItem = item as SchoolTerm;
                Terms.Remove(newItem);
            });
        }

        //Load the information into the collections for the forms to use
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Terms.Clear();
                var items = await DataStore.GetTermsAsync(true);
                foreach (var item in items)
                {
                    Terms.Add(item);
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