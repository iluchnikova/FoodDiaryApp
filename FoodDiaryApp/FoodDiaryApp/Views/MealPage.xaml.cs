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
    public partial class MealPage : ContentPage
    {
        protected internal ObservableCollection<MealRecipeDB> RecipeList { get; set; }
        MealDB Meal { get; set; }
        public MealPage(MealDB meal)
        {
            Meal = meal;

            RecipeList = Meal.ConvertToObservableCollection();

            InitializeComponent();

            BindingContext = this;
            mealNameListView.ItemsSource = RecipeList;
            mealNameListView.BindingContext = RecipeList;
            meal_DataPicker.Date = Meal.DateTime;
        }

        private List<MealRecipeDB> ConvertToList(ObservableCollection<MealRecipeDB> recipes)
        {
            List<MealRecipeDB> recipesList = new List<MealRecipeDB>();
            foreach (var r in recipes)
            {
                recipesList.Add(new MealRecipeDB { RecipeId = r.RecipeId, Name = r.Name, Weight = r.Weight, MealId = r.MealId });
            }
            return recipesList;
        }
        //вспомогательный метод для добавления рецепта (вызывается из RecipesPage при добавлении в Meal рецепта с весом)
        protected internal void AddRecipe(MealRecipeDB recipe)
        {
            RecipeList.Add(recipe);
        }
        //вспомогательный метод для удаления рецепта
        protected internal void DeleteRecipe(MealRecipeDB recipe)
        {
            if (IsRecipeExistInList(recipe))
            {
                RecipeList.Remove(recipe);
            }
            else
            {
                DisplayAlert("Error", "Meal doesn't consist recipe " + recipe.Name + " with weihgt " + recipe.Weight.ToString(), "OK");
            }
        }
        public bool IsRecipeExistInList(MealRecipeDB recipe)
        {
            return RecipeList.Contains(recipe);
        }
        //обработчик добавления нового рецепта (переход на страницу рецептов)
        private async void Add_Food_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipesPage());
        }
        //обработчик удаления рецепта с весом из приема пищи
        private async void DeleteRecipeButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;

            MealRecipeDB recipe = (from r in RecipeList
                                   where r.RecipeId == int.Parse(button.CommandParameter.ToString())
                                   select r).FirstOrDefault<MealRecipeDB>();
            bool result = await DisplayAlert("Сonfirm the action", "Do you want to delete " + recipe.Name + " from meal?", "Yes", "No");
            if (result)
            {
                RecipeList.Remove(recipe);
            }
        }
        //обработчик смены даты
        private void Meal_DataPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Meal.DateTime = e.NewDate;
        }
        //обработчик сохранения приема пищи
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Meal.Recipes = ConvertToList(RecipeList);

            if (Meal.Recipes.Count != 0)
            {
                App.Db.SaveMeal(Meal);

                await Navigation.PopAsync();

                // находим в стеке предпоследнюю страницу
                AppShell appShellpage = Application.Current.MainPage as AppShell;
                IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;

                try
                {
                    MyDietPage homePage = stack[stack.Count - 1] as MyDietPage;
                    if (homePage != null)
                        homePage.ShowAllMealsFromDB();
                }
                catch (Exception ex)
                {
                    await DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                }
            }
            else
                await DisplayAlert("Error", "Meal need at least one recipe", "Ok");
        }
        //обработчик выбора типа приема пищи
        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Meal.Name = picker.Items[picker.SelectedIndex];
        }
    }
}