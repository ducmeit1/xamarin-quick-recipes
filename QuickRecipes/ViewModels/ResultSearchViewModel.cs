using MvvmHelpers;
using QuickRecipes.Models;
using QuickRecipes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickRecipes.ViewModels
{
    class ResultSearchViewModel : ObservableObject
    {
        public ObservableRangeCollection<Recipe> Recipes { set; get; }

        public string Ingredient { set; get; }

        private IRecipeDataStore<Recipe> DataStore => DependencyService.Get<IRecipeDataStore<Recipe>>();

        public Command GetItemsCommand { get; private set; }


        public ResultSearchViewModel(string ingredients)
        {
            Recipes = new ObservableRangeCollection<Recipe>();
            GetItemsCommand = new Command(async () => await ExecuteGetItemsCommand(ingredients));
            GetItemsCommand.Execute(null);
            Ingredient = ingredients;
        }

        private async Task ExecuteGetItemsCommand(string ingredients)
        {
            Recipes.Clear();
            var items = await GetItemsAsync(ingredients);
            Recipes.ReplaceRange(items);
        }


        async Task<List<Recipe>> GetItemsAsync(string ingredients)
        {
            var items = await DataStore.GetRecipesListAsync();
            var recipes = new List<Recipe>();
            foreach (Recipe r in items)
            {
                if (r.Ingredients.Contains(ingredients))
                {
                    if (recipes.Contains(r)) continue;
                    recipes.Add(r);
                }
            }
            return await Task.FromResult(recipes);
        }
    }
}
