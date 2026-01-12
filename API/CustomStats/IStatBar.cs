using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.CustomStats
{
    public interface IStatBar
    {
        public abstract Color ColorBar { get; }
        public abstract Color ColorText { get; }



    }
}
