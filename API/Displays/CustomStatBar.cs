using KE.Utils.API.CustomStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.Displays
{
    public class CustomStatBar : IStatBar
    {

        public CustomStatBase customStat;

        public Color ColorBar { get; }
        public Color ColorText { get; }

        public CustomStatBar()
        {

        }

    }
}
