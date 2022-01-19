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

        }

    }
}
