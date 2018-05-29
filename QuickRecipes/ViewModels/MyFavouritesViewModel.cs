using System;
using System.Threading.Tasks;
using MvvmHelpers;
using QuickRecipes.Models;
using QuickRecipes.Services;
using Xamarin.Forms;
using System.Linq;
using QuickRecipes.Views;

namespace QuickRecipes.ViewModels
{
    public class MyFavouritesViewModel : ObservableObject
    {

        public ObservableRangeCollection<Recipe> FavouriteRecipes { set; get; }

		public IRecipeDataStore<Recipe> DataStore => DependencyService.Get<IRecipeDataStore<Recipe>>();

		bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        bool _isListEmpty;
        public bool IsListEmpty
        {
            get { return _isListEmpty; }
            set {SetProperty(ref _isListEmpty, value);}
        }

        public Command LoadItemsCommand { get; }

        public MyFavouritesViewModel()
        {
            FavouriteRecipes = new ObservableRangeCollection<Recipe>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            IsListEmpty = true;
            MessagingCenter.Subscribe<RecipeDetailPage, string>(this, "AddFavourite", async (obj, item) =>
            {
				var id = item as string;
				var _item = await DataStore.GetRecipeAsync(int.Parse(id));
                var _currentItem = FavouriteRecipes.Where((Recipe args) => args.Id == _item.Id).FirstOrDefault();
                if (_currentItem != null)
                {
                    if (_currentItem.IsMyFavourite == true)
                    {
                        await App.Current.MainPage.DisplayAlert("Unsuccessful", "This recipe is already exist in your favourites list!", "OK");
                        return;
                    }
                }
                _item.IsMyFavourite = true;
                await DataStore.UpdateFavouriteAsync(_item, true);
                FavouriteRecipes.Add(_item);
                await App.Current.MainPage.DisplayAlert("Succesfully", "This recipe is added to your favourites list", "OK");
                IsListEmpty = false;
            });
            MessagingCenter.Subscribe<ResultSearchPage, string>(this, "AddFavourite", async (obj, item) =>
			{
                var id = item as string;
                var _item = await DataStore.GetRecipeAsync(int.Parse(id));
				var _currentItem = FavouriteRecipes.Where((Recipe args) => args.Id == _item.Id).FirstOrDefault();
                if (_currentItem != null)
                {
                    if (_currentItem.IsMyFavourite == true)
                    {
                        await App.Current.MainPage.DisplayAlert("Unsuccessful", "This recipe is already exist in your favourites list!", "OK");
                        return;
                    }
                }
				_item.IsMyFavourite = true;
				await DataStore.UpdateFavouriteAsync(_item, true);
				FavouriteRecipes.Add(_item);
				await App.Current.MainPage.DisplayAlert("Succesfully", "This recipe is added to your favourites list", "OK");
				IsListEmpty = false;
			});
                                      

		}
        

        public async Task<bool> RemoveFavouriteAsync(int id)
        {
            var _item = await DataStore.GetRecipeAsync(id);
            if (_item != null)
            {
                await DataStore.UpdateFavouriteAsync(_item, false);
                FavouriteRecipes.Remove(_item);
                if (FavouriteRecipes.Count == 0) IsListEmpty = true;
            }
            return await Task.FromResult(true);
        }


        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            try
            {
                var items = await DataStore.GetFavouriteRecipesListAsync();
                FavouriteRecipes.Clear();
                if (items.Count() > 0)
                {
                    FavouriteRecipes.ReplaceRange(items);
                }
                if (FavouriteRecipes.Count > 0) IsListEmpty = false;
                else IsListEmpty = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
