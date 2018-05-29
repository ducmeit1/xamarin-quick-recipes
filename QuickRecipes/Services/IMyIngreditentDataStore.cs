using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRecipes.Services
{
    public interface IMyIngreditentDataStore<T>
    {
        Task<bool> AddIngredientAsync(string T);
        Task<List<T>> GetIngredientsListAsync();
    }
}
