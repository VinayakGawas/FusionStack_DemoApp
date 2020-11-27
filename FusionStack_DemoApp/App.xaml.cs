using Prism;
using Prism.Ioc;
using FusionStack_DemoApp.ViewModels;
using FusionStack_DemoApp.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.Helpers;
using FusionStack_DemoApp.Repo;

namespace FusionStack_DemoApp
{
    public partial class App
    {
        public static User CurrentUser { get; set; }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            DBHelper.CreateTables();
            App.CurrentUser = new User();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IGenericRepo<StudentInfo>, GenericRepo<StudentInfo>>();
            containerRegistry.RegisterSingleton<IGenericRepo<User>, GenericRepo<User>>();


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewUserPage, AddNewUserPageViewModel>();
            containerRegistry.RegisterForNavigation<OfflineUsersPage, OfflineUsersPageViewModel>();
            containerRegistry.RegisterForNavigation<OnlineListPage, OnlineListPageViewModel>();
        }
    }
}
