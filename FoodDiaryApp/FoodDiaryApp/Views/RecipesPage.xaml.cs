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
    public partial class RecipesPage : ContentPage
    {
        public bool IsButtonVisible { get; set; }
        protected internal ObservableCollection<RecipeDB> RecipeList { get; set; }

        public RecipesPage()
        {

            ShowAllRecipesFromDB();//выгружаем все рецепты из локальной БД

            IsButtonVisible = false;

            AppShell appShellpage = Application.Current.MainPage as AppShell;
            IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
            //Если предыдущая страница была MealPage
            if (stack[stack.Count - 1] is MealPage)
                IsButtonVisible = true;//сделать кнопку добавления рецепта видимой


            InitializeComponent();

            this.BindingContext = this;
            myRecipesList.ItemsSource = RecipeList;

        }

        //обработка редактирования рецепта по нажатию на рецепт
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipeDB selectedRecipe = e.SelectedItem as RecipeDB;
            if (selectedRecipe != null)
            {
                myRecipesList.SelectedItem = null;
                PushPageInStack();
                await Navigation.PushAsync(new NewRecipePage(selectedRecipe));
            }
        }

        public async void AddInMeal(MealRecipeDB m)
        {
            await Navigation.PopAsync();
            try
            {
                AppShell appShellpage = Application.Current.MainPage as AppShell;
                IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
                MealPage mealPage = stack[stack.Count - 1] as MealPage;
                mealPage.AddRecipe(m);
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.Message, ex.StackTrace, "ok");
            }
        }

        // обработка нажатия кнопки добавления нового рецепта
        private async void Add_New_Recipe_Button_Clicked(object sender, EventArgs e)
        {
            PushPageInStack();
            //await Navigation.PushAsync(new RecipesPage());
            //await Shell.Current.GoToAsync($"//NewRecipe?id={null}");
            await Navigation.PushAsync(new NewRecipePage(new RecipeDB()));
        }

        //обработка добавления в прием пищи по нажатию на кнопку
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            RecipeDB recipe = (from r in RecipeList
                               where r.Id == int.Parse(button.CommandParameter.ToString())
                               select r).FirstOrDefault();

            PushPageInStack();
            AppShell appShellpage = Application.Current.MainPage as AppShell;
            IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
            try
            {
                await Navigation.PushModalAsync(new SetWeightPage(recipe));
            }
            catch(Exception ex)
            {
                await DisplayAlert(ex.Message, ex.StackTrace, "ok");
            }
            
        }

        //метод для выгрузки всех рецептов из локальной БД
        public void ShowAllRecipesFromDB()
        {
            RecipeList = new ObservableCollection<RecipeDB>();
            foreach (var r in App.Db.GetRecipes())
            {
                RecipeList.Add(r);
            }
        }

        public async void PushPageInStack()
        {
            AppShell appShellpage = Application.Current.MainPage as AppShell;
            IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
            //если страница RecipesPage не значится последней в стеке навигации
            if (stack.Count == 1 || !(stack[stack.Count - 1] is RecipesPage))
            {
                await Navigation.PushAsync(new RecipesPage());//помещаем страницу в стек навигации
            }
        }
    }
}