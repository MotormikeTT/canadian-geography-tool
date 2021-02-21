using System.Collections.Generic;
using System.Linq;
using System.Device.Location;

namespace Project1_Group_16.Classes
{
    class Statistics
    {
        // Properties
        internal Dictionary<string, CityInfo> CityCatalogue = new Dictionary<string, CityInfo>();

        public Statistics(string fileName, string fileType)
        {
            CityCatalogue = DataModeler.ParseFile(fileName, fileType);
        }

        public CityInfo DisplayCityInformation(string city)
        {
            return CityCatalogue[city];
        }

        public CityInfo DisplayLargestPopulationCity(string province)
        {
            return CityCatalogue.Where(city => city.Value.Province == province).OrderByDescending(city => city.Value.Population).First().Value;
        }

        public CityInfo DisplaySmallestPopulationCity(string province)
        {
            return CityCatalogue.Where(city => city.Value.Province == province).OrderBy(city => city.Value.Population).First().Value;
        }

        public (CityInfo, ulong, ulong) CompareCitiesPopulation(string city1, string city2)
        {
            return (CityCatalogue[city1].Population > CityCatalogue[city2].Population ? CityCatalogue[city1] : CityCatalogue[city2], CityCatalogue[city2].Population, CityCatalogue[city1].Population);
        }

        public (double, double) ShowCityOnMap(string city)
        {
            return (CityCatalogue[city].Latitude, CityCatalogue[city].Longitude);  // TO DO
        }

        public double CalculateDistanceBetweenCities(string city1, string city2)
        {
            GeoCoordinate city1Coord = new GeoCoordinate(CityCatalogue[city1].Latitude, CityCatalogue[city1].Longitude);
            GeoCoordinate city2Coord = new GeoCoordinate(CityCatalogue[city2].Latitude, CityCatalogue[city2].Longitude);

            return city1Coord.GetDistanceTo(city2Coord);
        }

        public ulong DisplayProvincePopulation(string province)
        {
            return (ulong)CityCatalogue.Where(city => city.Value.Province == province.ToLower()).Sum(city => (decimal)city.Value.Population);
        }

        public List<CityInfo> DisplayProvinceCities(string province)
        {
            return CityCatalogue.Values.Where(city => city.Province == province).ToList();
        }

        public Dictionary<string, ulong> RankProvincesByPopulation()
        {
            return (from city in CityCatalogue.Values group city by new { city.Province } into province select new { province.Key.Province, Population = (ulong)province.Sum(prov => (decimal)prov.Population) }).OrderBy(prov => prov.Population).ToDictionary(x => x.Province, y => y.Population);
        }

        public Dictionary<string, int> RankProvincesByCities()
        {
            return (from city in CityCatalogue.Values group city by new { city.Province } into province select new { province.Key.Province, Count = province.Count() }).OrderBy(prov => prov.Count).ToDictionary(x => x.Province, y => y.Count);
        }

        public (string, double, double) GetCapital(string province)
        {
            // return (CityCatalogue.Values.Where(city => city.)); TODO

            return ("capital", 0, 0);
        }
    }
}