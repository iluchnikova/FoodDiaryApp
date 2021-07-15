using System.ComponentModel;
using Xamarin.Forms;
using FoodDiaryApp.ViewModels;

namespace FoodDiaryApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}