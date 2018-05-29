using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickRecipes.Models;
using Xamarin.Forms;
using System.Linq;
using QuickRecipes.Services;

namespace QuickRecipes.DataStore
{
    public class RecipeDataStore : IRecipeDataStore<Recipe>
    {

        const string FILE_RECIPES_PATH = "recipes.json";

        ISaveAndLoad FileServices => DependencyService.Get<ISaveAndLoad>();

        List<Recipe> recipesList;

        public RecipeDataStore()
        {
            recipesList = new List<Recipe>();
        }

        private List<Recipe> GetDefaultRecipesList
        {
            get {
                var tmpRecipes = new List<Recipe>();

                Recipe r1 = new Recipe();
                //recipe 1
                r1.Id = 1;
                r1.DishName = "Sausage Pasta";
                r1.Rate = 4;
                r1.ImageURL = "http://images.media-allrecipes.com/userphotos/560x315/2430433.jpg";
                r1.Detail = "\"This is a super easy recipe to throw together and, with the addition of different vegetables, can make lots of variations.\"";
                r1.Ingredients = new List<string>()
            {
                "Pasta", "Olive oil", "Spicy italian sausage", "Onion", "Garlic", "Chicken broth", "Dried basil", "Tomato", "Spinach", "Parmesan cheese"
            };
                r1.Prep = "15 minutes";
                r1.Cook = "30 minutes";
                r1.ReadyIn = "45 minutes";
                r1.Directions = "1. Bring a large pot of lightly salted water to a boil. Add pasta and cook for 8 to 10 minutes or until al dente; drain and reserve." +
                    "\n\n2.In a large skillet, heat oil and sausage; cook through until no longer pink. During the last 5 minutes of cooking, add onion and garlic to skillet. Add broth, basil and tomatoes with liquid." +
                    "\n\n3.Cook over medium heat for 5 minutes to slightly reduce. Add chopped spinach; cover skillet and simmer on reduced heat until spinach is tender." +
                    "\n\n4.Add pasta to skillet and mix together. Sprinkle with cheese and serve immediately.";
                tmpRecipes.Add(r1);

                Recipe r2 = new Recipe();
                //recipe 2
                r2.Id = 2;
                r2.DishName = "Fluffy Pancakes";
                r2.Rate = 5;
                r2.ImageURL = "http://images.media-allrecipes.com/userphotos/560x315/4469463.jpg";
                r2.Detail = "\"Tall and fluffy. These pancakes are just right. Topped with strawberries and whipped cream, they are impossible to resist.\"";
                r2.Ingredients = new List<string>()
            {
                "Milk", "White vinegar", "All-purpose flour", "White sugar", "Baking powder", "Baking soda", "Salt", "Egg", "Butter", "Cooking spray"
            };
                r2.Prep = "10 minutes";
                r2.Cook = "10 minutes";
                r2.ReadyIn = "25 minutes";
                r2.Directions = "1. Combine milk with vinegar in a medium bowl and set aside for 5 minutes to \"sour\"." +
                    "\n\n2. Combine flour, sugar, baking powder, baking soda, and salt in a large mixing bowl. Whisk egg and butter into \"soured\" milk. Pour the flour mixture into the wet ingredients and whisk until lumps are gone." +
                    "\n\n3. Heat a large skillet over medium heat, and coat with cooking spray. Pour 1/4 cupfuls of batter onto the skillet, and cook until bubbles appear on the surface. Flip with a spatula, and cook until browned on the other side.";

                tmpRecipes.Add(r2);

                Recipe r3 = new Recipe();
                //recipe 3
                r3.Id = 3;
                r3.DishName = "Baked Garlic Parmesan Chicken";
                r3.Rate = 4;
                r3.ImageURL = "http://images.media-allrecipes.com/userphotos/560x315/618489.jpg";
                r3.Detail = "\"A wonderful baked chicken recipe that's quick and easy! Using just a few handy ingredients, create a delicious main dish, that also makes great leftovers - if there are any! Serve with a salad and pasta or rice for a quick, scrumptious dinner.\"";
                r3.Ingredients = new List<string>()
            {
                "Olive oil", "Garlic", "Dried bread crumbs", "Parmesan cheese", "Dried basil", "Ground black pepper", "Boneless chicken breast"
            };
                r3.Prep = "15 minutes";
                r3.Cook = "30 minutes";
                r3.ReadyIn = "45 minutes";
                r3.Directions = "1. Preheat oven to 350 degrees F (175 degrees C). Lightly grease a 9x13 inch baking dish." +
                    "\n\n2. In a bowl, blend the olive oil and garlic. In a separate bowl, mix the bread crumbs, Parmesan cheese, basil, and pepper. Dip each chicken breast in the oil mixture, then in the bread crumb mixture. Arrange the coated chicken breasts in the prepared baking dish, and top with any remaining bread crumb mixture." +
                    "\n\n3. Bake 30 minutes in the preheated oven, or until chicken is no longer pink and juices run clear.";

                tmpRecipes.Add(r3);

                Recipe r4 = new Recipe();
                //recipe 4
                r4.Id = 4;
                r4.DishName = "Baked Omelet";
                r4.Rate = 4;
                r4.ImageURL = "http://images.media-allrecipes.com/userphotos/560x315/2263552.jpg";
                r4.Detail = "\"This is a great Christmas breakfast, brunch dish or company breakfast!\"";
                r4.Ingredients = new List<string>()
            {
                "Egg", "Milk", "Salt", "Sausage", "Cheddar cheese", "Mozzarella cheese", "Dried minced onion"
            };
                r4.Prep = "15 minutes";
                r4.Cook = "40 minutes";
                r4.ReadyIn = "55 minutes";
                r4.Directions = "1. Preheat oven to 350 degrees F (175 degrees C). Grease one 8x8 inch casserole dish and set aside." +
                    "\n\n2. Beat together the eggs and milk. Add seasoning salt, ham, Cheddar cheese, Mozzarella cheese and minced onion. Pour into prepared casserole dish." +
                    "\n\n3. Bake uncovered at 350 degrees F (175 degrees C) for 40 to 45 minutes.";

                tmpRecipes.Add(r4);

                Recipe r5 = new Recipe();
                //recipe 5
                r5.Id = 5;
                r5.DishName = "Curried Egg Sandwiches";
                r5.Rate = 4;
                r5.ImageURL = "http://images.media-allrecipes.com/userphotos/560x315/4513255.jpg";
                r5.Detail = "\"Delicious curried egg salad sandwiches guaranteed to fill you up.\"";
                r5.Ingredients = new List<string>()
            {
                "Egg", "Mayonnaise", "Curry powder", "Salt", "Pepper", "Bread"
            };
                r5.Prep = "10 minutes";
                r5.Cook = "15 minutes";
                r5.ReadyIn = "25 minutes";
                r5.Directions = "Mix together mayonnaise and curry powder in a bowl. Gently stir in eggs, then season to taste with salt and pepper. Evenly divide between 4 slices of bread, top with remaining 4 slices.";

                tmpRecipes.Add(r5);

                return tmpRecipes;
            }
        }

        public async Task<bool> AddRecipeAsync(Recipe item)
        {
            recipesList.Add(item);
            await SaveRecipesListToFileAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRecipeAsync(Recipe item)
        {
            recipesList.Remove(item);
            await SaveRecipesListToFileAsync();
            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await Task.FromResult(recipesList.Where((Recipe args) => args.Id == id).FirstOrDefault());
        }

        public async Task<List<Recipe>> GetRecipesListAsync()
        {
            if (FileServices.FileIsExist(FILE_RECIPES_PATH) == true)
            {
                var json = await FileServices.LoadTextAsync(FILE_RECIPES_PATH);
                var items = JsonConvert.DeserializeObject<List<Recipe>>(json);
                if (items != null && items.Count > 0)
                {
                    recipesList.Clear();
                    recipesList = items;
                    return await Task.FromResult(recipesList);
                }
            }
            recipesList.AddRange(GetDefaultRecipesList);
            await SaveRecipesListToFileAsync();
            return await Task.FromResult(recipesList);
        }

        public async Task<bool> UpdateRecipeAsync(Recipe item)
        {
            var _item = recipesList.Where((Recipe args) => args.Id == item.Id).FirstOrDefault();
            recipesList.Remove(_item);
            recipesList.Add(item);
            await SaveRecipesListToFileAsync();
            return await Task.FromResult(true);
     }

        public async Task<List<Recipe>> GetFavouriteRecipesListAsync()
		{
            List<Recipe> favouritesList = new List<Recipe>();
            var items = recipesList.Where((Recipe args) => args.IsMyFavourite == true);
            if (items.Count() > 0)
            favouritesList.AddRange(items);
            return await Task.FromResult(favouritesList);
		}

        private async Task SaveRecipesListToFileAsync()
        {
            var json = JsonConvert.SerializeObject(recipesList);
            await FileServices.SaveTextAsync(FILE_RECIPES_PATH, json);
        }

        public async Task<bool> UpdateFavouriteAsync(Recipe item, bool isFavourite)
        {
            var _newItem = item;
            item.IsMyFavourite = isFavourite;
            recipesList.Remove(item);
            recipesList.Add(_newItem);
            await SaveRecipesListToFileAsync();
            return await Task.FromResult(true);
        }

        public int NumberRecipesInRecipesList()
        {
            return recipesList.Count();
        }
    }
}
