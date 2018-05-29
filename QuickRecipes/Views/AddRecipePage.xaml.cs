﻿using System;
using System.Collections.Generic;
using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickRecipes.Models;
using QuickRecipes.Services;
using Xamarin.Forms;

namespace QuickRecipes.Views
{
    public partial class AddRecipePage : ContentPage
    {
        public Recipe Recipe { set; get; }

        public string IngredientsText { set; get; }

        string FilePath = "";

        public AddRecipePage()
        {
            InitializeComponent();
            Title = "Add New Recipe";
            Recipe = new Recipe()
            {
                DishName = "",
                Cook = "",
                Rate = 0,
                ImageURL = "",
                Detail = "",
                Directions = "",
                Ingredients = new List<string>(),
                Prep = "",
                ReadyIn = ""
            };
            BindingContext = this;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {

            List<string> errorMessages = new List<string>() {

                    "Please enter Name",
                    "Please enter Rate",
                    "Please enter Detail",
                    "Please enter Ingredients",
                    "Please enter Prep Time",
                    "Please enter Cook Time",
                    "Please enter Ready In Time",
                    "Please enter Directions",
                    "Please enter rate from 0 to 5"
            };

            int pos = IsDataValid();
            if (pos >= 0)
            {
                await DisplayAlert("Error", errorMessages[pos], "OK");
                return;
            }
            string[] ingredientsList = IngredientsText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            List<string> ingredientsMainList = new List<string>();
            foreach (string x in ingredientsList) ingredientsMainList.Add(x);
            Recipe.Ingredients = ingredientsMainList;
            Recipe.ImageURL = FilePath;
            MessagingCenter.Send(this, "AddItem", Recipe);
            await Navigation.PopToRootAsync();
        }

        int IsDataValid()
        {

            if (string.IsNullOrEmpty(Recipe.DishName)) return 0;

            if (string.IsNullOrEmpty(txtRate.Text)) return 1;

            if (!int.TryParse(txtRate.Text, out int number)) return 8;

            if (number < 0 || number > 5) return 8;

            if (string.IsNullOrEmpty(Recipe.Detail)) return 2;

            if (string.IsNullOrEmpty(IngredientsText)) return 3;

            if (string.IsNullOrEmpty(Recipe.Prep)) return 4;

            if (string.IsNullOrEmpty(Recipe.Cook)) return 5;

            if (string.IsNullOrEmpty(Recipe.ReadyIn)) return 6;

            if (string.IsNullOrEmpty(Recipe.Directions)) return 7;


            return -1;
        }

        async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Upload", "Picking a photo is not supported", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 60
            });

            if (file == null) return;
            var FileServices = DependencyService.Get<ISaveAndLoad>();
			ImageSource imgSrc = ImageSource.FromStream(() =>
			 {
				 var stream = file.GetStream();
				 return stream;
			 });
            string name = Guid.NewGuid() + ".jpg";
            await FileServices.SavePictureAsync(name, imgSrc);
            FilePath = FileServices.CreatePathToFile(name);
            imgRecipe.Source = ImageSource.FromFile(FilePath);
            file.Dispose();
        }

        async void TakeFromCamera_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 60,
                AllowCropping = true,
                Name = Guid.NewGuid() + ".jpg"
            });
            if (file == null) return;
            var FileServices = DependencyService.Get<ISaveAndLoad>();
			ImageSource imgSrc = ImageSource.FromStream(() =>
			 {
				 var stream = file.GetStream();
				 return stream;
			 });
            string name = Guid.NewGuid() + ".jpg";
            await FileServices.SavePictureAsync(name, imgSrc);
			FilePath = FileServices.CreatePathToFile(name);
			imgRecipe.Source = ImageSource.FromFile(FilePath);
            file.Dispose();
        }
    }
}
