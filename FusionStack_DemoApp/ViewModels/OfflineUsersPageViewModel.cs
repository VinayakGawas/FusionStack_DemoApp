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
using System.Text;
using System.Threading.Tasks;

namespace FusionStack_DemoApp.ViewModels
{
    public class OfflineUsersPageViewModel : ViewModelBase
    {
        private ObservableCollection<StudentInfo> _allStudentsList;
        public ObservableCollection<StudentInfo> AllStudentsList
        {
            get { return _allStudentsList; }
            set { SetProperty(ref _allStudentsList, value); }
        }
        public IGenericRepo<StudentInfo> _studentRepo { get; set; }
        public OfflineUsersPageViewModel(INavigationService navigationService, IPageDialogService pageDialog,
            IGenericRepo<StudentInfo> studentRepo) : base(navigationService, pageDialog)
        {
            _studentRepo = studentRepo;
            AllStudentsList = new ObservableCollection<StudentInfo>();
            LoadData();
        }

        private void LoadData()
        {
            var a = _studentRepo.QueryTable().ToList();
            AllStudentsList = new ObservableCollection<StudentInfo>(a.Where(x=>x.IsSynced == false)); 
        }

        internal async void SaveDataToServer(StudentInfo student)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("Sending Data..", MaskType.Clear);
                var jsonData = await Task.Run(() => JsonConvert.SerializeObject(student));
                try
                {
                    var authHeader = new AuthenticationHeaderValue("bearer", AppConstants.BearerToken);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = authHeader;
                    client.BaseAddress = new Uri("https://developer.fusionstak.com/demoapp/api/");
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("user/add", content);
                    var result = await response.Content.ReadAsStringAsync();
                    var response1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    UserDialogs.Instance.HideLoading();
                    if (response1 != null)
                    {
                        if (response1.success)
                        {
                            student.IsSynced = true;
                            _studentRepo.Update(student);
                            await PageDialogService.DisplayAlertAsync("Success!!!", "Data sent successfully...!!", "OK");
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
    }
}
