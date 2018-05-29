using System;
using System.Collections.Generic;
using QuickRecipes.ViewModels;
using Xamarin.Forms;

namespace QuickRecipes.Views
{
    public partial class MyFavouritesPage : ContentPage
    {

        MyFavouritesViewModel vm;

        public MyFavouritesPage()
        {
            InitializeComponent();
            Title = "My Favourites";
            BindingContext = vm = new MyFavouritesViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }


	    void ViewMore_Clicked(object sender, EventArgs e)
		{
            var item = sender as MenuItem;
			var id = int.Parse(item.CommandParameter.ToString());
            Navigation.PushAsync(new RecipeDetailPage(id));
            MyFavouritesListView.SelectedItem = null;
		}


        async void RemoveItem_Clicked(object sender, EventArgs e) 
        {
            var item = sender as MenuItem;
			var id = int.Parse(item.CommandParameter.ToString());
            await vm.RemoveFavouriteAsync(id);
			MyFavouritesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadItemsCommand.Execute(null);
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            MyFavouritesListView.SelectedItem = null;
        }
    }
}
