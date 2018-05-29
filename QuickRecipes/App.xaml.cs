using QuickRecipes.DataStore;
using QuickRecipes.Views;
using Xamarin.Forms;


namespace QuickRecipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<RecipeDataStore>();
            DependencyService.Register<MyIngredientsDataStore>();

            MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new QuickRecipePage()){
                        Title = "Browse",
                        Icon = "search.png"
                    },
                    new NavigationPage(new AllRecipesPage()) {
                        Title = "All Recipes",
                        Icon = "allrecipe.png"
                    },
                    new NavigationPage(new MyFavouritesPage()) {
                        Title = "My Favourites",
                        Icon = "favorite.png"
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
