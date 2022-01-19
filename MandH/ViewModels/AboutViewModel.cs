using System;
using System.Data;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MandH.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string CurrentMoney { get; set; }
        public string GoalProduct { get; set; }
        public string GoalMoney { get; set; }
        public double ProgressStatus { get; set; }

        public AboutViewModel()
        {
            Title = "도네이션";
            ItemTapped = new Command(CopyData);
            DataRefresh();
        }

        private void DataRefresh()
        {
            DataTable dt = DataStore.AboutRefresh();

            CurrentMoney = dataFormat( dt.Rows[0]["currentmoney"].ToString())+"원";
            GoalMoney = dataFormat(dt.Rows[0]["goalmoney"].ToString()) + "원";
            GoalProduct = dt.Rows[0]["goalproduct"].ToString();
            
            ProgressStatus = Convert.ToDouble(dt.Rows[0]["currentmoney"].ToString()) / Convert.ToDouble(dt.Rows[0]["goalmoney"].ToString()) ;

        }

        /// <summary>
        /// 복사하기
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void CopyData(object obj)
        {
            Clipboard.SetTextAsync("82530104075477 국민은행");
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
            }
        }

        public ICommand ItemTapped { get; }


        public static String dataFormat(String text)
        {
            string format = null;
            if (text.IndexOf(".") > 0)
            {//include decimal
                if (text.Length - text.IndexOf(".") - 1 == 0)
                {//include a decimal
                    format = "###,##0.";
                }
                else if (text.Length - text.IndexOf(".") - 1 == 1)
                {//include two decimals
                    format = "###,##0.0";
                }
                else
                {//include more than two decimal
                    format = "###,##0.00";
                }
            }
            else
            {//only integer
                format = "###,##0";
            }
            double number = 0.0;
            try
            {
                number = Double.Parse(text);
            }
            catch (Exception)
            {
                number = 0.0;
            }
            return number.ToString(format);
        }
    }
}