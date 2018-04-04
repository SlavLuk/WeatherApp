# Weather App Project

GoWeather application displays weather at the current location or based on city name search result.


## Description

Application is made up with 4 pages.Main page ,Search page,Setting page and 5 day Forecast page app uses OpenWeatherMap.org API to get current weather .
It uses internet network and GeoLocation sensor to get your current location and shows you weather on the Main page at the start of the app.
Search page uses a query based on city name or city name and country code (London,GB) and returns results as list for listview.If an item is being selected
then it passed back to the Main page and displayed.
Forecast page uses a query based on city's coordinates obtained previously and passed into OnNavigatedTo method ,forecast is sampled at 15:00 every day for 5 days.
Setting page is used to change temperature unit by changing temperature it automatically changes wind speed (m/s -> ml/h).Aplication data container storage is used to save settings.


## Installation

1. You can clone your repository to create a local copy on your computer and sync between the two locations 
2. On GitHub, navigate to the main page of the repository
3. Clone or download button Under the repository name, click Clone or download
4. Clone URL button In the Clone with HTTPs section, click to copy the clone URL for the repository. 
5. Open Git Bash. 
6. Change the current working directory to the location where you want the cloned directory to be made.
7. Type git clone, and then paste the URL you copied in Step 2.

git clone https://github.com/YOUR-USERNAME/YOUR-REPOSITORY 

8. Press Enter. Your local clone will be created.

## License

Educational 