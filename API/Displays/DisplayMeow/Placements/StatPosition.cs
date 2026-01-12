using HintServiceMeow.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Displays.DisplayMeow.Placements
{
    public class AdditionalStatPosition : HintPosition
    {
        public override float Xposition => 60;

        public override float Yposition => 1050;

        public override HintAlignment HintAlignment => HintAlignment.Left;
    }

    public class StatBarPosition : HintPosition
    {
        public override float Xposition => -350;

        public override float Yposition => 1000;

        public override HintAlignment HintAlignment => HintAlignment.Left;
    }

    public class SCP106StatBarPosition : StatBarPosition
    {
        public override float Xposition => -310;

        public override float Yposition => 940;

        public override HintAlignment HintAlignment => HintAlignment.Left;
        public override string Name => "BarStat";
    }

    public class SCP106StatTextPosition : StatBarPosition
    {
        public override float Xposition => -157;

        public override float Yposition => 941;

        public override HintAlignment HintAlignment => HintAlignment.Left;
        public override string Name => "TextStat";
    }
}
