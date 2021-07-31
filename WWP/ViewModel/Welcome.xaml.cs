using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WWP.ViewModel
{
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }
    }
}
