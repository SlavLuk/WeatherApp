using System;
using System.Collections.Generic;
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
    public sealed partial class Setting : Page
    {

        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public Setting()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

           

          



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var btnName = localSettings.Values["temp"];

            if (((string)btnName).Equals("metric"))
            {
                metric.IsChecked = true;
            }
            else
            {
                imperial.IsChecked = true;
            }

            base.OnNavigatedTo(e);
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }

        private void Temp_RadioButton_Checked(object sender, RoutedEventArgs e)
        {

          
            RadioButton rb = sender as RadioButton;
            var btnChecked = rb.IsChecked;

            if (btnChecked.HasValue)
            {

                error.Text = rb.Name.ToString();

                localSettings.Values["temp"] = rb.Name.ToString();
               
            }

           
            
        }
    }
}
