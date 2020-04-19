using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealEstateMobile.Models;

namespace RealEstateMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewContacts : ContentPage
    {
        public ViewContacts()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var url = "http://73.46.79.123:5000/api/ContactsApi";
            lstContacts.ItemsSource = GetContacts(url);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditContacts());
        }

        private async void LstContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AddEditContacts
                {
                    BindingContext = e.SelectedItem as Contacts
                });
            }
        }

        private List<Contacts> GetContacts(String url)
        {
            var client = new RestClient(url);

            var response = client.Execute(new RestRequest());

            //DisplayAlert("Alert", response.Content.ToString(), "OK");

            List<Contacts> cont = SimpleJson.DeserializeObject<List<Contacts>>(response.Content.ToString());

            return cont;
        }
    }
}