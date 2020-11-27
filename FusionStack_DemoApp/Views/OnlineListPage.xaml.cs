using FusionStack_DemoApp.Models;
using FusionStack_DemoApp.ViewModels;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Views
{
    public partial class OnlineListPage : ContentPage
    {
        private OnlineListPageViewModel _viewModel;

        public OnlineListPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as OnlineListPageViewModel;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var ab = (sender as Button).BindingContext as StudentInfo;
            if (ab != null)
            {
                _viewModel.SaveDataToDatabase(ab);
            }
        }

        private async void EditButton_Clicked(object sender, System.EventArgs e)
        {
            var ab = (sender as Button).BindingContext as StudentInfo;
            if (ab != null)
            {
                var res = await DisplayAlert("", "Do you want to Edit the record", "Yes", "No");
                if (res)
                    _viewModel.EditData(ab);
            }
        }

        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            var ab = (sender as Button).BindingContext as StudentInfo;
            if (ab != null)
            {
                var res = await DisplayAlert("", "Do you want to Delete the record", "Yes", "No");
                if(res)
                    _viewModel.DeleteRecord(ab);
            }
        }
    }
}
