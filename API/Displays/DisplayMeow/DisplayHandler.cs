using Exiled.API.Features;
using Hints;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using System;
using static HintServiceMeow.Core.Models.HintContent.AutoContent;




//damn why the same name
using MHint = HintServiceMeow.Core.Models.Hints.Hint;

namespace KE.Utils.API.Displays.DisplayMeow
{
    public class DisplayHandler
    {
        public static DisplayHandler Instance { get; } = new();

        private DisplayHandler() { }





        public AbstractHint AddHint(HintPlacement hintPlacement, Player player, string text, float delay)
        {
            var dis = PlayerDisplay.Get(player);
            string id = hintPlacement.GetId(player);
            AbstractHint hint;
            if (!dis.TryGetHint(id, out var aHint))
            {

                hint = new MHint()
                {
                    Text = text,
                    XCoordinate = hintPlacement.XCoordinate,
                    YCoordinate = hintPlacement.YCoordinate,
                    Alignment = hintPlacement.HintAlignment,
                    Id = id

                };
                dis.AddHint(hint);

            }
            else
            {
                hint = aHint;
                hint.Hide = false;
                hint.Text = text;
            }
            

            
            hint.HideAfter(delay);
            return hint;
        }



        public void HideHint(HintPlacement hintPlacement, Player player,bool hide = true)
        {
            var dis = PlayerDisplay.Get(player);

            string id = hintPlacement.GetId(player);

            AbstractHint hint = dis.GetHint(id);

            if(hint is not null)
            {
                hint.Hide = hide;
            }
        }

        public AbstractHint GetHint(Player player,HintPlacement placement)
        {
            return PlayerDisplay.Get(player).GetHint(placement.GetId(player));
        }


        public MHint CreateAuto(Player player, TextUpdateHandler update, HintPlacement hintPlacement, HintSyncSpeed syncSpeed = HintSyncSpeed.Fastest)
        {
            string id = hintPlacement.GetId(player);
            Log.Info("auto at " + id);
            var gint = new MHint()
            {
                XCoordinate = hintPlacement.XCoordinate,
                YCoordinate = hintPlacement.YCoordinate,
                Alignment = hintPlacement.HintAlignment,
                AutoText = update,
                SyncSpeed = syncSpeed,
                Id = id
            };

            PlayerDisplay.Get(player).AddHint(gint);
            return gint;
        }


        public void RemoveHint(Player player,HintPlacement placement)
        {
            PlayerDisplay.Get(player).RemoveHint(placement.GetId(player));
        }
        public void RemoveHint(Player player, AbstractHint hint)
        {
            PlayerDisplay.Get(player).RemoveHint(hint);
        }

        public bool HasHint(Player player,HintPlacement placement)
        {
            return PlayerDisplay.Get(player).HasHint(placement.GetId(player));
        }


    }
}
