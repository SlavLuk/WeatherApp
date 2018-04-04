using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Forecast : Page
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private ObservableCollection<ListForecast> _items = new ObservableCollection<ListForecast>();

        public ObservableCollection<ListForecast> Items
        {
            get { return this._items; }

        }
        public Forecast()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }



        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //check passed back parameter
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                try
                {
                    string[] coords = e.Parameter.ToString().Split(' ');

                    string tempUnit = localSettings.Values["temp"].ToString();

                    //get forecast root object based on coords passed back from search page
                    RootObjectForecast result = await ForecastProxy.GetWeatherByLocation(double.Parse(coords[0]), double.Parse(coords[1]), tempUnit);

                    Items.Clear();

                    cityName.Text = result.city.name.ToString() + "," + result.city.country.ToString();

                    if (result.list.Count > 0)
                    {

                        foreach (ListForecast c in result.list)
                        {
                            //add forecast weather object to collection based on 15:00 forecast
                            if (c.dt_txt.Contains("15:00:00"))
                            {
                                Items.Add(c);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    error.Text = ex.Message.ToString();
                   
                }      
            }
        }      
    }
}