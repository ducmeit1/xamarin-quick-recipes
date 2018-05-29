using System;
using QuickRecipes.Models;
using MvvmHelpers;
using QuickRecipes.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace QuickRecipes.ViewModels
{
    public class RecipeDetailViewModel : ObservableObject
    {
        public Recipe Recipe { get; set; }

        private IRecipeDataStore<Recipe> DataStore => DependencyService.Get<IRecipeDataStore<Recipe>>();

        public Command GetItemDetailCommand { get; private set; }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public RecipeDetailViewModel(int id)
        {
            GetItemDetailCommand = new Command(async () => await GetRecipeDetailAsync(id));
            GetItemDetailCommand.Execute(null);
        }


        public async Task GetRecipeDetailAsync(int id)
        {
            var _item = await DataStore.GetRecipeAsync(id);
            Recipe = _item;
        }
    }
}
