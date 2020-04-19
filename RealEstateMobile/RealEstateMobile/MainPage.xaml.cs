using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstateMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnContacts_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewContacts());
        }

        private void BtnListings_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewListings());
        }
    }
}
