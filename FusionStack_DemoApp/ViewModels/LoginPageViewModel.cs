using FusionStack_DemoApp.Helpers;
using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.Repo;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace FusionStack_DemoApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private User user;
        public User _user
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }
        public DelegateCommand LoginCommand { get; set; }
        public IGenericRepo<User> _userRepo { get; set; }
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialog
            , IGenericRepo<User> userRepo) : base(navigationService, pageDialog)
        {
            _userRepo = userRepo;
            _user = new User();
            _user.UserName = "admin@fusionstak.com";
            _user.Password = "Fusion@123";
            LoginCommand = new DelegateCommand(Login);
        }
        internal void CheckUser()
        {
            if (!string.IsNullOrEmpty(user.UserName))
            {
                if (user.UserName.ToLower() != "admin")
                {
                    var user = _userRepo.QueryTable().FirstOrDefault(x => x.UserName == _user.UserName);
                    if (user != null)
                    {
                        // ExistingUser = user;
                    }
                    else
                    {
                        PageDialogService.DisplayAlertAsync("", "User does not exist please register to continue.", "OK");
                    }
                }
            }
        }
       

        private async void Login()
        {
            if (string.IsNullOrEmpty(_user.UserName))
            {
                await PageDialogService.DisplayAlertAsync("", "User Name cannot be empty", "OK");
                return;
            }

            if (string.IsNullOrEmpty(_user.Password))
            {
                await PageDialogService.DisplayAlertAsync("", "Password cannot be empty", "OK");
                return;
            }


            if (_user.UserName.ToLower() == "admin@fusionstak.com" && _user.Password == "Fusion@123")
            {
                App.CurrentUser.UserName = "admin@fusionstak.com";
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                {

                    var jsonData = await Task.Run(() => JsonConvert.SerializeObject(_user));
                    try
                    {
                        var client = new HttpClient();
                        client.BaseAddress = new Uri("https://developer.fusionstak.com/demoapp/api/");
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("account/token", content);
                        var result = await response.Content.ReadAsStringAsync();
                        var response1 = JsonConvert.DeserializeObject<Root>(result);

                        if (response != null)
                        {
                            AppConstants.BearerToken = response1.tokenResult.token;
                            AppConstants.UserId = response1.userId;
                            await NavigationService.NavigateAsync(nameof(HomePage));
                        }
                        else
                        {
                            await PageDialogService.DisplayAlertAsync("Error!!!", "Please try again...!!", "OK");
                        }
                    }
                    catch (Exception e)
                    {
                        await PageDialogService.DisplayAlertAsync("Error!!!", "Please try again...!!", "OK" + e);

                    }
                }
                else
                {
                   await PageDialogService.DisplayAlertAsync("", "You are not connected to the internet, Please check your connectivity and try again.", "OK");
                   return;
                }
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("", "Wrong username and Password.", "OK");
                return;
            }


        }
    }
}
