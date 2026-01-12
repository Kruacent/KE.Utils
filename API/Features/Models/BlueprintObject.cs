using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.Features.Models
{
    public abstract class BlueprintObject
    {




        public virtual Vector3 Position { get; set; } = Vector3.zero;

        public virtual Vector3 Rotation { get; set; } = Vector3.zero;

        public virtual Vector3 Scale { get; set; } = Vector3.one;


        public abstract GameObject Create(); 
    }
}
