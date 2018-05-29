using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using QuickRecipes.Droid;
using QuickRecipes.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ISaveAndLoad_Android))]
namespace QuickRecipes.Droid
{
    public class ISaveAndLoad_Android : ISaveAndLoad
    {
        public string CreatePathToFile(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(documentsPath, fileName);
        }

        public async Task SaveTextAsync(string fileName, string text)
        {
            var path = CreatePathToFile(fileName);
            using (StreamWriter sw = File.CreateText(path))
            {
                await sw.WriteAsync(text);
            }
        }

        public async Task<string> LoadTextAsync(string fileName)
        {
            var path = CreatePathToFile(fileName);
            using (StreamReader sr = File.OpenText(path))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public bool FileIsExist(string fileName)
        {
            return File.Exists(CreatePathToFile(fileName));
        }


        public async Task SavePictureAsync(string fileName, ImageSource imgSrc)
        {
            var renderer = new StreamImagesourceHandler();
            var photo = await renderer.LoadImageAsync(imgSrc, Android.App.Application.Context);
            var path = CreatePathToFile(fileName);
            using (FileStream fs = new FileStream(path, FileMode.CreateNew)) 
            {
                photo.Compress(Bitmap.CompressFormat.Jpeg, 100, fs);
                Debug.WriteLine("Saved as " + path);
            }
        }
    }
}