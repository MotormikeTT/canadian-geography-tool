using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Group_16
{
    public class CityPopulationChangeEvent
    {
        public ulong CurrentPopulation { get; set; }
        public ulong NewPopulation { get; set; }

        public CityPopulationChangeEvent(ulong CurrentPopulation, ulong NewPopulation)
        {
            this.CurrentPopulation = CurrentPopulation;
            this.NewPopulation = NewPopulation;
        }

        // Publisher
        public class UpdatePopulation
        {
            ulong cityPopulation;
            public ulong CityPopulation
            {
                get { return cityPopulation; }
                set
                {
                    var e = new CityPopulationChangeEvent(cityPopulation, value);
                    OnPopulationChange(e);

                    cityPopulation = value;
                }
            }

            public delegate void CityPopulationChangeHandler(CityPopulationChangeEvent e);
            // Define event
            public event CityPopulationChangeHandler PopulationChange; 
            // Raise the event
            protected virtual void OnPopulationChange(CityPopulationChangeEvent e)
            {
                PopulationChange.Invoke(e);
            }
        }
    }
}
