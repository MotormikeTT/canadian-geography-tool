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

        // constructors
        public CityInfo(string cityID, string cityName, string cityAscii, string population, string province, string latitude, string longitude)
        {
            if(ulong.TryParse(cityID, out ulong id))
                this.CityID = id;

            if (ulong.TryParse(population, out ulong pop))
                this.Population = pop;

            if (double.TryParse(latitude, out double lat))
                this.Latitude = lat;

            if (double.TryParse(longitude, out double lng))
                this.Longitude = lng;

            this.CityName = cityName;
            this.CityAscii = cityAscii;
            this.Province = province;
        }

        public CityInfo(ulong cityID, string cityName, string cityAscii, ulong population, string province, double latitude, double longitude)
        {
            this.CityID = cityID;
            this.CityName = cityName;
            this.CityAscii = cityAscii;
            this.Population = population;
            this.Province = province;
            this.Latitude = latitude;
            this.Longitude = longitude;
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
