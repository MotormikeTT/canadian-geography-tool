using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using GeoCoordinatePortable;

namespace Project1_Group_16.Classes
{
    /// <summary>
    /// Statistic class with functions that allows the user to retrieve information about the cities
    /// </summary>
    class Statistics
    {
        // Properties
        internal Dictionary<string, CityInfo> CityCatalogue = new Dictionary<string, CityInfo>();

        // Constructor
        public Statistics(string fileName, string fileType)
        {
            CityCatalogue = DataModeler.ParseFile(fileName, fileType);
        }

        /// <summary>
        /// Get city information based on the city.
        /// </summary>
        /// <param name="city">A string with the name of the city.</param>
        /// <returns>A list with the city information</returns>
        public CityInfo DisplayCityInformation(string city)
        {
            return CityCatalogue[city];
        }

        /// <summary>
        /// Gets the largest populous city in a given province.
        /// </summary>
        /// <param name="province">A string with the name of the province</param>
        /// <returns>A CityInfo object with the city information.</returns>
        public CityInfo DisplayLargestPopulationCity(string province)
        {
            return CityCatalogue.Where(city => city.Value.Province == province).OrderByDescending(city => city.Value.Population).First().Value;
        }

        /// <summary>
        /// Gets the smallest populous city in a given province.
        /// </summary>
        /// <param name="province">A string with the name of the province</param>
        /// <returns>A CityInfo object with the city information.</returns>
        public CityInfo DisplaySmallestPopulationCity(string province)
        {
            return CityCatalogue.Where(city => city.Value.Province == province).OrderBy(city => city.Value.Population).First().Value;
        }

        /// <summary>
        /// Compares 2 cities population
        /// </summary>
        /// <param name="city1">A string with the name of city 1.</param>
        /// <param name="city2">A string with the name of city 2.</param>
        /// <returns></returns>
        public (CityInfo, ulong, ulong) CompareCitiesPopulation(string city1, string city2)
        {
            return (CityCatalogue[city1].Population > CityCatalogue[city2].Population ? CityCatalogue[city1] : CityCatalogue[city2], CityCatalogue[city2].Population, CityCatalogue[city1].Population);
        }

        /// <summary>
        /// Shows city on the map
        /// </summary>
        /// <param name="city">A string with the name of the city.</param>
        /// <param name="province">A string with the name of the province.</param>
        public void ShowCityOnMap(string city, string province)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://www.google.com/maps/place/" + city + "," + province;
            process.Start();
        }

        /// <summary>
        /// Gets the distance between cities.
        /// </summary>
        /// <param name="city1">A string with the name of city 1.</param>
        /// <param name="city2">A string with the name of city 2.</param>
        /// <returns>A double with the distance between two cities.</returns>
        public double CalculateDistanceBetweenCities(string city1, string city2)
        {
            GeoCoordinate city1Coord = new GeoCoordinate(CityCatalogue[city1].Latitude, CityCatalogue[city1].Longitude);
            GeoCoordinate city2Coord = new GeoCoordinate(CityCatalogue[city2].Latitude, CityCatalogue[city2].Longitude);

            return city1Coord.GetDistanceTo(city2Coord);
        }

        /// <summary>
        /// Gets the province population.
        /// </summary>
        /// <param name="province">A string with the province name.</param>
        /// <returns>A ulong with the population of the province.</returns>
        public ulong DisplayProvincePopulation(string province)
        {
            return (ulong)CityCatalogue.Where(city => city.Value.Province == province).Sum(city => (decimal)city.Value.Population);
        }

        /// <summary>
        /// Get all cities in a certain province.
        /// </summary>
        /// <param name="province">A string with the name of the province.</param>
        /// <returns>A List with the cities information</returns>
        public List<CityInfo> DisplayProvinceCities(string province)
        {
            return CityCatalogue.Values.Where(city => city.Province == province).ToList();
        }

        /// <summary>
        /// Ranks all provinces by population.
        /// </summary>
        /// <returns>A Dictionary with the province name and population.</returns>
        public Dictionary<string, ulong> RankProvincesByPopulation()
        {
            return (from city in CityCatalogue.Values group city by new { city.Province } into province select new { province.Key.Province, Population = (ulong)province.Sum(prov => (decimal)prov.Population) }).OrderBy(prov => prov.Population).ToDictionary(x => x.Province, y => y.Population);
        }

        /// <summary>
        /// Ranks provinces on how many cities in each province.
        /// </summary>
        /// <returns>A Dictionary with the province name and count of cities.</returns>
        public Dictionary<string, int> RankProvincesByCities()
        {
            return (from city in CityCatalogue.Values group city by new { city.Province } into province select new { province.Key.Province, Count = province.Count() }).OrderBy(prov => prov.Count).ToDictionary(x => x.Province, y => y.Count);
        }

        /// <summary>
        /// Gets capital of a province.
        /// </summary>
        /// <param name="province">A string with the name of the province.</param>
        /// <returns>A string with the capital name, a double with latitude and a double with longitude.</returns>
        public (string, double, double) GetCapital(string province)
        {
            CityInfo city = CityCatalogue.Values.Where(city => city.Capital == province).First();
            return (city.CityName, city.Latitude, city.Longitude);
        }
    }   // end Statistics class
}