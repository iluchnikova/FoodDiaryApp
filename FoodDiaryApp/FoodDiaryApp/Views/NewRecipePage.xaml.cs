using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FoodDiaryApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecipePage : ContentPage
    {
        RecipeDB Recipe { get; set; }
        protected internal ObservableCollection<IngredientAndWeightDB> Ingredients { get; set; }

        public NewRecipePage(RecipeDB recipe)
        {
            Recipe = recipe;
            if (Recipe.Ingredients.Count > 0)
                foreach (var i in Recipe.Ingredients)
                {
                    i.SetIngredientName();
                }

            InitializeComponent();
            BindingContext = Recipe;


            Ingredients = Recipe.ConvertToObservableCollection();

            ingredientList.ItemsSource = Ingredients;
            ingredientList.BindingContext = Ingredients;
            recipeName.Text = Recipe.Name;
            recipeWeight.Text = Recipe.Weight.ToString();
            recipeDescription.Text = Recipe.Description;
        }

        private List<IngredientAndWeightDB> ConvertToList(ObservableCollection<IngredientAndWeightDB> Ingredients)
        {
            List<IngredientAndWeightDB> ingrList = new List<IngredientAndWeightDB>();
            foreach (var i in Ingredients)
            {
                ingrList.Add(new IngredientAndWeightDB { IngredientId = i.IngredientId, Name = i.Name, Weight = i.Weight, RecipeId = i.RecipeId });
            }
            return ingrList;
        }
        //обработчик выбора ингредиента в списке для редактирования
        private async void IngredientList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IngredientAndWeightDB selectedIngredient = e.SelectedItem as IngredientAndWeightDB;
            if (selectedIngredient != null)
            {
                ingredientList.SelectedItem = null;//снятие выделения
                //await Shell.Current.GoToAsync($"//AddIngredient?ingredient={selectedIngredient}");//переход на страницу редактирования ингредиента
                await Navigation.PushAsync(new SearchIngredientPage(selectedIngredient));//переход на страницу редактирования ингредиент
            }
        }
        //обработчик добавления нового ингредиента
        private async void Add_New_Ingredient_Button_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//AddIngredient?ingredient={null}");
            await Navigation.PushAsync(new SearchIngredientPage(null));
        }

        // вспомогательный метод для добавления элемента в список
        protected internal void AddIngredient(IngredientAndWeightDB ingredient)
        {
            Ingredients.Add(ingredient);
        }
        //вспомогательный метод для удаления ингредиента
        protected internal void DeleteIngredient(IngredientAndWeightDB ingredient)
        {
            if (IsIngredientExistInList(ingredient))
            {
                Ingredients.Remove(ingredient);
            }
            else
            {
                DisplayAlert("Error", "Recipe doesn't consist ingredient " + ingredient.Name + " with weihgt " + ingredient.Weight.ToString(), "OK");
            }
        }
        public bool IsIngredientExistInList(IngredientAndWeightDB ingredient)
        {
            return Ingredients.Contains(ingredient);
        }
        private async void DeleteIngredientButton_Clicked(object sender, EventArgs e)
        {
            var but = sender as ImageButton;

            IngredientAndWeightDB ingredient = (from i in Ingredients
                                                where i.IngredientId == int.Parse(but.CommandParameter.ToString())
                                                select i).FirstOrDefault<IngredientAndWeightDB>();
            bool result = await DisplayAlert("Сonfirm the action", "Do you want to delete " + ingredient.Name + " from recipe?", "Yes", "No");
            if (result)
            {
                DeleteIngredient(ingredient);
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recipe.Name = e.NewTextValue;
        }

        private void RecipeWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            var newText = e.NewTextValue;
            if (!newText.Equals(""))
            {
                if (!double.TryParse(newText, out _))
                {
                    DisplayAlert("Error", "The weight should have a numeric value", "Ok");
                }
                else
                {
                    Recipe.Weight = double.Parse(newText);
                }
            }
            else
            {
                Recipe.Weight = 0.0;
            }
        }
        private async void DeleteRecipe_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Сonfirm the action", "Do you want to delete recipe?", "Yes", "Cancel");
            if (result)
            {
                if (App.Db.IsRecipeExistInDB(Recipe.Id))
                    App.Db.DeleteRecipe(Recipe);
                else
                    Recipe = new RecipeDB();
                /*                RecipeDB recipe = (RecipeDB)BindingContext;
                                App.Db.DeleteRecipe(recipe.Id);
                                App.Db.DeleteIngredientNotSaveFromRecipe();*/
                await this.Navigation.PopAsync();
                // await Shell.Current.GoToAsync("..");
            }
        }
        private async void SaveRecipe_Clicked(object sender, EventArgs e)
        {
            Recipe.Ingredients = ConvertToList(Ingredients);

            if (Recipe.Name != "")
            {
                if (Recipe.Ingredients.Count != 0)
                {
                    if (recipeWeight.Text.Equals("0") || recipeWeight.Text == null)//если пользователь не установил вес
                    {
                        Recipe.Weight = 0;
                        await DisplayAlert("Message", "The weight will be calculated automatically", "Ok");
                        foreach (var i in Ingredients)
                        {
                            Recipe.Weight += i.Weight;//присвоить весу рецепта вес ингредиентов
                        }
                        recipeWeight.Text = Recipe.Weight.ToString();
                    }

                    App.Db.SaveRecipe(Recipe);

                    await Navigation.PopAsync();

                    // находим в стеке предпоследнюю страницу
                    AppShell appShellpage = Application.Current.MainPage as AppShell;
                    IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;

                    try
                    {
                        RecipesPage homePage = stack[stack.Count - 1] as RecipesPage;
                        if (homePage != null)
                            homePage.ShowAllRecipesFromDB();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                    }
                }
                else
                    await DisplayAlert("Error", "Recipe need at least one ingredient", "Ok");
            }
            else
                await DisplayAlert("Error", "Name can't be empty", "Ok");

        }
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("..");
            this.Navigation.PopAsync();

        }


    }
}