
namespace Sales.ViewModels
{
    using Services;
    using Sales.Common.Models;
    using System.Collections.ObjectModel;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;

    public class ProductsViewModel: BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Products> products;

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
            var response = await this.apiService.GetList<Products>("http://192.168.137.1/apisales", "/api", "/Products");
            if (!response.IsSuccess) {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }

            var list = (List<Products>)response.Result;
            this.Products = new ObservableCollection<Products>(list);

        }
    }
}
