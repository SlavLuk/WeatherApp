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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search : Page
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private RootObjectCity result;
        private ObservableCollection<ListCity> _items = new ObservableCollection<ListCity>();

        public ObservableCollection<ListCity> Items
        {
            get { return this._items; }
           
        }

        public Search()
        {
            this.InitializeComponent();
        }

        

        private async void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            Items.Clear();
            error.Text = "";
            string tempUnit = localSettings.Values["temp"].ToString();



            try
            {
                result = await CityProxy.GetWeatherByCityName(args.QueryText,tempUnit);

           

                if (result.list.Count > 0)
                {

                    foreach (ListCity c in result.list)
                    {
                        Items.Add(c);


                    }

                }
                else
                {
                    error.Text = "No results";
                }
            }
            catch (Exception )
            {

                error.Text = "No results";
            }

       

         

           

        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ListCity)listCity.SelectedItem;
            string passedValue = selected.coord.lat+" "+selected.coord.lon;
          

            if (selected != null)
            {

                 Frame.Navigate(typeof(MainPage), passedValue);
            }




        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
              
            }
        }
    }
}
