using C971.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            //Fill the list with the menu items we want.
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.TermList, Title="Terms" },
                new HomeMenuItem {Id = MenuItemType.CourseList, Title="Courses" },
                new HomeMenuItem {Id = MenuItemType.AssessmentList, Title="Assessments" },
                new HomeMenuItem {Id = MenuItemType.InstructorList, Title="Instructors" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}