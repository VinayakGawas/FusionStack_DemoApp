using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        public IPageDialogService PageDialogService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _conn;
        public string conn
        {
            get { return _conn; }
            set { SetProperty(ref _conn, value); }
        }
        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
            CheckWifiOnStart();
            CheckWifiContinuously();
        }
        private void CheckWifiContinuously()
        {
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                conn = args.IsConnected ? "connected" : "disconnected";
            };
        }

        private void CheckWifiOnStart()
        {
            var a = Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
            if (a)
                conn = "connected";
            else
                conn = "disconnected";
        }
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
