using Acr.UserDialogs;
using FusionStack_DemoApp.Helpers;
using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.Repo;
using FusionStack_DemoApp.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FusionStack_DemoApp.ViewModels
{
    public class DashboardPageViewModel : ViewModelBase
    { 
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand CreateNewUserCommand { get; set; }
        public DelegateCommand SyncDataCommand { get; set; }
        public DelegateCommand NavigateToSavedUsers { get; set; }
        public DelegateCommand NavigateToOnlineRecords { get; set; }
        public IGenericRepo<StudentInfo> _studentRepo { get; set; }
        public DashboardPageViewModel(INavigationService navigationService, IPageDialogService pageDialog,
            IGenericRepo<StudentInfo> studentRepo) : base(navigationService, pageDialog)
        {
            _studentRepo = studentRepo;
            CreateNewUserCommand = new DelegateCommand(AddStudentInfo);
            SyncDataCommand = new DelegateCommand(SyncAllData);
            NavigateToOnlineRecords = new DelegateCommand(NavigateToOnlineList);
             NavigateToSavedUsers = new DelegateCommand(NavigateToSavedUsersMethod);
            LogoutCommand = new DelegateCommand(Logout);
        }
        private async void Logout()
        {
            var response = await PageDialogService.DisplayAlertAsync("Log Out?", "Do you want to log out?", "Yes", "No");
            //await NavigationService.NavigateAsync("NavigationPage/LoginPage");
            if (response)
                await NavigationService.NavigateAsync("app:///NavigationPage/LoginPage");
        }
        private void NavigateToOnlineList()
        {
            NavigationService.NavigateAsync(nameof(OnlineListPage));
        }
        private void AddStudentInfo()
        {
            NavigationService.NavigateAsync(nameof(AddNewUserPage));
        }
        private void NavigateToSavedUsersMethod()
        {
            NavigationService.NavigateAsync(nameof(OfflineUsersPage));
        }

        private void SyncAllData()
        {

        }

    }
}
