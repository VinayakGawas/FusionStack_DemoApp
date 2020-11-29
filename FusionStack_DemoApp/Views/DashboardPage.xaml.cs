using FusionStack_DemoApp.ViewModels;
using Xamarin.Forms;

namespace FusionStack_DemoApp.Views
{
    public partial class DashboardPage : ContentPage
    {
        DashboardPageViewModel _viewModel;
        public DashboardPage()
        {
            InitializeComponent();
           

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
//3700B3