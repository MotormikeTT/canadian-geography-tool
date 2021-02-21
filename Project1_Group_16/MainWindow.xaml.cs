using Microsoft.Win32;
using Project1_Group_16.Classes;
using System;
using System.Collections.Generic;
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
        private List<string> provinces;
        private List<string> cities;

        public MainWindow()
        {
            InitializeComponent();
            provinces = new List<string>();
            cities = new List<string>();
        }

        private void Button_ClickParse(object sender, RoutedEventArgs e)
        {
            try
            {
                string browsedType = sender.ToString().Split(' ')[2].ToLower();

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Browse for data file to parse",
                    Filter = $"{browsedType} files (*.{browsedType})|*.{browsedType}"
                };

                bool? result = openFileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    //string ext = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('.') + 1);
                    stats = new Statistics(openFileDialog.FileName, browsedType);
                }

                // after parsing make sure provinces and cities collections are populated
                if (!provinces.Any())
                {
                    //stats.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // calculate distance between two cities
        private void Button_ClickCalculate(object sender, RoutedEventArgs e)
        {

        }
        // compare population of two cities
        private void Button_ClickCompare(object sender, RoutedEventArgs e)
        {

        }
    }
}
