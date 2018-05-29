using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using QuickRecipes.iOS;
using QuickRecipes.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: Dependency(typeof(SaveAndLoad_iOS))]
namespace QuickRecipes.iOS
{
    public class SaveAndLoad_iOS : ISaveAndLoad
    {
        public SaveAndLoad_iOS()
        {
        }

        public string CreatePathToFile(string fileName) 
        {
            var documentsPath = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last();
            var url = documentsPath.Path;
            return Path.Combine(url, fileName);
        }

        public bool FileIsExist(string fileName)
        {
            return File.Exists(CreatePathToFile(fileName));
        }

        public async Task<string> LoadTextAsync(string fileName)
        {
            var path = CreatePathToFile(fileName);
            using (StreamReader sr = File.OpenText(path)) {
                return await sr.ReadToEndAsync();
            }
        }

        public async Task SaveTextAsync(string fileName, string text)
        {
            var path = CreatePathToFile(fileName);
            using (StreamWriter sw = File.CreateText(path)) {
                await sw.WriteAsync(text);
            }
        }

        public async Task SavePictureAsync(string fileName, ImageSource imgSrc)
        {
            var renderer = new StreamImagesourceHandler();
            var photo = await renderer.LoadImageAsync(imgSrc);
            var path = CreatePathToFile(fileName);
            NSData imgData = photo.AsJPEG();
            if (imgData.Save(path, false, out NSError err)) {
                Debug.WriteLine("Saved as " + path);
            } else {
                Debug.WriteLine("Not saved as " + path + " because " + err.LocalizedDescription);
            }
        }
    }
}
