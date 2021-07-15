using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyDietPage : ContentPage
    {
        protected internal ObservableCollection<MealDB> MealList { get; set; }
        DateTime Date { get; set; }
        public MyDietPage()
        {
            Date = DateTime.Today;
            ShowAllMealsFromDB();

            InitializeComponent();

            BindingContext = this;

            mealList.ItemsSource = MealList;
        }

        private async void Add_Meal_Button_Clicked(object sender, EventArgs e)
        {
            PushPageInStack();//помещаем страницу в стек навигации
            await Navigation.PushAsync(new MealPage(new MealDB() { DateTime = Date, Name = "Lunch", Recipes = new List<MealRecipeDB>() }));//переходим на страницу добавления приема пищи
        }

        private void MyDietPage_DatePicker_DataSelected(object sender, DateChangedEventArgs e)
        {
            Date = e.NewDate;

            ShowAllMealsFromDB();
            mealList.ItemsSource = MealList;
        }

        public void ShowAllMealsFromDB(DateTime date)
        {
            MealList = new ObservableCollection<MealDB>();
            foreach (var m in App.Db.GetMeals(date))
                MealList.Add(m);
        }
        /*        // вспомогательный метод для добавления элемента в список
                protected internal void AddMeal(MealDB meal)
                {
                    MealList.Add(meal);
                }*/
        //вспомогательный метод для удаления ингредиента
        public void ShowAllMealsFromDB()
        {
            ShowAllMealsFromDB(Date);
        }
        public async void PushPageInStack()
        {
            AppShell appShellpage = Application.Current.MainPage as AppShell;
            IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
            //если страница MyDietPage не значится последней в стеке навигации
            if (stack.Count == 1 || !(stack[stack.Count - 1] is MyDietPage))
            {
                await Navigation.PushAsync(new MyDietPage());//помещаем страницу в стек навигации
            }
        }

        private void DeleteMeal_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;

            MealDB meal = (from m in MealList
                           where m.MealId == int.Parse(button.CommandParameter.ToString())
                           select m).FirstOrDefault();

            //удаление из БД
            App.Db.DeleteMeal(meal);
            //обновление списка приемов пищи
            ShowAllMealsFromDB(meal.DateTime);
        }

        private async void mealList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MealDB meal = e.SelectedItem as MealDB;
            if (meal != null)
            {
                mealList.SelectedItem = null;

                PushPageInStack();//помещаем страницу в стек навигации
                await Navigation.PushAsync(new MealPage(meal));//переходим на страницу редактирования приема пищи
            }
        }
    }
}