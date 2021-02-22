using Microsoft.Win32;
using Project1_Group_16.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Project1_Group_16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Statistics stats;
        private IEnumerable<string> provinces;
        private IEnumerable<string> cities;
        private string selectedProvinceName;
        private string selectedCityName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ClickParse(object sender, RoutedEventArgs e)
        {
            try
            {
                string browsedType = sender.ToString().Split(' ')[2].ToLower();

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                    Title = "Browse for data file to parse",
                    Filter = $"{browsedType} files (*.{browsedType})|*.{browsedType}"
                };

                bool? result = openFileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    stats = new Statistics(openFileDialog.FileName, browsedType);

                    // after parsing make sure provinces and cities collections are populated
                    city1ComboBox.ItemsSource = stats.CityCatalogue.Keys;
                    city2ComboBox.ItemsSource = stats.CityCatalogue.Keys;
                    provinces = stats.CityCatalogue.Values.Select(info => info.Province).Distinct();
                }

                provinceList.ItemsSource = provinces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void provinceList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedProvinceName = e.AddedItems[0] as string;
                cities = stats.DisplayProvinceCities(selectedProvinceName).Select(info => info.CityName);
                citiesList.ItemsSource = cities;

                provName.Text = selectedProvinceName;
                ulong provPopulation = stats.DisplayProvincePopulation(selectedProvinceName);
                provPop.Text = String.Format("{0:n0}", provPopulation);
                provLargestCity.Text = stats.DisplayLargestPopulationCity(selectedProvinceName).CityName;
                provSmallestCity.Text = stats.DisplaySmallestPopulationCity(selectedProvinceName).CityName;
                provCaptial.Text = stats.GetCapital(selectedProvinceName).Item1;
            }
        }

        private void cityList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedCityName = e.AddedItems[0] as string;

                city.Text = selectedCityName;
                CityInfo cityInfo = stats.DisplayCityInformation($"{selectedCityName}, {selectedProvinceName}");
                cityPop.Text = String.Format("{0:n0}", cityInfo.Population);
            }
        }

        private void city_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                CityHelper();
            }
        }

        private void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedCityName) && !string.IsNullOrEmpty(selectedProvinceName))
            {
                stats.ShowCityOnMap(selectedCityName, selectedProvinceName);
            }
        }

        private void Button_ClickRank(object sender, RoutedEventArgs e)
        {
            if (stats != null)
            {
                string rankType = sender.ToString().Split(' ')[1].ToLower();
                if (rankType == "population")
                {
                    provinces = stats.RankProvincesByPopulation().Keys;
                }
                else if (rankType == "city")
                {
                    provinces = stats.RankProvincesByCities().Keys;
                }

                provinceList.ItemsSource = provinces;
            }
        }

        // calculate distance between two cities
        private void CityHelper()
        {
            try
            {
                if (city1ComboBox.SelectedItem != null && city2ComboBox.SelectedItem != null)
                {
                    CityInfo city;
                    ulong smallerPop, largerPop;
                    (city, smallerPop, largerPop) = stats.CompareCitiesPopulation(city1ComboBox.SelectedItem.ToString(), city2ComboBox.SelectedItem.ToString());
                    largerCityNameTextBlock.Text = city.CityName;
                    largerCityPopulationTextBlock.Text = String.Format("{0:n0}", largerPop);
                    smallerCityPopulationTextBlock.Text = String.Format("{0:n0}", smallerPop);

                    distanceBetweenTextBlock.Text = String.Format("{0:n}", (stats.CalculateDistanceBetweenCities(city1ComboBox.SelectedItem.ToString(), city2ComboBox.SelectedItem.ToString()) / 1000)) + " km";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
