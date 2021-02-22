using Microsoft.Win32;
using Project1_Group_16.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<string> provinces;
        private IEnumerable<string> cities;

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
                    //string ext = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('.') + 1);
                    stats = new Statistics(openFileDialog.FileName, browsedType);

                    // after parsing make sure provinces and cities collections are populated
                    provinces = new ObservableCollection<string>(stats.CityCatalogue.Values.Select(info => info.Province).Distinct());

                    city1ComboBox.ItemsSource = stats.CityCatalogue.Keys;
                    city2ComboBox.ItemsSource = stats.CityCatalogue.Keys;
                }
                provinceList.ItemsSource = provinces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // calculate distance between two cities
        private void Button_ClickFunction(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (sender.ToString().Split(' ')[1].ToLower())
                {
                    case "compare":
                        {
                            CityInfo city;
                            ulong smallerPop, largerPop;
                            (city, smallerPop, largerPop) = stats.CompareCitiesPopulation(city1ComboBox.SelectedItem.ToString(), city2ComboBox.SelectedItem.ToString());
                            largerCityNameTextBlock.Text = city.CityName;
                            largerCityPopulationTextBlock.Text = String.Format("{0:n0}", largerPop);
                            smallerCityPopulationTextBlock.Text = String.Format("{0:n0}", smallerPop);
                            break;
                        }
                    case "calculate":
                        {
                            var blah = stats.CalculateDistanceBetweenCities(city1ComboBox.SelectedItem.ToString(), city2ComboBox.SelectedItem.ToString());
                            distanceBetweenTextBlock.Text = String.Format("{0:n}", (stats.CalculateDistanceBetweenCities(city1ComboBox.SelectedItem.ToString(), city2ComboBox.SelectedItem.ToString())/ 1000) + " km");
                            break;
                        }
                    default:
                        throw new Exception("invalid button");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
