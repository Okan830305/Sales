
namespace Sales.ViewModels
{
    using Services;
    using Sales.Common.Models;
    using System.Collections.ObjectModel;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ProductsViewModel: BaseViewModel
    {
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<Products> products;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<Products> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess) {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetList<Products>(url, "/api", "/Products");
            if (!response.IsSuccess) {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }

            var list = (List<Products>)response.Result;
            this.Products = new ObservableCollection<Products>(list);
            this.IsRefreshing = false;

        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }
}
