using MandH.Models;
using MandH.Services;
using MandH.ViewModels;
using MandH.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MandH.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        { }
        public ItemsPage(string current)
        {

            InitializeComponent();
            //분기별 가져오는 데이터 바꾸기
            //var current = Shell.Current.CurrentItem.CurrentItem.Route;
            switch (current)
            {
                case "Monday":
                    BindingContext = _viewModel = new ItemsViewModel("Monday");
                    break;
                case "Tuesday":
                    BindingContext = _viewModel = new ItemsViewModel("Tuesday");
                    break;
                case "Wednesday":
                    BindingContext = _viewModel = new ItemsViewModel("Wednesday");
                    break;
                case "Thursday":
                    BindingContext = _viewModel = new ItemsViewModel("Thursday");
                    break;
                case "Friday":
                    BindingContext = _viewModel = new ItemsViewModel("Friday");
                    break;
                default:
                    return;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}