using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;

namespace FoodDiaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BalancePage : ContentPage
    {
        public DateTime FirstDate { get; set; }
        public DateTime SecondtDate { get; set; }
        protected internal IngredientDB NormalDailyIntake { get; set; }
        protected internal IngredientDB UserDailyIntake { get; set; }
        protected internal List<ChartEntry> UserEntries { get; set; }
        protected internal List<ChartEntry> NormalEntries { get; set; }

        public BalancePage()
        {
            NormalDailyIntake = new IngredientDB();
            UserDailyIntake = new IngredientDB();
            FirstDate = DateTime.UtcNow;
            SecondtDate = DateTime.UtcNow;

            UpdateValuesInDiagramms();

            InitializeComponent();
            this.BindingContext = this;
            ChartNormal.Chart = new DonutChart { Entries = NormalEntries };
            ChartUser.Chart = new DonutChart { Entries = UserEntries };
        }

        private void date1_DateSelected(object sender, DateChangedEventArgs e)
        {
            FirstDate = e.NewDate;
            UpdateValuesInDiagramms();
        }

        private void date2_DateSelected(object sender, DateChangedEventArgs e)
        {
            SecondtDate = e.NewDate;
            UpdateValuesInDiagramms();
        }

        public IngredientDB GetNormalDailyIntake()//определение нормы потребленных БЖУ и микроэлементов
        {
            //вычисляем количество полных лет пользователя
            int age = DateTime.UtcNow.Year - App.User.Birthday.Year;
            if (DateTime.UtcNow.DayOfYear < App.User.Birthday.DayOfYear)
                age++;

            return new IngredientDB().GetNormalDailyIntake(age, App.User.IsMale, App.User.GroupOfPhysicalActivity);
        }
        public IngredientDB GetDailyIntake(DateTime firstDate, DateTime secondtDate)
        { 
            IngredientDB daylyIntake = App.Db.GetSumOfIngredients(firstDate, secondtDate);

            int days = (secondtDate - firstDate).Days + 1;

            return daylyIntake.MultiplyByRate(1 / days);
        }
        public async void UpdateValuesInDiagramms()
        {
            if (App.User != null)
            {
                try
                {
                    UserEntries = new List<ChartEntry>();
                    NormalEntries = new List<ChartEntry>();

                    NormalDailyIntake = GetNormalDailyIntake();
                    NormalEntries.Add(new ChartEntry(float.Parse(NormalDailyIntake.Fat.ToString())) { Color = SKColor.Parse("#e98e8e"), Label = "Fat", ValueLabel = NormalDailyIntake.Fat.ToString() });
                    NormalEntries.Add(new ChartEntry(float.Parse(NormalDailyIntake.Protein.ToString())) { Color = SKColor.Parse("#d7e5f5"), Label = "Protein", ValueLabel = NormalDailyIntake.Protein.ToString() });
                    NormalEntries.Add(new ChartEntry(float.Parse(NormalDailyIntake.Carb.ToString())) { Color = SKColor.Parse("#e0eeb0"), Label = "Carb", ValueLabel = NormalDailyIntake.Carb.ToString() });

                    UserDailyIntake = GetDailyIntake(FirstDate, SecondtDate);
                    UserEntries.Add(new ChartEntry(float.Parse(UserDailyIntake.Fat.ToString())) { Color = SKColor.Parse("#e98e8e"), Label = "Fat", ValueLabel = UserDailyIntake.Fat.ToString() });
                    UserEntries.Add(new ChartEntry(float.Parse(UserDailyIntake.Protein.ToString())) { Color = SKColor.Parse("#d7e5f5"), Label = "Protein", ValueLabel = UserDailyIntake.Protein.ToString() });
                    UserEntries.Add(new ChartEntry(float.Parse(UserDailyIntake.Carb.ToString())) { Color = SKColor.Parse("#e0eeb0"), Label = "Carb", ValueLabel = UserDailyIntake.Carb.ToString() });

                    ChartNormal.Chart = new DonutChart { Entries = NormalEntries };
                    ChartUser.Chart = new DonutChart { Entries = UserEntries };

                    UserA.Text = (UserDailyIntake.A * 1000000).ToString();
                    NormalA.Text = (NormalDailyIntake.A * 1000000).ToString();
                    UserB1.Text = (UserDailyIntake.B1 * 1000000).ToString();
                    NormalB1.Text = (NormalDailyIntake.B1 * 1000000).ToString();
                    UserB2.Text = (UserDailyIntake.B2 * 1000000).ToString();
                    NormalB2.Text = (NormalDailyIntake.B2 * 1000000).ToString();
                    UserB3.Text = (UserDailyIntake.B3 * 1000000).ToString();
                    NormalB3.Text = (NormalDailyIntake.B3 * 1000000).ToString();
                    UserB4.Text = (UserDailyIntake.B4 * 1000000).ToString();
                    NormalB4.Text = (NormalDailyIntake.B4 * 1000000).ToString();
                    UserB5.Text = (UserDailyIntake.B5 * 1000000).ToString();
                    NormalB5.Text = (NormalDailyIntake.B5 * 1000000).ToString();
                    UserB6.Text = (UserDailyIntake.B6 * 1000000).ToString();
                    NormalB6.Text = (NormalDailyIntake.B6 * 1000000).ToString();
                    UserB8.Text = (UserDailyIntake.B8 * 1000000).ToString();
                    NormalB8.Text = (NormalDailyIntake.B8 * 1000000).ToString();
                    UserB9.Text = (UserDailyIntake.B9 * 1000000).ToString();
                    NormalB9.Text = (NormalDailyIntake.B9 * 1000000).ToString();
                    UserB12.Text = (UserDailyIntake.B12 * 1000000).ToString();
                    NormalB12.Text = (NormalDailyIntake.B12 * 1000000).ToString();
                    UserC.Text = (UserDailyIntake.C * 1000000).ToString();
                    NormalC.Text = (NormalDailyIntake.C * 1000000).ToString();
                    UserD.Text = (UserDailyIntake.D * 1000000).ToString();
                    NormalD.Text = (NormalDailyIntake.D * 1000000).ToString();
                    UserE.Text = (UserDailyIntake.E * 1000000).ToString();
                    NormalE.Text = (NormalDailyIntake.E * 1000000).ToString();
                    UserH.Text = (UserDailyIntake.H * 1000000).ToString();
                    NormalH.Text = (NormalDailyIntake.H * 1000000).ToString();
                    UserK.Text = (UserDailyIntake.K * 1000000).ToString();
                    NormalK.Text = (NormalDailyIntake.K * 1000000).ToString();
                    UserFerrum.Text = (UserDailyIntake.Ferrum * 1000000).ToString();
                    NormalFerrum.Text = (NormalDailyIntake.Ferrum * 1000000).ToString();
                    UserZinc.Text = (UserDailyIntake.Zinc * 1000000).ToString();
                    NormalZinc.Text = (NormalDailyIntake.Zinc * 1000000).ToString();
                    UserCuprum.Text = (UserDailyIntake.Cuprum * 1000000).ToString();
                    NormalCuprum.Text = (NormalDailyIntake.Cuprum * 1000000).ToString();
                    UserCobalt.Text = (UserDailyIntake.Cobalt * 1000000).ToString();
                    NormalCobalt.Text = (NormalDailyIntake.Cobalt * 1000000).ToString();
                    UserManganese.Text = (UserDailyIntake.Manganese * 1000000).ToString();
                    NormalManganese.Text = (NormalDailyIntake.Manganese * 1000000).ToString();
                    UserSelenium.Text = (UserDailyIntake.Selenium * 1000000).ToString();
                    NormalSelenium.Text = (NormalDailyIntake.Selenium * 1000000).ToString();
                    UserFluorine.Text = (UserDailyIntake.Fluorine * 1000000).ToString();
                    NormalFluorine.Text = (NormalDailyIntake.Fluorine * 1000000).ToString();
                    UserIodine.Text = (UserDailyIntake.Iodine * 1000000).ToString();
                    NormalIodine.Text = (NormalDailyIntake.Iodine * 1000000).ToString();
                    UserCalcium.Text = (UserDailyIntake.Calcium * 1000000).ToString();
                    NormalCalcium.Text = (NormalDailyIntake.Calcium * 1000000).ToString();
                    UserPhosphorus.Text = (UserDailyIntake.Phosphorus * 1000000).ToString();
                    NormalPhosphorus.Text = (NormalDailyIntake.Phosphorus * 1000000).ToString();
                    UserMagnesium.Text = (UserDailyIntake.Magnesium * 1000000).ToString();
                    NormalMagnesium.Text = (NormalDailyIntake.Magnesium * 1000000).ToString();
                    UserSodium.Text = (UserDailyIntake.Sodium * 1000000).ToString();
                    NormalSodium.Text = (NormalDailyIntake.Sodium * 1000000).ToString();
                    UserChlorine.Text = (UserDailyIntake.Chlorine * 1000000).ToString();
                    NormalChlorine.Text = (NormalDailyIntake.Chlorine * 1000000).ToString();
                    UserSulfur.Text = (UserDailyIntake.Sulfur * 1000000).ToString();
                    NormalSulfur.Text = (NormalDailyIntake.Sulfur * 1000000).ToString();
                    UserMolybdenum.Text = (UserDailyIntake.Molybdenum * 1000000).ToString();
                    NormalMolybdenum.Text = (NormalDailyIntake.Molybdenum * 1000000).ToString();
                    UserChromium.Text = (UserDailyIntake.Chromium * 1000000).ToString();
                    NormalChromium.Text = (NormalDailyIntake.Chromium * 1000000).ToString();
                    UserKalium.Text = (UserDailyIntake.Kalium * 1000000).ToString();
                    NormalKalium.Text = (NormalDailyIntake.Kalium * 1000000).ToString();

                }
                catch (Exception ex)
                {
                    await DisplayAlert(ex.Message, ex.StackTrace, "ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "You shoud set your parameters", "ok");
                try
                {                    
                    await Navigation.PushModalAsync(new SettingsPage());
                }
                catch (Exception ex)
                {
                    await DisplayAlert(ex.Message, ex.StackTrace, "ok");
                }

            }
        }

    }
}