using Exiled.API.Features;
using RueI.API;
using RueI.API.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Displays.DisplayRueI
{
    public class DisplayHandler
    {
        public static DisplayHandler Instance { get; } = new();

        private DisplayHandler() { }





        public void AddHint(Player player)
        {
            RueDisplay display = RueDisplay.Get(player);

            Tag tag = new();

            


        }
    }
}
