using QuickRecipes.ViewModels;
using System;
using Xamarin.Forms;

namespace QuickRecipes.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        RecipeDetailViewModel vm;

        public RecipeDetailPage()
        {
            InitializeComponent();
        }


        public RecipeDetailPage(int id)
        {
            InitializeComponent();
            BindingContext = vm = new RecipeDetailViewModel(id);
            Title = vm.Recipe.DishName;
        }

        void AddFavourite_Clicked(object sender, System.EventArgs e)
        {
            var _item = sender as ToolbarItem;
            var id = _item.CommandParameter.ToString();
            MessagingCenter.Send(this, "AddFavourite", id);
        }

        void EditItem_Clicked(object sender, EventArgs e)
        {
            var item = sender as ToolbarItem;
            var id = int.Parse(item.CommandParameter.ToString());
            Navigation.RemovePage(this);
            Navigation.PushAsync(new EditRecipePage(id));
        }
    }
}
