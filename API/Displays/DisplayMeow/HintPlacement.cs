using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Displays.DisplayMeow
{
    public readonly struct HintPlacement : IEquatable<HintPlacement>
    {
        public float YCoordinate { get; }
        public float XCoordinate { get; }
        public HintAlignment HintAlignment { get; } = HintAlignment.Center;

        public string Name { get; }


        public bool Equals(HintPlacement obj)
        {
            return YCoordinate == obj.YCoordinate && XCoordinate == obj.XCoordinate && Name == obj.Name;
        }


        public HintPlacement(float xCoordinate, float yCoordinate, HintAlignment alignment = HintAlignment.Center)
        {
            YCoordinate = yCoordinate;
            XCoordinate = xCoordinate;
            HintAlignment = alignment;
            Name = string.Empty;
        }

        public HintPlacement(string name,float xCoordinate, float yCoordinate, HintAlignment alignment = HintAlignment.Center)
        {
            YCoordinate = yCoordinate;
            XCoordinate = xCoordinate;
            HintAlignment = alignment;
            Name = name;
        }

        public string GetId(Player player)
        {
            return $"{player.Id}_{Name}_{XCoordinate}_{YCoordinate}_{HintAlignment}";
        }   
 
    }
}
