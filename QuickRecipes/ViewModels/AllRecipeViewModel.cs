using System.Threading.Tasks;
using MvvmHelpers;
using System.Linq;
using QuickRecipes.Models;
using QuickRecipes.Services;
using QuickRecipes.Views;
using Xamarin.Forms;

namespace QuickRecipes.ViewModels
{
    public class AllRecipeViewModel : ObservableObject
    {
        public IRecipeDataStore<Recipe> DataStore => DependencyService.Get<IRecipeDataStore<Recipe>>();

        public ObservableRangeCollection<Recipe> Recipes { set; get; }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Command LoadItemsCommand { get; }

        public AllRecipeViewModel()
        {
            Recipes = new ObservableRangeCollection<Recipe>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<AddRecipePage, Recipe>(this, "AddItem", async (obj, item) => 
            {
                var _item = item as Recipe;
                int currentTotal = DataStore.NumberRecipesInRecipesList();
                do
                {
                    _item.Id = currentTotal++;
                } while (await DataStore.GetRecipeAsync(_item.Id) != null);
                Recipes.Add(_item);
                await DataStore.AddRecipeAsync(_item);
                await App.Current.MainPage.DisplayAlert("Successful", "Added new recipe", "OK");
            });

            MessagingCenter.Subscribe<EditRecipePage, Recipe>(this, "EditItem", async (obj, item) =>
			{
				var _item = item as Recipe;
                await DataStore.UpdateRecipeAsync(_item);
                var _currentItem = Recipes.Where((Recipe args) => args.Id == _item.Id).FirstOrDefault();
                Recipes.Remove(_currentItem);
                Recipes.Add(_item);
			});
        }

		public async Task<bool> RemoveItemAsync(int id)
		{
            var _item = await DataStore.GetRecipeAsync(id);
            if (_item != null)
            await DataStore.DeleteRecipeAsync(_item);
            Recipes.Remove(_item);
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
                var items = await DataStore.GetRecipesListAsync();
                if (items != null && items.Count() > 0)
                {
                    Recipes.Clear();
                    Recipes.ReplaceRange(items);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
