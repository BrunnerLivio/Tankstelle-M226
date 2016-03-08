using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    [Serializable]
    public class GasPump
    {
        private GasStation gasStation;
        public GasPump(GasStation gasStation)
        {
            this.gasStation = gasStation;
        }
    }
}
