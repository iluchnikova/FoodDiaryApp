using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private string sex;

        public DateTime Birthday { get; set; }
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged(nameof(Sex));
            }
        }
        public bool IsMale { get; set; }
        public App.GroupOfPhysicalActivity PhysicalActivity { get; set; }
        public double Tall { get; set; }
        public double Weight { get; set; }
        public bool Agree { get; set; }
        private double StepValue { get; set; }
        public SettingsPage()
        {
            StepValue = 1.0;

            if (App.User == null)
            {
                SetStartParameters();
            }
            else
            {
                SetUserParameters();
            }

            InitializeComponent();
            BindingContext = this;
        }

        public double GetFat(DateTime date1, DateTime date2)
        {
            return App.Db.GetSumOfIngredients(date1, date2).Fat;
        }
        public double GetProtein(DateTime date1, DateTime date2)
        {
            return App.Db.GetSumOfIngredients(date1, date2).Protein;
        }
        public double GetCarb(DateTime date1, DateTime date2)
        {
            return App.Db.GetSumOfIngredients(date1, date2).Carb;
        }
        public void SetStartParameters()
        {
            Birthday = DateTime.UtcNow;
            Sex = "Male";
            IsMale = true;
            PhysicalActivity = App.GroupOfPhysicalActivity.I;
            Tall = 0;
            Weight = 0;
            Agree = true;
        }
        public void SetUserParameters()
        {
            Birthday = App.User.Birthday;
            if (App.User.IsMale)
                Sex = "Male";
            else
                Sex = "Female";
            IsMale = App.User.IsMale;
            PhysicalActivity = App.User.GroupOfPhysicalActivity;
            Tall = App.User.Tall;
            Weight = App.User.Weight;
            Agree = App.User.Agree;
        }
        private void SaveInProperties(User user)
        {
            App.Current.Properties["user"] = JsonConvert.SerializeObject(user);
        }
        private void DeleteProperties()
        {
            object obj = "";
            if (App.Current.Properties.TryGetValue("user", out obj))
                App.Current.Properties.Remove("user");
            App.User = null;
        }


        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isMale = e.Value;
            IsMale = isMale;

            if (isMale)
                Sex = "Male";
            else
                Sex = "Female";
        }
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);
            activitySlider.Value = newStep * StepValue;
            int val = int.Parse(newStep.ToString());

            PhysicalActivity = (App.GroupOfPhysicalActivity)(val);
        }
        private void Tall_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out _))
                Tall = double.Parse(e.NewTextValue);
            else
                Tall = 0;
        }
        private void Weight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out _))
                Weight = double.Parse(e.NewTextValue);
            else
                Weight = 0;
        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Birthday = e.NewDate;
        }
        private void Agree_Toggled(object sender, ToggledEventArgs e)
        {
            Agree = e.Value;
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            UserData userData;
            //проверка, сохранение, отправка на сервер
            if (Weight > 5)
            {
                if (Birthday.Date < DateTime.UtcNow.Date)
                {
                    if (Tall > 50)
                    {
                        userData = new UserData();
                        if (App.User != null)
                        {
                            userData.OldTall = App.User.Tall;
                            userData.OldWeight = App.User.Weight;
                            userData.DaysFromChange = (DateTime.UtcNow - App.User.DateOfChange).Days;
                            userData.FatFromChange = GetFat(DateTime.UtcNow, App.User.DateOfChange);
                            userData.ProteinFromChange = GetProtein(DateTime.UtcNow, App.User.DateOfChange);
                            userData.CarbsFromChange = GetCarb(DateTime.UtcNow, App.User.DateOfChange);
                        }
                        else
                        {
                            userData.OldTall = this.Tall;
                            userData.OldWeight = this.Weight;
                            userData.DaysFromChange = 0;
                            userData.FatFromChange = 0;
                            userData.ProteinFromChange = 0;
                            userData.CarbsFromChange = 0;
                        }

                        App.User = new User()
                        {
                            Birthday = this.Birthday,
                            IsMale = this.IsMale,
                            Tall = this.Tall,
                            Weight = this.Weight,
                            Agree = this.Agree,
                            GroupOfPhysicalActivity = PhysicalActivity,
                            DateOfChange = DateTime.UtcNow
                        };

                        userData.isMale = App.User.IsMale;
                        userData.NewTall = App.User.Tall;
                        userData.NewWeight = App.User.Weight;

                        //записываем в словарь Proprties
                        SaveInProperties(App.User);

                        /*                        if (Agree)
                                                {
                                                    UserDataService service = new UserDataService();
                                                    UserData data = await service.Add(userData);
                                                    if (data != null)
                                                        await DisplayAlert("Message", "Settings were send", "Ok");
                                                    //прописать сериализацию в json userData и отправку на сервер
                                                }*/


                        AppShell appShellpage = Application.Current.MainPage as AppShell;
                        IReadOnlyList<Page> modalstack = appShellpage.Navigation.ModalStack;
                        if (modalstack.Count > 0)
                        {
                            await Navigation.PopModalAsync();
/*                            IReadOnlyList<Page> stack = appShellpage.Navigation.ModalStack;
                            BalancePage homePage = stack[stack.Count - 1] as BalancePage;

                            if (homePage != null)
                            {
                                homePage.UpdateValuesInDiagramms();
                            }*/
                        }

                        await DisplayAlert("Message", "Settings were saved", "Ok");
                    }
                    else
                        await DisplayAlert("Error", "Tall should be more 50 cm", "OK");
                }
                else
                    await DisplayAlert("Error", "Birthday should be earlier than today", "OK");
            }
            else
                await DisplayAlert("Error", "Weight should be more 5 kg", "OK");
        }
        private void Clear_Clicked(object sender, EventArgs e)
        {
            DeleteProperties();
            SetStartParameters();
        }
    }

    [Serializable]
    public class UserData//прописать отправку
    {
        public bool isMale { get; set; }
        public double OldTall { get; set; }
        public double NewTall { get; set; }
        public double OldWeight { get; set; }
        public double NewWeight { get; set; }
        public int DaysFromChange { get; set; }
        public double FatFromChange { get; set; }
        public double ProteinFromChange { get; set; }
        public double CarbsFromChange { get; set; }

        private string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        private UserData Deserialize(string jsonStr)
        {
            return JsonConvert.DeserializeObject<UserData>(jsonStr);
        }
        public void SendData()///
        {
            string jsonStr = this.Serialize();

        }
    }

    //вспомогательный класс для отправки на сервер
    public class UserDataService
    {
        const string Url = "http://192.168.0.17:8080/api/userdata/";//адрес веб-сервиса

        private HttpClient GetHttpClient()//настройка клиента
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async Task<UserData> Add(UserData userData)
        {
            HttpClient httpClient = GetHttpClient();
            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8,
                "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<UserData>(
                await response.Content.ReadAsStringAsync());
        }
    }
}