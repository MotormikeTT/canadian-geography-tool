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
                }
                provinceList.ItemsSource = provinces;
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
