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
        public string Capital { get; set; }

        // constructors
        public CityInfo(string cityID, string cityName, string cityAscii, string population, string province, string latitude, string longitude, string capital)
        {
            if (ulong.TryParse(cityID, out ulong id))
                CityID = id;

            if (ulong.TryParse(population, out ulong pop))
                Population = pop;

            if (double.TryParse(latitude, out double lat))
                Latitude = lat;

            if (double.TryParse(longitude, out double lng))
                Longitude = lng;
            
            switch (capital)
            {
                case "admin":
                    Capital = province;
                    break;
                case "primary":
                    Capital = cityName;
                    break;
                default:
                    Capital = capital;
                    break;
            }

            CityName = cityName;
            CityAscii = cityAscii;
            Province = province;
        }

        public CityInfo(ulong cityID, string cityName, string cityAscii, ulong population, string province, double latitude, double longitude, string capital)
        {
            CityID = cityID;
            CityName = cityName;
            CityAscii = cityAscii;
            Population = population;
            Province = province;
            Latitude = latitude;
            Longitude = longitude;
            Capital = capital;
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
