using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickRecipes.Services
{
    public interface ISaveAndLoad
    {
        string CreatePathToFile(string fileName);
        Task SaveTextAsync(string fileName, string text);
        Task<string> LoadTextAsync(string fileName);
        bool FileIsExist(string fileName);
        Task SavePictureAsync(string fileName, ImageSource imgSr);
    }
}
