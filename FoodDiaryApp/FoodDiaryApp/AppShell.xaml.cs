using System;
using System.Collections.Generic;
using FoodDiaryApp.ViewModels;
using FoodDiaryApp.Views;
using Xamarin.Forms;

namespace FoodDiaryApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
