﻿using System;
using System.Collections.Generic;
using QuickRecipes.Models;
using QuickRecipes.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRecipes.Views
{
    public partial class QuickRecipePage : ContentPage
    {
        BrowseViewModel vm;

        public QuickRecipePage()
        {
            InitializeComponent();
            Title = "Browse";
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = vm = new BrowseViewModel();
        }

        void Clear_Clicked(object sender, EventArgs e)
        {
            IngredientSearch.Text = "";
            IngredientSearch.Focus();
        }

        async void Suggestion_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ingredient = e.Item as string;
            await vm.AddIngredientAsync(ingredient);
            SuggestionListView.SelectedItem = null;
            SuggestionListView.IsVisible = false;
            IngredientsListView.IsVisible = true;
            IngredientSearch.Text = "";
            await Navigation.PushAsync(new ResultSearchPage(ingredient));
        }

        void Ingredients_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ingredient = e.Item as string;
            IngredientSearch.Text = ingredient;
            IngredientsListView.SelectedItem = null;
        }


        async void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vm.IsBusy)
                return;
            var keyword = IngredientSearch.Text;
            if (keyword.Length > 1)
            {
                vm.IsBusy = true;
                var suggestionList = await vm.SearchIngredientAsync(keyword);
                SuggestionListView.ItemsSource = suggestionList;
                SuggestionListView.IsVisible = true;
                IngredientsListView.IsVisible = false;
                vm.IsBusy = false;
            }
            else
            {
                IngredientsListView.IsVisible = true;
                SuggestionListView.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.MyIngredients.Count == 0)
            {
                vm.LoadMyIngredientsCommand.Execute(null);
            }
        }
    }
}
