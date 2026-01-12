using HintServiceMeow.Core.Enum;


namespace KE.Utils.API.Displays.DisplayMeow.Placements
{
    public abstract class HintPosition
    {
        public abstract float Xposition { get; }
        public abstract float Yposition { get; }
        public abstract HintAlignment HintAlignment { get; }
        public virtual string Name { get; } = string.Empty;



        private HintPlacement? placement = null;

        public HintPlacement HintPlacement
        {
            get
            {

                if(!placement.HasValue)
                {
                    placement = new HintPlacement(Name, Xposition, Yposition, HintAlignment);
                }
                return placement.Value;
            }
        }


    }
}
