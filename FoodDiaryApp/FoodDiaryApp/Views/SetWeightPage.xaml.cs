using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetWeightPage : ContentPage
    {
        public MealRecipeDB RecipeAndWeight { get; set; }
        public SetWeightPage(RecipeDB recipe)
        {
            RecipeAndWeight = new MealRecipeDB();
            RecipeAndWeight.Name = recipe.Name;
            RecipeAndWeight.RecipeId = recipe.Id;
            RecipeAndWeight.Weight = 0;
            InitializeComponent();
            recipeName.Text = RecipeAndWeight.Name;
            fieldForWeight.Text = RecipeAndWeight.Weight.ToString();
            //RecipeAndWeight.Weight= double.Parse(fieldForWeight.Text);//прописать TryParse
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(fieldForWeight.Text, out _))
                await DisplayAlert("Error", "The weight should be numeric", "OK");
            else
                RecipeAndWeight.Weight = double.Parse(fieldForWeight.Text);
            //проверка, чтобы вес был больше 0;
            if (RecipeAndWeight.Weight > 0)
            {
                await Navigation.PopModalAsync();
                //компоновка
                AppShell appShellpage = Application.Current.MainPage as AppShell;
                IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;

                try
                {
                    RecipesPage recipesPage = stack[stack.Count - 1] as RecipesPage;
                    recipesPage.AddInMeal(RecipeAndWeight);

                }
                catch (Exception ex)
                {
                    DisplayAlert(ex.Message, ex.StackTrace, "ok");
                }
            }
            else
            {
                DisplayAlert("Error", "The weight should be more than 0", "OK");
            }
        }
    }
}