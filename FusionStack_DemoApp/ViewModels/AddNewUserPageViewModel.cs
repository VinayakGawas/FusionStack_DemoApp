using Acr.UserDialogs;
using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.Repo;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FusionStack_DemoApp.ViewModels
{
    public class AddNewUserPageViewModel : ViewModelBase
    {
        private StudentInfo _NewUser;
        public StudentInfo NewUser

        {
            get { return _NewUser; }
            set { SetProperty(ref _NewUser, value); }
        }
        public DelegateCommand SaveUserCommand { get; set; }
        public IGenericRepo<StudentInfo> _studentRepo;
        public AddNewUserPageViewModel(INavigationService navigationService, IPageDialogService pageDialog
            , IGenericRepo<StudentInfo> studentRepo) : base(navigationService, pageDialog)
        {
            _studentRepo = studentRepo;
            NewUser = new StudentInfo();
            SaveUserCommand = new DelegateCommand(SaveUser);
        }

        private async void SaveUser()
        {
            if (string.IsNullOrEmpty(NewUser.firstName))
            {
                await PageDialogService.DisplayAlertAsync("","First Name cannot be empty","Ok");
                return;
            }

            if (string.IsNullOrEmpty(NewUser.lastName))
            {
                await PageDialogService.DisplayAlertAsync("", "Last Name cannot be empty", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(NewUser.phoneNumber))
            {
                await PageDialogService.DisplayAlertAsync("", "Phone Number cannot be empty", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(NewUser.email))
            {
                await PageDialogService.DisplayAlertAsync("", "Email ID cannot be empty", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(NewUser.address))
            {
               await PageDialogService.DisplayAlertAsync("", "Address cannot be empty", "Ok");
                return;
            }
            if (NewUser.age == 0)
            {
               await PageDialogService.DisplayAlertAsync("", "Please enter age", "Ok");
                return;
            }

            var a = await  PageDialogService.DisplayAlertAsync("Save","Do you want to save it?","Yes","No");

            if (a)
            {
                if (NewUser.IsSynced)
                {
                    SendToServerTOUpdate(NewUser);
                }
                else
                {
                    _studentRepo.Insert(NewUser);
                    await PageDialogService.DisplayAlertAsync("", "Saved successfully", "OK");
                }
                
            }
        }

        private async void SendToServerTOUpdate(StudentInfo newUser)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("Logging In", MaskType.Clear);
                var jsonData = await Task.Run(() => JsonConvert.SerializeObject(newUser));
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://developer.fusionstak.com/demoapp/api/");
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("user/update", content);
                    var result = await response.Content.ReadAsStringAsync();
                    var response1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    UserDialogs.Instance.HideLoading();
                    if (response1 != null)
                    {
                        if (response1.success)
                        {
                            await PageDialogService.DisplayAlertAsync("Success!!!", "Updated successfully...!!", "OK");
                        }
                        else
                        {
                            await PageDialogService.DisplayAlertAsync("Error!!!", "Please try again...!!", "OK");
                        }
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
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await PageDialogService.DisplayAlertAsync("", "You are not connected to the internet, Please check your connectivity and try again.", "OK");
                return;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("student"))
            {
                NewUser = parameters["student"] as StudentInfo;
            }
        }
    }
}
