using System;
using System.Collections.Generic;
using MvvmHelpers;

namespace QuickRecipes.Models
{
    public class Recipe : ObservableObject
    {
        int _id;
        public int Id { get { return _id; } set { SetProperty(ref _id, value); } }
        string _dishName;
        public string DishName { get {return _dishName; }  set {SetProperty(ref _dishName, value); } }
        int _rate;
        public int Rate { get {return _rate; }  set {SetProperty(ref _rate, value); }  }
        string _imageURL;
        public string ImageURL { get {return _imageURL; } set {SetProperty(ref _imageURL, value); } }
        string _detail;
        public string Detail { get {return _detail; }  set {SetProperty(ref _detail, value); }  }
        List<string> _ingredients;
        public List<string> Ingredients { get {return _ingredients; } set {SetProperty(ref _ingredients, value); } }
        string _prep;
        public string Prep { get  {return _prep;} set {SetProperty(ref _prep, value); } }
        string _cook;
        public string Cook { get {return _cook;} set {SetProperty(ref _cook, value); } }
        string _readyIn;
        public string ReadyIn { get { return _readyIn; } set {SetProperty(ref _readyIn, value); } }
        string _directions;
        public string Directions { get { return _directions; } set { SetProperty(ref _directions, value); } }
		public bool _isMyFavourite = false;
        public bool IsMyFavourite { get { return _isMyFavourite; } set { SetProperty(ref _isMyFavourite, value); } }

		//public int Id { get; set; }
		//public string DishName { get; set; }
		//public int Rate { get; set; }
		//public string ImageURL { get; set; }
		//public string Detail { get; set; }
		//public List<string> Ingredients { get; set; }
		//public string Prep { get; set; }
		//public string Cook { get; set; }
		//public string ReadyIn { get; set; }
		//public string Directions { get; set; }
		//public bool _isMyFavourite = false;
		//public bool IsMyFavourite { get { return _isMyFavourite; } set { _isMyFavourite = value; } }
	}
}