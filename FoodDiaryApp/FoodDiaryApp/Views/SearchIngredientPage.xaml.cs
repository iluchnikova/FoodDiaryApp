using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using System.Globalization;
using Newtonsoft.Json;*/

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchIngredientPage : ContentPage
    {
        bool edited = true;//флаг редактирования
        public IngredientAndWeightDB Ingredient { get; set; }
        public List<IdAndName> IdAndNameList { get; set; }// список для выгрузки названий ингредиентов
        public IdAndName IdAndName { get; set; }

        public SearchIngredientPage(IngredientAndWeightDB ingredient)
        {
            IdAndNameList = new List<IdAndName>();
            foreach (var i in App.Db.GetIngredients())//добавляем id и наименования ингредиентов из БД Ingredients
            {
                IdAndNameList.Add(new IdAndName(i.Id, i.Name));
            }
            this.BindingContext = Ingredient;

            InitializeComponent();
            if (ingredient == null)
            {
                Ingredient = new IngredientAndWeightDB();
                edited = false;
            }
            else
            {
                Ingredient = ingredient;
            }

            //определение связанных переменных
            searchIngredient.Text = Ingredient.Name;
            ingredientWeight.Text = Ingredient.Weight.ToString();
            //определение источника для списка ингредиентов
            IngredientNameListView.ItemsSource = IdAndNameList;
        }

        //обработчик изменения текста в поле названия ингредиента
        private void SearchIngredient_TextChanged(object sender, TextChangedEventArgs e)
        {
            var key = searchIngredient.Text;
            IngredientNameListView.ItemsSource = IdAndNameList.Where(ingr => ingr.Name.Contains(key));//список показывает только названия ингредиентов, в которых содержится введенная пользователем информация
            if (IdAndNameList.Where(ingr => ingr.Name.Contains(key)).Count() == 0)
            {
                DisplayAlert("Error", "There aren't this ingredient's name in database", "OK");
            }
        }

        //обработчик нажатия на ингредиент в списке
        private void IngredientNameListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            IdAndName = e.Item as IdAndName;
            Ingredient.IngredientId = IdAndName.Id;
            Ingredient.Name = IdAndName.Name;

            if (IdAndName != null)
            {
                searchIngredient.Text = Ingredient.Name;//дозаполнение текста в поле для записи
            }
        }

        //обработчик нажатия кнопки сохранения
        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!Ingredient.Name.Equals(null))
                {
                    //проверка на наличие выбранного ингредиента в предоставляемом пользователю списке (ползователь должен нажать на выбранный ингредиент в списке)
                    if (IdAndNameList.Contains(new IdAndName(Ingredient.IngredientId, Ingredient.Name)))
                    {
                        //проверяем чтобы вес был больше нуля
                        if (Ingredient.Weight > 0)
                        {
                            //await Shell.Current.GoToAsync("..");
                            await Navigation.PopAsync();//переходим на предыдущую страницу

                            if (edited == false)//если ингредиент новый
                            {
                                // находим в стеке предпоследнюю страницу
                                AppShell appShellpage = Application.Current.MainPage as AppShell;
                                IReadOnlyList<Page> stack = appShellpage.Navigation.NavigationStack;
                                NewRecipePage homePage = stack[stack.Count - 1] as NewRecipePage;

                                if (homePage != null)
                                {
                                    homePage.AddIngredient(Ingredient);//вызываем с нее страницы метод добавления ингредиента в список
                                }
                            }
                        }
                        else
                        {
                            await DisplayAlert("Error", "Weight should be more than null", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "You should tap on ingredient's name on list under field for name", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Name can't be empty", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
            }
        }

        //обработчик изменения веса
        private void IngredientWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            var newText = e.NewTextValue;
            if (!newText.Equals(""))
            {
                if (!double.TryParse(newText, out _))
                {

                    DisplayAlert("Error", "Weight should be numeric", "Ok");
                }
                else
                {
                    //присваиваем значение введенному весу
                    Ingredient.Weight = double.Parse(newText);
                }
            }
            else
            {
                Ingredient.Weight = 0.0;
            }
        }
    }

    public class IdAndName : IEquatable<IdAndName>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IdAndName()
        {

        }
        public IdAndName(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //реализация интерфейса IEquatable для проверки на равенство объектов
        public bool Equals(IdAndName i)
        {
            if (Id == i.Id && Name.Equals(i.Name))
                return true;
            else
                return false;
        }
    }
}