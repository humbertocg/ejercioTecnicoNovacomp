using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using ejercioTecnico1.Models;
using Newtonsoft.Json;

namespace ejercioTecnico1.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        HttpClient client = new HttpClient();
        public String TotalItems { get; set; }
        public List<Entry> Entries { get; set; }

        public PetDirectory PetDirectory { get; set; }

        public MainPageVM()
        {
            PetDirectory = new PetDirectory();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void OnViewAppearing()
        {
            //fetch from services

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.publicapis.org/entries");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                PetDirectory result = JsonConvert.DeserializeObject<PetDirectory>(responseBody);

                PetDirectory = result;

                TotalItems = result.Count.ToString();
                NotifyPropertyChanged(nameof(TotalItems));


                var filteredItems = GetFiltereDataByCategory("Animals");
                Entries = filteredItems;
                NotifyPropertyChanged(nameof(Entries));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private List<Entry> GetFiltereDataByCategory(string category)
        {
            Entries = PetDirectory.Entries.Where(item => item.Category.ToUpper() == category.ToUpper()).ToList();
            return Entries;
        }
    }

}