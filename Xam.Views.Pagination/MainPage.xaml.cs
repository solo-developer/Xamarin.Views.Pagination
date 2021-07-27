using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Xam.Views.Pagination
{
    public partial class MainPage : ContentPage
    {
        public IAsyncCommand<int> OnPaginatedCommand { get; set; }
        public MainPage()
        {
            InitializeComponent();
            OnPaginatedCommand = new AsyncCommand<int>((page_num) => OnPaginated(page_num), allowsMultipleExecutions: false);
            BindingContext = this;
          
        }

        private async Task OnPaginated(int page_num)
        {
            await DisplayAlert("Callback in Main Page", $"Page number you clicked from Pagination view : {page_num}", "OK");
        }
    }
}
