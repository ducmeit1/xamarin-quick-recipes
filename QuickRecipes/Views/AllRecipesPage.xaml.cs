using System;
using System.Collections.Generic;
using QuickRecipes.ViewModels;
using Xamarin.Forms;

namespace QuickRecipes.Views
{
    public partial class AllRecipesPage : ContentPage
    {
        AllRecipeViewModel vm;

        public AllRecipesPage()
        {
            InitializeComponent();
            Title = "All Recipes";
            BindingContext = vm = new AllRecipeViewModel();
        }

		async void ViewMore_Clicked(object sender, System.EventArgs e)
		{
			var item = sender as MenuItem;
			var id = int.Parse(item.CommandParameter.ToString());
			await Navigation.PushAsync(new RecipeDetailPage(id));
            AllRecipesListView.SelectedItem = null;
		}

        void MoveToAddPage_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new AddRecipePage());
        }

		async void RemoveItem_Clicked(object sender, EventArgs e)
		{
			var item = sender as MenuItem;
			var id = int.Parse(item.CommandParameter.ToString());
            await vm.RemoveItemAsync(id);
            AllRecipesListView.SelectedItem = null;
		}

        void EditItem_Clicked(object sender, EventArgs e) 
        {
            var item = sender as MenuItem;
            var id = int.Parse(item.CommandParameter.ToString());
            Navigation.PushAsync(new EditRecipePage(id));
            AllRecipesListView.SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.Recipes.Count == 0) {
                vm.LoadItemsCommand.Execute(null);
            }
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            AllRecipesListView.SelectedItem = null;
        }
    }
}
