using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.ViewModels;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Views
{
    public partial class OfflineUsersPage : ContentPage
    {
        OfflineUsersPageViewModel _viewModel;
        public OfflineUsersPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as  OfflineUsersPageViewModel;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var ab = (sender as Button).BindingContext as StudentInfo;
            if (ab!=null)
            {
                _viewModel.SaveDataToServer(ab);
            }
            
        }
    }
}
