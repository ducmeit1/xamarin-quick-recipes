using QuickRecipes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickRecipes.DataStore
{
    class MyIngredientsDataStore : IMyIngreditentDataStore<string>
    {
        const string FILE_INGREDIENTS_PATH = "myingredients.json";

        ISaveAndLoad FileServices => DependencyService.Get<ISaveAndLoad>();

        List<string> myIngredients;

        public MyIngredientsDataStore()
        {
            myIngredients = new List<string>();
        }

        public async Task<bool> AddIngredientAsync(string item)
        {
            myIngredients.Add(item);
            await SaveIngredientsListToFileAsync();
            return await Task.FromResult(true);
        }


        public async Task<List<string>> GetIngredientsListAsync()
        {
            if (FileServices.FileIsExist(FILE_INGREDIENTS_PATH))
            {
                var text = await FileServices.LoadTextAsync(FILE_INGREDIENTS_PATH);
                var items = JsonConvert.DeserializeObject<List<string>>(text);
                if (items != null)
                {
                    myIngredients.Clear();
                    myIngredients = items;
                }
            }
            return await Task.FromResult(myIngredients);
        }

        private async Task SaveIngredientsListToFileAsync()
        {
            var json = JsonConvert.SerializeObject(myIngredients);
            await FileServices.SaveTextAsync(FILE_INGREDIENTS_PATH, json);
        }
    }
}
