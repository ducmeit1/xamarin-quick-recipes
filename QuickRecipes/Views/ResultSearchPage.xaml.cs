using QuickRecipes.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuickRecipes.Views
{
    public partial class ResultSearchPage : ContentPage
    {
        ResultSearchViewModel vm;

        public ResultSearchPage()
        {
            InitializeComponent();
        }

        public ResultSearchPage(string ingredient)
        {
            InitializeComponent();
            BindingContext = vm = new ResultSearchViewModel(ingredient);
            Title = "Recipes with " + vm.Ingredient;
        }

        void ViewMore_Clicked(object sender, System.EventArgs e)
        {
            var item = sender as MenuItem;
            var id = int.Parse(item.CommandParameter.ToString());
            Navigation.PushAsync(new RecipeDetailPage(id));
            ResultSearchListView.SelectedItem = null;
        }

        void AddFavourite_Clicked(object sender, EventArgs e)
        {
            var _item = sender as MenuItem;
            var id = _item.CommandParameter.ToString();
            MessagingCenter.Send(this, "AddFavourite", id);
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            ResultSearchListView.SelectedItem = null;
        }
    }
}