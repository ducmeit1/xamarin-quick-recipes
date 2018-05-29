using System.Threading.Tasks;
using MvvmHelpers;
using QuickRecipes.Models;
using QuickRecipes.Services;
using Xamarin.Forms;
using System.Collections.Generic;

namespace QuickRecipes.ViewModels
{
    public class BrowseViewModel : ObservableObject
    {
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ObservableRangeCollection<string> MyIngredients { get; set; }

        public IRecipeDataStore<Recipe> RecipeDataStore = DependencyService.Get<IRecipeDataStore<Recipe>>();

        public IMyIngreditentDataStore<string> MyIngredientsDataStore = DependencyService.Get<IMyIngreditentDataStore<string>>();

        public Command LoadMyIngredientsCommand { get; }

        public Command AddItemsCommand { get; }


        public BrowseViewModel()
        {
            MyIngredients = new ObservableRangeCollection<string>();
            LoadMyIngredientsCommand = new Command(async () => await ExecuteLoadMyIngredients());
        }

        public async Task AddIngredientAsync(string ingredient)
        {
            if (!MyIngredients.Contains(ingredient))
            {
                MyIngredients.Add(ingredient);
                await MyIngredientsDataStore.AddIngredientAsync(ingredient);
            }
        }

        async Task ExecuteLoadMyIngredients()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var items = await MyIngredientsDataStore.GetIngredientsListAsync();
                if (items != null && items.Count > 0)
                {
                    MyIngredients.Clear();
                    MyIngredients.ReplaceRange(items);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<List<string>> SearchIngredientAsync(string keyword) {
            var recipesList = await RecipeDataStore.GetRecipesListAsync();
			var suggestionList = new List<string>();

			foreach (Recipe recipe in recipesList)
			{
				foreach (string ingredient in recipe.Ingredients)
				{
					if (ingredient.ToLower().Contains(keyword.ToLower()))
					{
						if (suggestionList.Contains(ingredient)) continue;
						suggestionList.Add(ingredient);
					}
				}
			}
            return await Task.FromResult(suggestionList);
        }

    }
}
