using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateMobile.Models;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

namespace RealEstateMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditContacts : ContentPage
    {
        public AddEditContacts()
        {
            InitializeComponent();
        }

        bool isNewItem = false;
        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            //check for input first. 
            if (txtContactNames.Text == "" || txtContactLastNames.Text is null)
            {
                await DisplayAlert("Error", "Name Field is required!", "OK");
                txtContactNames.BackgroundColor = Color.Red;
                return;
            }
            else
                txtContactNames.BackgroundColor = Color.White;

            HttpClient _client = new HttpClient();

            var json = jsonData();

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (isNewItem)
            {
                var uri = new Uri(string.Format("http://73.46.79.123:5000/api/ContactsApi", string.Empty));

                try
                {
                    var response = await _client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Add", "Item updated", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Add", "Some Error ocurred", "OK");
                }

            }
            else
            {
                var uri = new Uri(string.Format("http://73.46.79.123:5000/api/ContactsApi/{0}", int.Parse(txtContactId.Text)));

                try
                {
                    var response = await _client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Update", "Item updated", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Update", "Some Error ocurred", "OK");
                }
            }
            

            await Navigation.PopAsync();
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            HttpClient _client = new HttpClient();

            var uri = new Uri(string.Format("http://73.46.79.123:5000/api/ContactsApi/{0}", int.Parse(txtContactId.Text)));

            await DisplayAlert("Alert", uri.ToString(), "OK");

            try 
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Delete", "Item Deleted", "OK");
                }                                 
            }
            catch (Exception ex)
            {
                await DisplayAlert("Delete", "Some Error occured", "OK");
            }         

            await Navigation.PopAsync();
        }
        private string jsonData()
        {
            Contacts details = new Contacts();

            if (txtContactId.Text != null)
            {
                details.contactsId = int.Parse(txtContactId.Text);
                isNewItem = false;
            }
            else 
            {
                details.contactsId = 0;
                isNewItem = true;
            }
            
            details.contactNames = txtContactNames.Text;
            details.contactLastNames = txtContactLastNames.Text;
            details.idNumber = txtIdNumber.Text;
            details.phoneNumber = txtPhoneNumber.Text;
            details.alternateNumber = txtAlternateNumber.Text;
            details.emailAddress = txtEmailAddress.Text;
            details.contactType = txtContactType.Text;
            details.fkCompaniesId = int.Parse(txtFKCompaniesId.Text);

            var json = JsonConvert.SerializeObject(details);

            return json;
        }        
    }
}