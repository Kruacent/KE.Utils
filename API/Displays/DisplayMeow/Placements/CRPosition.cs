using HintServiceMeow.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Displays.DisplayMeow.Placements
{
    public class AbilitiesPosition : HintPosition
    {
        public override float Xposition => -350;

        public override float Yposition => 800;

        public override HintAlignment HintAlignment => HintAlignment.Left;
    }


    public class CurrentCustomRolePosition : HintPosition
    {
        public override float Xposition => 600;

        public override float Yposition => 1030;

        public override HintAlignment HintAlignment => HintAlignment.Center;
    }
}
