using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodDiaryApp.Services;
using FoodDiaryApp.Views;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Exceptions;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace FoodDiaryApp
{
    public partial class App : Application
    {
        public const string DB_NAME = "foodDiary.db";
        public static DBRepository db;
        public static DBRepository Db
        {
            get
            {
                if (db == null)
                {
                    db = new DBRepository(Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_NAME));
                }
                return db;
            }
        }
        public static User User { get; set; }

        public enum GroupOfPhysicalActivity { I, II, III, IV, V };

        public App()
        {
            /*            Db.DropTabels();
                        Db.SaveIngredient("cucumber", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("orange", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("carrot", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("potato", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("onion", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("strawberry", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                        Db.SaveIngredient("sugar", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                       Db.SaveIngredient("salt", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);*/
            //Db.ClearTable("Recipes");
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            GetProperties();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private bool IsPropertiesExist()
        {
            object obj = "";
            if (App.Current.Properties.TryGetValue("user", out obj))
                return true;
            else
                return false;
        }
        private void GetProperties()
        {
            object obj = "";
            if (IsPropertiesExist())
                App.User = JsonConvert.DeserializeObject<User>(obj.ToString());
        }
    }

    [Serializable]
    public class User
    {
        private DateTime birthday;
        private bool isMale;
        private double tall;
        private double weight;
        private bool agree;

        public DateTime Birthday
        {
            set
            {
                if (value < DateTime.UtcNow)
                    birthday = value;
            }
            get { return birthday; }
        }
        public bool IsMale
        {
            set
            {
                isMale = value;
            }
            get { return isMale; }
        }
        public double Tall
        {
            set
            {
                if (value > 0)
                    tall = value;
            }
            get { return tall; }
        }
        public double Weight
        {
            set
            {
                if (value > 0)
                    weight = value;
            }
            get { return weight; }
        }
        public App.GroupOfPhysicalActivity GroupOfPhysicalActivity { get; set; }
        public bool Agree
        {
            set { agree = value; }
            get { return agree; }
        }
        public DateTime DateOfChange { get; set; }
    }

    [Table("Ingredients")]
    public class IngredientDB : INotifyPropertyChanged
    {
        private string name;
        private double protein;
        private double fat;
        private double carb;
        private double a;
        private double b1;
        private double b2;
        private double b3;
        private double b4;
        private double b5;
        private double b6;
        private double b8;
        private double b9;
        private double b12;
        private double c;
        private double d;
        private double e;
        private double h;
        private double k;
        private double ferrum;
        private double zinc;
        private double cuprum;
        private double cobalt;
        private double manganese;
        private double selenium;
        private double fluorine;
        private double iodine;
        private double calcium;
        private double phosphorus;
        private double magnesium;
        private double sodium;
        private double chlorine;
        private double sulfur;
        private double molybdenum;
        private double chromium;
        private double kalium;


        [PrimaryKey, AutoIncrement, Column("ingredient_id"), NotNull]
        public int Id { get; set; }
        [Unique, Column("ingredient_name")]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public double Protein
        {
            get { return protein; }
            set
            {
                if (protein != value)
                {
                    protein = value;
                    OnPropertyChanged("Protein");
                }
            }
        }
        public double Fat
        {
            get { return fat; }
            set
            {
                if (fat != value)
                {
                    fat = value;
                    OnPropertyChanged("Fat");
                }
            }
        }
        public double Carb
        {
            get { return carb; }
            set
            {
                if (carb != value)
                {
                    carb = value;
                    OnPropertyChanged("Carb");
                }
            }
        }
        public double A
        {
            get { return a; }
            set
            {
                if (a != value)
                {
                    a = value;
                    OnPropertyChanged("A");
                }
            }
        }
        public double B1
        {
            get { return b1; }
            set
            {
                if (b1 != value)
                {
                    b1 = value;
                    OnPropertyChanged("B1");
                }
            }
        }
        public double B2
        {
            get { return b2; }
            set
            {
                if (b2 != value)
                {
                    b2 = value;
                    OnPropertyChanged("B2");
                }
            }
        }
        public double B3
        {
            get { return b3; }
            set
            {
                if (b3 != value)
                {
                    b3 = value;
                    OnPropertyChanged("B3");
                }
            }
        }
        public double B4
        {
            get { return b4; }
            set
            {
                if (b4 != value)
                {
                    b4 = value;
                    OnPropertyChanged("B4");
                }
            }
        }
        public double B5
        {
            get { return b5; }
            set
            {
                if (b5 != value)
                {
                    b5 = value;
                    OnPropertyChanged("B5");
                }
            }
        }
        public double B6
        {
            get { return b6; }
            set
            {
                if (b6 != value)
                {
                    b6 = value;
                    OnPropertyChanged("B6");
                }
            }
        }
        public double B8
        {
            get { return b8; }
            set
            {
                if (b8 != value)
                {
                    b8 = value;
                    OnPropertyChanged("B8");
                }
            }
        }
        public double B9
        {
            get { return b9; }
            set
            {
                if (b9 != value)
                {
                    b9= value;
                    OnPropertyChanged("B9");
                }
            }
        }
        public double B12
        {
            get { return b2; }
            set
            {
                if (b12 != value)
                {
                    b12 = value;
                    OnPropertyChanged("B12");
                }
            }
        }
        public double C
        {
            get { return c; }
            set
            {
                if (c != value)
                {
                    c = value;
                    OnPropertyChanged("C");
                }
            }
        }
        public double D
        {
            get { return d; }
            set
            {
                if (d != value)
                {
                    d = value;
                    OnPropertyChanged("D");
                }
            }
        }
        public double E
        {
            get { return e; }
            set
            {
                if (e != value)
                {
                    e = value;
                    OnPropertyChanged("E");
                }
            }
        }
        public double H
        {
            get { return h; }
            set
            {
                if (h != value)
                {
                    h = value;
                    OnPropertyChanged("H");
                }
            }
        }
        public double K
        {
            get { return k; }
            set
            {
                if (k != value)
                {
                    k = value;
                    OnPropertyChanged("K");
                }
            }
        }
        public double Ferrum
        {
            get { return ferrum; }
            set
            {
                if (ferrum != value)
                {
                    ferrum = value;
                    OnPropertyChanged("Ferrum");
                }
            }
        }
        public double Zinc
        {
            get { return zinc; }
            set
            {
                if (zinc != value)
                {
                    zinc = value;
                    OnPropertyChanged("Zinc");
                }
            }
        }
        public double Cuprum
        {
            get { return cuprum; }
            set
            {
                if (cuprum != value)
                {
                    cuprum = value;
                    OnPropertyChanged("Cuprum");
                }
            }
        }
        public double Cobalt
        {
            get { return cobalt; }
            set
            {
                if (cobalt != value)
                {
                    cobalt = value;
                    OnPropertyChanged("Cobalt");
                }
            }
        }
        public double Manganese
        {
            get { return manganese; }
            set
            {
                if (manganese != value)
                {
                    manganese = value;
                    OnPropertyChanged("Manganese");
                }
            }
        }
        public double Selenium
        {
            get { return selenium; }
            set
            {
                if (selenium != value)
                {
                    selenium = value;
                    OnPropertyChanged("Selenium");
                }
            }
        }
        public double Fluorine
        {
            get { return fluorine; }
            set
            {
                if (fluorine != value)
                {
                    fluorine = value;
                    OnPropertyChanged("Fluorine");
                }
            }
        }
        public double Iodine
        {
            get { return iodine; }
            set
            {
                if (iodine != value)
                {
                    iodine = value;
                    OnPropertyChanged("Iodine");
                }
            }
        }
        public double Calcium
        {
            get { return calcium; }
            set
            {
                if (calcium != value)
                {
                    calcium = value;
                    OnPropertyChanged("Calcium");
                }
            }
        }
        public double Phosphorus
        {
            get { return phosphorus; }
            set
            {
                if (phosphorus != value)
                {
                    phosphorus = value;
                    OnPropertyChanged("Phosphorus");
                }
            }
        }
        public double Magnesium
        {
            get { return magnesium; }
            set
            {
                if (magnesium != value)
                {
                    magnesium = value;
                    OnPropertyChanged("Magnesium");
                }
            }
        }
        public double Sodium
        {
            get { return sodium; }
            set
            {
                if (sodium != value)
                {
                    sodium = value;
                    OnPropertyChanged("Sodium");
                }
            }
        }
        public double Chlorine
        {
            get { return chlorine; }
            set
            {
                if (chlorine != value)
                {
                    chlorine = value;
                    OnPropertyChanged("Chlorine");
                }
            }
        }
        public double Sulfur
        {
            get { return sulfur; }
            set
            {
                if (sulfur != value)
                {
                    sulfur = value;
                    OnPropertyChanged("Sulfur");
                }
            }
        }
        public double Molybdenum
        {
            get { return molybdenum; }
            set
            {
                if (molybdenum != value)
                {
                    molybdenum = value;
                    OnPropertyChanged("Molybdenum");
                }
            }
        }
        public double Chromium
        {
            get { return chromium; }
            set
            {
                if (chromium != value)
                {
                    chromium = value;
                    OnPropertyChanged("Chromium");
                }
            }
        }
        public double Kalium
        {
            get { return kalium; }
            set
            {
                if (kalium != value)
                {
                    kalium = value;
                    OnPropertyChanged("Kalium");
                }
            }
        }

        public IngredientDB()
        { }
        public IngredientDB(string name, double protein, double fat, double carb, double a, double b1, double b2, double b3, double b4, double b5, double b6,
            double b8, double b9, double b12, double c, double d, double e, double h, double k, double ferrum, double zinc, double cuprum, double cobalt, double manganese, double selenium,
            double fluorine, double iodine, double calcium, double phosphorus, double magnesium, double sodium, double chlorine, double sulfur, double molybdenum, double chromium, double kalium)
        {
            Name = name;
            Protein = protein;
            Fat = fat;
            Carb = carb;
            A = a;
            B1 = b1;
            B2 = b2;
            B3 = b3;
            B4 = b4;
            B5 = b5;
            B6 = b6;
            B8 = b8;
            B9 = b9;
            B12 = b12;
            C = c;
            D = d;
            E = e;
            H = h;
            K = k;
            Ferrum = ferrum;
            Zinc = zinc;
            Cuprum = cuprum;
            Cobalt = cobalt;
            Manganese = manganese;
            Selenium = selenium;
            Fluorine = fluorine;
            Iodine = iodine;
            Calcium = calcium;
            Phosphorus = phosphorus;
            Magnesium = magnesium;
            Sodium = sodium;
            Chlorine = chlorine;
            Sulfur = sulfur;
            Molybdenum = molybdenum;
            Chromium = chromium;
            Kalium = kalium;
        }
        public IngredientDB(string name) : this(name, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        private Dictionary<string, double> ConvertVitaminsToDictionary(IngredientDB i)
        {
            return new Dictionary<string, double>() {
            {"a", i.A}, {"b1", i.B1 }, {"b2", i.B2 }, {"b3", i.B3 }, {"b4", i.B4 }, {"b5", i.B5 }, {"b6", i.B6 }, {"b8", i.B8 }, {"b9", i.B9 },
            {"b12", i.B12 }, {"c", i.C}, {"d", i.D}, {"e", i.E}, {"h", i.H}, {"k", i.K}, {"ferrum", i.Ferrum}, {"zinc", i.Zinc}, {"cuprum", i.Cuprum},
            {"cobalt", i.Cobalt}, {"manganese", i.Manganese }, {"selenium", i.Selenium}, {"fluorine", i.Fluorine}, {"iodine", i.Iodine}, {"calcium", i.Calcium},
            {"phosphorus", i.Phosphorus}, {"magnesium", i.Magnesium }, {"sodium", i.Sodium}, {"chlorine", i.Chlorine}, {"sulfur", i.Sulfur},
            {"molybdenum", i.Molybdenum}, {"chromium", i.Chromium},{"kalium", i.Kalium} };
        }
        private IngredientDB ConvertVitaminsDictionaryToIngredientDB(Dictionary<string, double> dict)
        {
            IngredientDB ingredient = this;
            return new IngredientDB(ingredient.Name, ingredient.Protein, ingredient.Fat, ingredient.Carb, dict["a"], dict["b1"], dict["b2"], dict["b3"], dict["b4"], dict["b5"], dict["b6"], dict["b8"], dict["b9"], dict["b12"], dict["c"], dict["d"], dict["e"], dict["h"], dict["k"], dict["ferrum"], dict["zinc"], dict["cuprum"],
             dict["cobalt"], dict["manganese"], dict["selenium"], dict["fluorine"], dict["iodine"], dict["calcium"],
             dict["phosphorus"], dict["magnesium"], dict["sodium"], dict["chlorine"], dict["sulfur"],
             dict["molybdenum"], dict["chromium"], dict["kalium"]);
        }
        public List<Pair> SplitNotNullVitaminsIntoPairs(Dictionary<string, double> elements)//получение пар ненулевых элементов
        {
            List<Pair> list = new List<Pair>();
            //разбиваем на возможные пары
            foreach (string e1 in elements.Keys)
            {
                foreach (string e2 in elements.Keys)
                {
                    if (!e1.Equals(e2))
                        list.Add(new Pair(e1, elements[e1], e2, elements[e2]));
                }
            }
            //удаляем повторы, где ключи словаря указаны в обратной последовательности
            Pair pair;
            foreach (Pair p in list)
            {
                pair = new Pair(p.B, elements[p.B], p.A, elements[p.A]);
                if (list.Contains(pair))
                    list.Remove(pair);
            }

            //удаляем пары, в которых хоть одно значение нулевое
            foreach (Pair p in list)
            {
                foreach (int i in elements.Values)
                {
                    if (p.ACount == 0 || p.BCount == 0)
                        list.Remove(p);
                }
            }

            return list;
        }
        public List<Pair> SplitNotNullVitaminsIntoPairs(IngredientDB i)
        {
            return SplitNotNullVitaminsIntoPairs(ConvertVitaminsToDictionary(i));
        }
        public bool IsBadСompatibility(string a, string b)
        {
            Dictionary<string, List<string>> badCompatibility = new Dictionary<string, List<string>>() {
            {"a", new List<string>() {"b12", "k" } },
            {"b1",new List<string>() {"b2","b4","b3","b6","b12","magnesium" } },
            {"b2", new List<string>() {"b1","b12","ferrum", "cuprum" } },
            {"b3", new List<string>() {"b1","b12"} },
            {"b4", new List<string>() {"b1" } },
            {"b5", new List<string>() {"cuprum" } },
            {"b6", new List<string>() {"b1","b12" } },
            {"b8", new List<string>()  },
            {"b9", new List<string>() {"zinc"} },
            {"b12", new List<string>() { "a", "b1", "b2", "b3", "b6", "c", "e", "ferrum", "cuprum", "molybdenum" } },
            {"c", new List<string>() { "b12", "cuprum" } },
            {"d", new List<string>() {"e"} },
            {"e", new List<string>() {"b12", "d", "k", "ferrum", "zinc", "cuprum", "magnesium"} },
            {"h", new List<string>() },
            {"k", new List<string>() { "a", "e" } },
            {"ferrum", new List<string>() { "b2", "b12", "e", "zinc", "magnesium", "calcium", "manganese", "chromium" } },//железо
            {"zinc", new List<string>() {"b9","e", "ferrum", "cuprum", "calcium", "chromium" } },//цинк
            {"cuprum", new List<string>() { "b2", "b5", "b12", "c", "e", "zinc", "selenium", "molybdenum" } },//медь
            {"cobalt", new List<string>() },//кобальт
            {"manganese", new List<string>() { "ferrum", "calcium", "phosphorus", "iodine" }  },//марганец
            {"selenium", new List<string>() { "cuprum" } },//селен
            {"fluorine", new List<string>() },//фтор
            {"iodine", new List<string>() { "calcium", "manganese" } },//йод
            {"calcium", new List<string>() { "ferrum", "phosphorus", "zinc", "iodine", "chromium", "manganese" } },//кальций
            {"phosphorus", new List<string>() { "magnesium", "calcium", "manganese" } },//фосфор
            {"magnesium", new List<string>() {"b1", "e", "ferrum", "phosphorus"}  },//магний
            {"sodium", new List<string>() },//натрий
            {"chlorine", new List<string>() },//хлор
            {"sulfur", new List<string>() },//сера
            {"molybdenum", new List<string>() {"b12", "cuprum" } },//молибден
            {"chromium", new List<string>() { "ferrum", "calcium", "zinc" } }, //хром 
            {"kalium", new List<string>() }//калий
            };
            if (badCompatibility[a].Contains(b) || badCompatibility[b].Contains(a))
                return true;
            else
                return false;
        }
        public IngredientDB MultiplyByValueForBadСompatibility(Dictionary<string, double> elements, double val)
        {
            IngredientDB ingredient = this;
            foreach (var pair in SplitNotNullVitaminsIntoPairs(elements))
            {
                if (IsBadСompatibility(pair.A, pair.B))
                {
                    elements[pair.A] *= val;
                    elements[pair.B] *= val;
                }
            }
            return ingredient.ConvertVitaminsDictionaryToIngredientDB(elements);
        }
        public IngredientDB MultiplyByValueForBadСompatibility(IngredientDB i, double val)
        {
            return MultiplyByValueForBadСompatibility(ConvertVitaminsToDictionary(i), val);
        }
        public IngredientDB MultiplyByValueForBadСompatibility(double val)
        {
            return MultiplyByValueForBadСompatibility(this, val);
        }
        public IngredientDB SumIngredients(IngredientDB i)
        {
            this.Protein += i.Protein;
            this.Fat += i.Fat;
            this.Carb += i.Carb;
            this.A += i.A;
            this.B1 += i.B1;
            this.B2 += i.B2;
            this.B3 += i.B3;
            this.B4 += i.B4;
            this.B5 += i.B5;
            this.B6 += i.B6;
            this.B8 += i.B8;
            this.B9 += i.B9;
            this.B12 += i.B12;
            this.C += i.C;
            this.D += i.D;
            this.E += i.E;
            this.H += i.H;
            this.K += i.K;
            this.Ferrum += i.Ferrum;
            this.Zinc += i.Zinc;
            this.Cuprum += i.Cuprum;
            this.Cobalt += i.Cobalt;
            this.Manganese += i.Manganese;
            this.Selenium += i.Selenium;
            this.Fluorine += i.Fluorine;
            this.Iodine += i.Iodine;
            this.Calcium += i.Calcium;
            this.Phosphorus += i.Phosphorus;
            this.Magnesium += i.Magnesium;
            this.Sodium += i.Sodium;
            this.Chlorine += i.Chlorine;
            this.Sulfur += i.Sulfur;
            this.Molybdenum += i.Molybdenum;
            this.Chromium += i.Chromium;

            return this;
        }
        public IngredientDB SumIngredients(List<IngredientDB> ingredientList)
        {
            IngredientDB sum = new IngredientDB("sum");
            foreach (var i in ingredientList)
            {
                sum = SumIngredients(i);
            }
            return sum;
        }
        //метод, устанавливающий минимальное значение возрастной категории
        public int GetAgeKey(SortedDictionary<int, IngredientDB> keyValues, int age)
        {
            List<int> keys = new List<int>();
            foreach (var i in keyValues.Keys)
                keys.Add(i);

            int ageKey = keys[0];
            if (age > keys[keyValues.Keys.Count - 1])
                ageKey = keys[keyValues.Keys.Count - 1];
            else
            {
                for (int n = 0; n < keyValues.Keys.Count - 1; n++)
                {
                    if (keys[n] == age || (keys[n] < age && keys[n + 1] > age))
                        ageKey = keys[n];
                }
            }
            return ageKey;
        }
        public IngredientDB MultiplyByRateOfPhysicalActivity(IngredientDB i, double rate)
        {
            i.Fat *= rate;
            i.Carb *= rate;
            i.Protein *= rate;

            return i;
        }
        public IngredientDB MultiplyByRate(double rate)
        {
            this.Fat *= rate;
            this.Carb *= rate;
            this.Protein *= rate;
            this.A *= rate;
            this.B1 *= rate;
            this.B2 *= rate;
            this.B3 *= rate;
            this.B4 *= rate;
            this.B5 *= rate;
            this.B6 *= rate;
            this.B8 *= rate;
            this.B9 *= rate;
            this.B12 *= rate;
            this.C *= rate;
            this.D *= rate;
            this.E *= rate;
            this.H *= rate;
            this.K *= rate;
            this.Ferrum *= rate;
            this.Zinc *= rate;
            this.Cuprum *= rate;
            this.Cobalt *= rate;
            this.Manganese *= rate;
            this.Selenium *= rate;
            this.Fluorine *= rate;
            this.Iodine *= rate;
            this.Calcium *= rate;
            this.Phosphorus *= rate;
            this.Magnesium *= rate;
            this.Sodium *= rate;
            this.Chlorine *= rate;
            this.Sulfur *= rate;
            this.Molybdenum *= rate;
            this.Chromium *= rate;

            return this;
        }
        public double SetRateOfPhysicalActivity(App.GroupOfPhysicalActivity g)
        {

            switch (g)
            {
                case App.GroupOfPhysicalActivity.I:
                    return 1.4;
                case App.GroupOfPhysicalActivity.II:
                    return 1.6;
                case App.GroupOfPhysicalActivity.III:
                    return 1.9;
                case App.GroupOfPhysicalActivity.IV:
                    return 2.2;
                case App.GroupOfPhysicalActivity.V:
                    return 2.5;
            }
            return 0;
        }
        public IngredientDB GetNormalDailyIntake(int age, bool isMale, App.GroupOfPhysicalActivity groupOfPhysicalActivity)
        {
            double rate = SetRateOfPhysicalActivity(groupOfPhysicalActivity);

            SortedDictionary<int, IngredientDB> forMale = new SortedDictionary<int, IngredientDB>() {
            { 0,new IngredientDB("m0",29,60,130,0.0004,0.0005,0.0006,0.007,0,0.002,0.0006,0,0.00006,0.0000005,0.03,0.00001,0.004,0.00001,00003,0.007,0.004,0.0005,0,0,0.000012,0.0012,0.00006,0.6,0.5,0.07,0.35,0.55,0,0,0,0)},
            { 1,new IngredientDB("m1",36,40,174,0.00045,0.0008,0.0009,0.008,0,0.0025,0.0009,0,0.0001,0.0000007,0.045,0.00001,0.004,0.00001,00003,0.01,0.005,0.0005,0,0,0.000015,0.0014,0.00007,0.8,0.7,0.08,0.5,0.8,0,0,0.000011,0.4)},
            { 2,new IngredientDB("m2",42,47,203,0.00045,0.0008,0.0009,0.008,0,0.0025,0.0009,0,0.0001,0.0000007,0.045,0.00001,0.004,0.00001,00003,0.01,0.005,0.0005,0,0,0.000015,0.0014,0.00007,0.8,0.7,0.08,0.5,0.8,0,0,0.000011,0.4)},
            { 3,new IngredientDB("m3",54,60,261,0.0005,0.0009,0.001,0.011,0,0.003,0.0012,0,0.0002,0.0000015,0.05,0.00001,0.007,0.000015,000055,0.01,0.008,0.0006,0,0,0.00002,0.002,0.0001,0.9,0.8,0.2,0.7,1.1,0,0,0.000015,0.6)},
            { 7,new IngredientDB("m7",63,70,305,0.0007,0.0011,0.0012,0.015,0,0.003,0.0015,0,0.0002,0.000002,0.06,0.00001,0.01,0.00002,00006,0.012,0.01,0.0007,0,0,0.00003,0.003,0.00012,1.1,1.1,0.25,1,1.7,0,0,0.000015,0.9)},
            { 11,new IngredientDB("m11",75,83,363,0.001,0.0013,0.0015,0.018,0,0.0035,0.0017,0,0.0004,0.000003,0.07,0.00001,0.012,0.000025,00008,0.012,0.012,0.0008,0,0,0.00004,0.004,0.00013,1.2,1.2,0.3,1.1,1.9,0,0,0.000025,1.5)},
            { 14,new IngredientDB("m14",87,97,421,0.001,0.0015,0.0018,0.02,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.015,0.00005,00012,0.015,0.012,0.001,0,0,0.00004,0.004,0.00015,1.2,1.2,0.4,1.3,2.3,0,0,0.000035,2.5)},

            { 18,new IngredientDB("18",52,57,256,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.01,0.012,0.001,0,0.002,0.00007,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 30,new IngredientDB("30",49,55,239,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.01,0.012,0.001,0,0.002,0.00007,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 40,new IngredientDB("40",47,50,217,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.01,0.012,0.001,0,0.002,0.00007,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 60,new IngredientDB("60",59,55,239,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.01,0.012,0.001,0,0.002,0.00007,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},

            };
            SortedDictionary<int, IngredientDB> forFemale = new SortedDictionary<int, IngredientDB>()
            {
            { 0,new IngredientDB("f0",29,60,130,0.0004,0.0005,0.0006,0.007,0,0.002,0.0006,0,0.00006,0.0000005,0.03,0.00001,0.004,0.00001,00003,0.007,0.004,0.0005,0,0,0.000012,0.0012,0.00006,0.6,0.5,0.07,0.35,0.55,0,0,0,0)},
            { 1,new IngredientDB("f1",36,40,174,0.00045,0.0008,0.0009,0.008,0,0.0025,0.0009,0,0.0001,0.0000007,0.045,0.00001,0.004,0.00001,00003,0.01,0.005,0.0005,0,0,0.000015,0.0014,0.00007,0.8,0.7,0.08,0.5,0.8,0,0,0.000011,0.4)},
            { 2,new IngredientDB("f2",42,47,203,0.00045,0.0008,0.0009,0.008,0,0.0025,0.0009,0,0.0001,0.0000007,0.045,0.00001,0.004,0.00001,00003,0.01,0.005,0.0005,0,0,0.000015,0.0014,0.00007,0.8,0.7,0.08,0.5,0.8,0,0,0.000011,0.4)},
            { 3,new IngredientDB("f3",54,60,261,0.0005,0.0009,0.001,0.011,0,0.003,0.0012,0,0.0002,0.0000015,0.05,0.00001,0.007,0.000015,000055,0.01,0.008,0.0006,0,0,0.00002,0.002,0.0001,0.9,0.8,0.2,0.7,1.1,0,0,0.000015,0.6)},
            { 7,new IngredientDB("f7",63,70,305,0.0007,0.0011,0.0012,0.015,0,0.003,0.0015,0,0.0002,0.000002,0.06,0.00001,0.01,0.00002,00006,0.012,0.01,0.0007,0,0,0.00003,0.003,0.00012,1.1,1.1,0.25,1,1.7,0,0,0.000015,0.9)},
            { 11,new IngredientDB("f11",69,77,334,0.0008,0.0013,0.0015,0.018,0,0.0035,0.0016,0,0.0004,0.000003,0.06,0.00001,0.012,0.000025,00007,0.015,0.012,0.0008,0,0,0.00004,0.004,0.00015,1.2,1.2,0.3,1.1,1.9,0,0,0.000025,1.5)},
            { 14,new IngredientDB("f14",75,83,363,0.0008,0.0013,0.0015,0.018,0,0.004,0.0016,0,0.0004,0.000003,0.07,0.00001,0.015,0.00005,0001,0.018,0.012,0.001,0,0,0.00004,0.004,0.00015,1.2,1.2,0.4,1.3,2.3,0,0,0.000035,2.5)},
            { 18,new IngredientDB("f18",44,48,207,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.018,0.012,0.001,0,0.002,0.000055,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 30,new IngredientDB("f30",42,45,195,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.018,0.012,0.001,0,0.002,0.000055,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 40,new IngredientDB("f40",41,43,184,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.018,0.012,0.001,0,0.002,0.000055,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)},
            { 60,new IngredientDB("f60",44,48,202,0.0009,0.0015,0.0018,0.002,0,0.005,0.002,0,0.0004,0.000003,0.09,0.00001,0.000015,0.00005,0.00012,0.018,0.012,0.001,0,0.002,0.000055,0.004,0.00015,1,0.8,0.4,1.3,2.3,0,0.00007,0.00005,2.5)}
    };

            if (isMale)
            {
                if (age < 18)
                    return forMale[GetAgeKey(forMale, age)];
                else
                    return MultiplyByRateOfPhysicalActivity(forMale[GetAgeKey(forMale, age)], rate);
            }
            else
            {
                if (age < 18)
                    return forFemale[GetAgeKey(forFemale, age)];
                else
                    return MultiplyByRateOfPhysicalActivity(forFemale[GetAgeKey(forFemale, age)], rate);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Pair
    {
        private string a;
        private string b;
        private double aCount;
        private double bCount;
        public string A
        {
            set
            {
                if (!value.Equals(null))
                    a = value;
            }
            get
            {
                return a;
            }
        }
        public string B
        {
            set
            {
                if (!value.Equals(null))
                    b = value;
            }
            get
            {
                return b;
            }
        }
        public double ACount
        {
            set
            {
                if (value >= 0)
                    aCount = value;
            }
            get
            {
                return aCount;
            }
        }
        public double BCount
        {
            set
            {
                if (value >= 0)
                    bCount = value;
            }
            get
            {
                return bCount;
            }
        }

        public Pair(string a, double aCount, string b, double bCount)
        {
            this.A = a;
            this.ACount = aCount;
            this.B = b;
            this.BCount = bCount;
        }
        public Pair(string a, string b) : this(a, 0, b, 0)
        {

        }
    }


    [Table("IngredientsAndWeights")]
    public class IngredientAndWeightDB : INotifyPropertyChanged
    {
        //private int id;
        private int ingredientId;
        private string name;
        private double weight;
        private int recipeId;

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [ForeignKey(typeof(IngredientDB)), Column("ingredient_id")]
        public int IngredientId
        {
            get { return ingredientId; }
            set
            {
                if (ingredientId != value)
                {
                    ingredientId = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        [Ignore]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        [Column("ingredient_weight")]
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        [ForeignKey(typeof(RecipeDB)), Column("recipe_id")]
        public int RecipeId
        {
            get { return recipeId; }
            set
            {
                if (recipeId != value)
                {
                    recipeId = value;
                    OnPropertyChanged("RecipeId");
                }
            }
        }

        public IngredientAndWeightDB(int ingredientId, double weight)
        {
            IngredientId = ingredientId;
            Name = App.Db.GetIngredientName(ingredientId);
            Weight = weight;
        }
        public IngredientAndWeightDB(int ingredientId)
        {
            IngredientId = ingredientId;
            Name = App.Db.GetIngredientName(ingredientId);
        }

        public IngredientAndWeightDB()
        {
        }
        public void SetIngredientName()
        {
            if (IngredientId != 0)
                Name = App.Db.GetIngredientName(IngredientId);
            else
                Name = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    [Table("Recipes")]
    public class RecipeDB : INotifyPropertyChanged
    {
        private string name;
        private List<IngredientAndWeightDB> ingredients;
        private string description;
        private double weight;

        [PrimaryKey, AutoIncrement, Column("recipe_id")]
        public int Id { get; set; }
        [Column("recipe_name")]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<IngredientAndWeightDB> Ingredients
        {
            get { return ingredients; }
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        [Column("recipe_weight")]
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }

        public RecipeDB()
        {
            Name = "";
            Ingredients = new List<IngredientAndWeightDB>();
            Description = "";
            Weight = 0;
        }

        public ObservableCollection<IngredientAndWeightDB> ConvertToObservableCollection()
        {
            ObservableCollection<IngredientAndWeightDB> Ingredients = new ObservableCollection<IngredientAndWeightDB>();
            foreach (var i in this.Ingredients)
            {
                Ingredients.Add(new IngredientAndWeightDB { IngredientId = i.IngredientId, Name = i.Name, Weight = i.Weight, RecipeId = i.RecipeId });
            }
            return Ingredients;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    [Table("Meals")]
    public class MealDB : INotifyPropertyChanged
    {
        private List<MealRecipeDB> recipes;
        private DateTime dateTime;
        private string name;

        [PrimaryKey, AutoIncrement, Column("meal_id")]
        public int MealId { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MealRecipeDB> Recipes
        {
            get { return recipes; }
            set
            {
                if (recipes != value)
                {
                    recipes = value;
                    OnPropertyChanged("Recipes");
                }
            }
        }
        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    OnPropertyChanged("DateTime");
                }
            }
        }

        public ObservableCollection<MealRecipeDB> ConvertToObservableCollection()
        {
            ObservableCollection<MealRecipeDB> RecipeList = new ObservableCollection<MealRecipeDB>();
            if (Recipes != null)
                foreach (var r in this.Recipes)
                    RecipeList.Add(new MealRecipeDB { RecipeId = r.RecipeId, Name = r.Name, Weight = r.Weight, MealId = r.MealId });

            return RecipeList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public IngredientDB GetSumOfIngredients(double rateOfBadСompatibility)
        {
            IngredientDB sumOfIngredients = new IngredientDB();
            List<IngredientDB> list = new List<IngredientDB>();

            foreach (var r in Recipes)
            {
                foreach (var i in App.Db.GetRecipe(r.Id).Ingredients)
                {
                    list.Add(App.Db.GetIngredient(i.Id).MultiplyByRate(i.Weight * r.Weight / App.Db.GetRecipe(r.Id).Weight));
                }
            }

            sumOfIngredients.SumIngredients(list);

            return sumOfIngredients.MultiplyByValueForBadСompatibility(rateOfBadСompatibility);
        }
    }

    [Table("MealRecipe")]
    public class MealRecipeDB : INotifyPropertyChanged
    {
        private int mealId;
        private string name;
        private double weight;
        private int recipeId;


        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [ForeignKey(typeof(MealDB)), Column("meal_id")]
        public int MealId
        {
            get { return mealId; }
            set
            {
                if (mealId != value)
                {
                    mealId = value;
                    OnPropertyChanged("MealId");
                }
            }
        }
        [ForeignKey(typeof(RecipeDB)), Column("recipe_id")]
        public int RecipeId
        {
            get { return recipeId; }
            set
            {
                if (recipeId != value)
                {
                    recipeId = value;
                    OnPropertyChanged("RecipeId");
                }
            }
        }
        [Ignore]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        [Column("recipe_weight")]
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }


        public MealRecipeDB()
        {
        }

        public void SetRecipeName()
        {
            if (RecipeId != 0)
                Name = App.Db.GetRecipeName(RecipeId);
            else
                Name = "";

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class DBRepository
    {
        SQLiteConnection database;
        public DBRepository(string DBPath)
        {
            database = new SQLiteConnection(DBPath);
            database.CreateTable<IngredientDB>();//эту базу должны запросить с глобального сервера
            database.CreateTable<IngredientAndWeightDB>();
            database.CreateTable<RecipeDB>();
            database.CreateTable<MealDB>();
            database.CreateTable<MealRecipeDB>();
        }

        public void DropTabels()
        {
            database.DropTable<IngredientDB>();
            database.DropTable<IngredientAndWeightDB>();
            database.DropTable<RecipeDB>();
            database.DropTable<MealDB>();
            database.DropTable<MealRecipeDB>();
        }
        public void ClearTable(string tableName)
        {
            database.Execute("DELETE FROM " + tableName);
        }

        public IEnumerable<IngredientDB> GetIngredients()
        {
            return database.Table<IngredientDB>(); ;
        }
        public IngredientDB GetIngredient(int id)
        {
            try
            {
                return database.Get<IngredientDB>(id);
            }
            catch
            {
                return null;
            }
        }
        public string GetIngredientName(int id)
        {
            return GetIngredient(id).Name;
        }
        public string GetIngredientName(IngredientAndWeightDB i)
        {
            return GetIngredientName(i.IngredientId);
        }
        public void DeleteIngredient(int id)
        {
            database.Delete<IngredientDB>(id);
        }
        public bool IsIngredientExistInDB(int id)
        {
            if (GetIngredient(id) != null)
                return true;
            else
                return false;
        }
        public void SaveIngredient(IngredientDB i)
        {
            if (IsIngredientExistInDB(i.Id))
            {
                database.Update(i);
            }
            else
            {
                database.Insert(i);
            }
        }
        public void SaveIngredient(string name, double protein, double fat, double carb, double a, double b1, double b2, double b3, double b4, double b5, double b6,
            double b8, double b9, double b12, double c, double d, double e, double h, double k, double ferrum, double zinc, double cuprum, double cobalt, double manganese, double selenium,
            double fluorine, double iodine, double calcium, double phosphorus, double magnesium, double sodium, double chlorine, double sulfur, double molybdenum, double chromium, double kalium)
        {
            SaveIngredient(new IngredientDB(name, protein, fat, carb, a, b1, b2, b3, b4, b5, b6, b8, b9, b12, c, d, e, h, k, ferrum, zinc, cuprum,
                cobalt, manganese, selenium, fluorine, iodine, calcium, phosphorus, magnesium, sodium, chlorine, sulfur, molybdenum, chromium, kalium));
        }


        public List<RecipeDB> GetRecipes()
        {
            return database.GetAllWithChildren<RecipeDB>(null, true);
        }
        public RecipeDB GetRecipe(int id)
        {
            try
            {
                return database.GetWithChildren<RecipeDB>(id, true);
            }
            catch
            {
                return null;
            }

        }
        public int GetMaxRecipeId()
        {
            int maxValue = 0;

            foreach (var r in GetRecipes())
            {
                if (r.Id > maxValue)
                    maxValue = r.Id;
            }

            return maxValue;
        }
        public string GetRecipeName(int id)
        {
            return GetRecipe(id).Name;
        }
        public List<IngredientAndWeightDB> GetIngredientsAndWeightsFromRecipe(int id)
        {
            RecipeDB recipe = database.GetWithChildren<RecipeDB>(id, true);
            return recipe.Ingredients;
        }
        public IngredientAndWeightDB GetIngredientAndWeightFromRecipe(int recipeId, int ingredientId)
        {
            return database.Query<IngredientAndWeightDB>("SELECT * FROM IngredientsAndWeights WHERE recipe_id =? AND ingredient_id =?", recipeId, ingredientId)[0];
        }
        public double GetKiloCalories(int id)
        {
            double kcal = 0;

            foreach (var i in GetIngredientsAndWeightsFromRecipe(id))
            {
                var ingredient = GetIngredient(i.IngredientId);
                kcal += (ingredient.Fat * 9) + (ingredient.Carb + ingredient.Protein) * 4;
            }

            return kcal;
        }

        public void DeleteRecipe(int id)
        {
            database.Delete<RecipeDB>(id);
            database.Execute("DELETE FROM IngredientsAndWeights WHERE recipe_id =?", id);
            database.Execute("DELETE FROM Meals WHERE recipe_id =?", id);
        }
        public void DeleteRecipe(RecipeDB r)
        {
            database.DeleteAll(r.Ingredients);
            database.Delete(r);
        }
        public bool IsRecipeExistInDB(int id)
        {
            if (GetRecipe(id) != null)
                return true;
            else
                return false;
        }
        public void SaveRecipe(RecipeDB r)
        {
            if (IsRecipeExistInDB(r.Id))
            {
                database.UpdateWithChildren(r);
                database.DeleteAll(r.Ingredients);
                database.InsertAll(r.Ingredients);

            }
            else
            {
                database.InsertWithChildren(r, true);
            }
        }
        public List<RecipeDB> SearchRecipes(string name)
        {
            List<RecipeDB> recipes = new List<RecipeDB>();
            foreach (var r in GetRecipes())
            {
                if (r.Name.Contains(name))
                    if (r.Name.Contains(name))
                    {
                        recipes.Add(r);
                    }
            }

            return recipes;
        }
        public List<RecipeDB> SearchRecipes(List<string> ingredientNameList, string name)
        {
            List<RecipeDB> recipes = new List<RecipeDB>();
            if (SearchRecipes(name).Count > 0)
            {
                foreach (var r in SearchRecipes(name))
                {
                    foreach (var i in r.Ingredients)
                    {
                        int count = 0;
                        foreach (var n in ingredientNameList)
                        {

                            if (i.Name.Equals(n))
                            {
                                count++;
                            }
                        }
                        if (count == ingredientNameList.Count)
                        {
                            recipes.Add(r);
                        }
                    }
                }
            }

            return recipes;
        }
        public List<RecipeDB> SearchRecipes(List<string> ingredientNameList)
        {
            List<RecipeDB> recipes = new List<RecipeDB>();

            foreach (var r in GetRecipes())
            {
                foreach (var i in r.Ingredients)
                {
                    int count = 0;
                    foreach (var n in ingredientNameList)
                    {

                        if (i.Name.Equals(n))
                        {
                            count++;
                        }
                    }
                    if (count == ingredientNameList.Count)
                    {
                        recipes.Add(r);
                    }
                }
            }

            return recipes;
        }

        public List<MealDB> GetMeals()
        {
            return database.GetAllWithChildren<MealDB>(null, true);
        }
        public List<MealDB> GetMeals(DateTime date)
        {
            return database.GetAllWithChildren<MealDB>(d => d.DateTime == date, true);
        }
        public List<MealDB> GetMeals(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
            {
                DateTime date = date1;
                date1 = date2;
                date2 = date;
            }
            List<MealDB> Meals = new List<MealDB>();
            for (DateTime date = date1.Date; date <= date2.Date; date = date.AddDays(1))
            {
                List<MealDB> mealsInDay = database.GetAllWithChildren<MealDB>(d => d.DateTime == date, true);
                foreach (var m in mealsInDay)
                    Meals.Add(m);
            }
            return Meals;
        }
        public IngredientDB GetSumOfIngredients(DateTime date1, DateTime date2, double rateOfBadСompatibility)
        {
            IngredientDB ingredient = new IngredientDB();
            foreach (var meal in GetMeals(date1, date2))
            {
                ingredient.SumIngredients(meal.GetSumOfIngredients(rateOfBadСompatibility));
            }
            return ingredient;
        }
        public IngredientDB GetSumOfIngredients(DateTime date1, DateTime date2)
        {
            return GetSumOfIngredients(date1, date2, 0.5);
        }

        public MealDB GetMeal(int id)
        {
            try
            {
                return database.GetWithChildren<MealDB>(id, true);
            }
            catch
            {
                return null;
            }

        }
        public bool IsMealExistInDB(int id)
        {
            return (GetMeal(id) != null) ? true : false;
        }
        public void SaveMeal(MealDB m)
        {
            if (IsMealExistInDB(m.MealId))
            {
                database.UpdateWithChildren(m);
                database.DeleteAll(m.Recipes);
                database.InsertAll(m.Recipes);

            }
            else
            {
                database.InsertWithChildren(m, true);
            }
        }
        public void DeleteMeal(MealDB m)
        {
            database.Delete(m);
            database.DeleteAll(m.Recipes);
        }
    }
}
