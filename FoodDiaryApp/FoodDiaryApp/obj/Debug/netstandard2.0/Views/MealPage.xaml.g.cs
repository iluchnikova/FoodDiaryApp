//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("FoodDiaryApp.Views.MealPage.xaml", "Views/MealPage.xaml", typeof(global::FoodDiaryApp.Views.MealPage))]

namespace FoodDiaryApp.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\MealPage.xaml")]
    public partial class MealPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.DatePicker meal_DataPicker;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Picker picker;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView mealNameListView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(MealPage));
            meal_DataPicker = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.DatePicker>(this, "meal_DataPicker");
            picker = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Picker>(this, "picker");
            mealNameListView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "mealNameListView");
        }
    }
}
