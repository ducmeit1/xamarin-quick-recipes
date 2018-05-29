using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickRecipes.Services
{
    public interface IRecipeDataStore<T>
    {
		Task<bool> AddRecipeAsync(T item);
		Task<bool> UpdateRecipeAsync(T item);
		Task<bool> DeleteRecipeAsync(T item);
		Task<T> GetRecipeAsync(int id);
		Task<List<T>> GetRecipesListAsync();
        Task<List<T>> GetFavouriteRecipesListAsync();
        Task<bool> UpdateFavouriteAsync(T item, bool isFavourite);
        int NumberRecipesInRecipesList();
    }
}
