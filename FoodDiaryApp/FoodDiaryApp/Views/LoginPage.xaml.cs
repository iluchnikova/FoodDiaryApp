using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDiaryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //this.BindingContext = new LoginViewModel();
        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            //прописать проверку логина и пароля
            //С LoginPage вы можете проверить поля и синхронизировать с API. На основании ответа(успех / неудача) вы можете перенаправить на главную страницу(домашняя страница)

/*if (loginStatus == "isSuccess")
            {
                // if it ie MasterController/Drawer view/flyout menu/menu view
                Application.Current.MainPage = new MasterControllerPage(); // need to create a page in the type 'MasterDetailPage' 


                //or normal content page use this(here HomePage is your MainPage) 
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                // handle error alert
                DisplayAlert("Sorry", "Something went wrong in server.", "Ok");
            }*/
            Application.Current.MainPage = new AppShell();
        }
    }
}