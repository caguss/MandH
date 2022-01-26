using MandH.ViewModels;
using MandH.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MandH
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Init(this);

        }

        private void Init(Shell root)
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[0];
                    break;
                case DayOfWeek.Tuesday:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[1];
                    break;
                case DayOfWeek.Wednesday:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[2];
                    break;
                case DayOfWeek.Thursday:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[3];
                    break;
                case DayOfWeek.Friday:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[4];
                    break;
                default:
                    root.CurrentItem.CurrentItem = root.CurrentItem.Items[0];
                    break;
            }
        }

    }
}
