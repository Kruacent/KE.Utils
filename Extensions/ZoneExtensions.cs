using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.Extensions
{
    public static class ZoneExtensions
    {

        /// <summary>
        /// Check if a <see cref="ZoneType"/> is Safe (Decontamination,Warhead)
        /// </summary>
        /// <returns>return true if the zone is safe for a <see cref="Player"/> ; false otherwise</returns>
        public static bool IsSafe(this ZoneType zone)
        {
            bool result = true;
            if (zone == ZoneType.LightContainment)
            {
                result = Map.DecontaminationState < DecontaminationState.Countdown;
            }

            switch (zone)
            {
                case ZoneType.LightContainment:
                case ZoneType.HeavyContainment:
                case ZoneType.Entrance:
                    result &= !Warhead.IsDetonated;
                    break;
            }
            return result;
        }

        public static string GetName(this ZoneType zone)
        {
            return zone switch
            {
                ZoneType.LightContainment => "Light Containment Zone",
                ZoneType.HeavyContainment => "Heavy Containment Zone",
                ZoneType.Entrance => "Entrance Zone",
                ZoneType.Surface => "Surface Zone",
                _ => "Not found"
            };
        }




    }
}
