using System;
using System.Collections.Generic;
using System.Text;

namespace Project1_Group_16.Classes
{
    public class CityInfo
    {
        public ulong CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public ulong Population { get; set; }
        public string Province { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // constructor
        public CityInfo(ulong CityID, string CityName, string CityAscii, ulong Population, string Province, double Latitude, double Longitude)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CityAscii = CityAscii;
            this.Population = Population;
            this.Province = Province;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        /// <summary>
        /// Retrieves the province of the city
        /// </summary>
        /// <returns>province</returns>
        public string GetProvince()
        {
            return Province;
        }

        /// <summary>
        /// Retrieves the population in the city
        /// </summary>
        /// <returns>population</returns>
        public ulong GetPopulation()
        {
            return Population;
        }

        /// <summary>
        /// Retrieves the location of a city
        /// </summary>
        /// <returns>latitude and longitude</returns>
        public (double Latitude, double Longitude) GetLocation()
        {
            return (Latitude, Longitude);
        }

    }
}
