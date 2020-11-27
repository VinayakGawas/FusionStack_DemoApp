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
    public class OnlineListPageViewModel : ViewModelBase
    {
        private ObservableCollection<StudentInfo> _allStudentsList;
        public ObservableCollection<StudentInfo> AllStudentsList
        {
            get { return _allStudentsList; }
            set { SetProperty(ref _allStudentsList, value); }
        }

       

        public IGenericRepo<StudentInfo> _studentRepo { get; set; }
        public OnlineListPageViewModel(INavigationService navigationService, IPageDialogService pageDialog,
            IGenericRepo<StudentInfo> studentRepo) : base(navigationService, pageDialog)
        {
            _studentRepo = studentRepo;
            LoadData();
        }
        internal async void DeleteRecord(StudentInfo ab)
        {
           
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("Loading Data", MaskType.Clear);
                try
                {
                    var authHeader = new AuthenticationHeaderValue("bearer", AppConstants.BearerToken);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = authHeader;
                    var URL = "https://developer.fusionstak.com/demoapp/api/user/" +ab.id;
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response = await client.DeleteAsync(URL);
                    var result = await response.Content.ReadAsStringAsync();
                    var response1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    UserDialogs.Instance.HideLoading();
                    if (response1 != null)
                    {
                        if (response1.success)
                        {
                            await PageDialogService.DisplayAlertAsync("","Deleted Successfully","OK");
                            AllStudentsList.Remove(ab);
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

        internal async void EditData(StudentInfo ab)
        {
            await NavigationService.NavigateAsync(nameof(AddNewUserPage),new NavigationParameters { { "student", ab} });
        }

        internal  async void SaveDataToDatabase(StudentInfo ab)
        {
            var res = await PageDialogService.DisplayAlertAsync("","Do you want to save this data?","Yes","No");
            if (res)
            {
                var abc = _studentRepo.QueryTable().Where(x=>x.firstName == ab.firstName && x.phoneNumber == ab.phoneNumber).Any();
               if(!abc)
                _studentRepo.Insert(ab);
                else
                {
                   await PageDialogService.DisplayAlertAsync("","Student Data Exists.","Ok");
                }
            }
        }
        private async void LoadData()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("Loading Data", MaskType.Clear);
                try
                {
                    var authHeader = new AuthenticationHeaderValue("bearer", AppConstants.BearerToken);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = authHeader;
                    var URL = "https://developer.fusionstak.com/demoapp/api/user";
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    HttpRequestMessage request = new HttpRequestMessage();
                    HttpResponseMessage response = await client.GetAsync(URL);
                    var result = await response.Content.ReadAsStringAsync();
                    var response1 = JsonConvert.DeserializeObject<RootStudents>(result);

                    if (response1 != null)
                    {
                        AllStudentsList = new ObservableCollection<StudentInfo>(response1.students);
                        AllStudentsList.ToList().ForEach(x=>x.IsSynced = true);
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
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
    }
}
