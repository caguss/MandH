using MandH.Models;
using MandH.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MandH.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; }
        public Command AboutCommand { get; }
        private string dinner = "";
        private string lunch = "";
        private string lunchbox = "";
        private string lastdate = "";
        public string Dinner
        {
            get => dinner;
            set => SetProperty(ref dinner, value);
        }
        public string Lunch
        {
            get => lunch;
            set => SetProperty(ref lunch, value);
        }
        public string Lunchbox
        {
            get => lunchbox;
            set => SetProperty(ref lunchbox, value);
        }

        public ItemsViewModel()
        {

        }
        public ItemsViewModel(string Type)
        {
            lastdate = Type;
            switch (Type)
            {
                case "Monday":
                    Title = "월요일";
                    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand("Monday"));
                    break;
                case "Tuesday":
                    Title = "화요일";
                    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand("Tuesday"));
                    break;
                case "Wednesday":
                    Title = "수요일";
                    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand("Wednesday"));
                    break;
                case "Thursday":
                    Title = "목요일";
                    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand("Thursday"));
                    break;
                case "Friday":
                    Title = "금요일";
                    LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand("Friday"));
                    break;
            }
            AboutCommand = new Command(AboutSelected);

        }

        async Task ExecuteLoadItemsCommand(string type)
        {
            IsBusy = true;

            try
            {
                DateTime dateToday = DateTime.Today;
                DateTime monDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(dateToday.DayOfWeek));
                DateTime thuDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Tuesday) - Convert.ToInt32(dateToday.DayOfWeek));
                DateTime wedDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Wednesday) - Convert.ToInt32(dateToday.DayOfWeek));
                DateTime thrDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Thursday) - Convert.ToInt32(dateToday.DayOfWeek));
                DateTime friDate = dateToday.AddDays(Convert.ToInt32(DayOfWeek.Friday) - Convert.ToInt32(dateToday.DayOfWeek));

                string monstr = monDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                string thustr = thuDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                string wedstr = wedDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                string thrstr = thrDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));
                string fristr = friDate.ToString("yyyyMMdd", new CultureInfo("ko-KR"));

                FoodString result = null;
                switch (type)
                {
                    case "Monday":
                        result = await DataStore.GetItemAsync(monstr);
                        break;
                    case "Tuesday":
                        result = await DataStore.GetItemAsync(thustr);
                        break;
                    case "Wednesday":
                        result = await DataStore.GetItemAsync(wedstr);
                        break;
                    case "Thursday":
                        result = await DataStore.GetItemAsync(thrstr);
                        break;
                    case "Friday":
                        result = await DataStore.GetItemAsync(fristr);
                        break;
                }
                if (result == null)
                {
                    Dinner = "등록안됨";
                    Lunch = Lunch = "등록안됨";
                    Lunchbox = Lunch = "등록안됨";
                }
                else
                {
                    Dinner = result.Dinner;
                    Lunch = result.Lunch;
                    Lunchbox = result.LunchBox;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
      

        public void OnAppearing()
        {
            IsBusy = true;
            LoadItemsCommand.Execute(lastdate);
        }



        async void AboutSelected(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        }


    }
}