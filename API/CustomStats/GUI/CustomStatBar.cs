using Exiled.API.Features;
using HintServiceMeow.Core.Models.Hints;
using KE.Utils.API.Displays.DisplayMeow;
using KE.Utils.API.Displays.DisplayMeow.Placements;
using KE.Utils.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.CustomStats.GUI
{
    public abstract class CustomStatBar : CustomStatBase, IStatBar
    {
        public abstract Color ColorBar { get; }
        public abstract Color ColorText { get; }
        public abstract int Width { get; }
        public abstract char Segment { get; }
        public AbstractHint BarHint { get; private set;}
        public AbstractHint ValueHint { get; private set;}

        public virtual StatusBar.AutoHideType AutoHide => StatusBar.AutoHideType.AlwaysVisible;

        public override void Init(ReferenceHub ply)
        {
            base.Init(ply);
            BarHint = DisplayHandler.Instance.CreateAuto(Player.Get(Hub), arg => GetRaw(), StatBarPosition.HintPlacement);
            ValueHint = DisplayHandler.Instance.CreateAuto(Player.Get(Hub), arg => GetRawValue(), StatTextPosition.HintPlacement);
        }


        protected StatBarPosition StatBarPosition = new SCP106StatBarPosition();
        protected StatBarPosition StatTextPosition = new SCP106StatTextPosition();
        private static string EmptyColor = ColorUtility.ToHtmlStringRGB(new Color32(0, 0, 0, 0));
        

        public virtual string GetRawValue()
        {
            string result = " ";

            if (!Check())
            {
                return result;
            }

            result = "<b><color=#" + ColorUtility.ToHtmlStringRGB(ColorText) + ">" + Math.Round(CurValue,0) + "</color></b>";
            return result;


        }

        public virtual bool Check()
        {

            if(Hub is null)
            {
                return false;
            }

            if(this is IRoleStat role && role.Role != Hub.roleManager.CurrentRole.RoleTypeId)
            {
                return false;
            }

            return true;



        }
        public virtual string GetRaw()
        {
            string result = " ";

            if (!Check())
            {
                return result;
            }



            int filled = (int)((float)CurValue / MaxValue * Width);
            result = "<color=#"+ ColorUtility.ToHtmlStringRGB(ColorBar) +">[" + 
                new string(Segment, filled) + "</color>" +
                 "<color=#" + EmptyColor + ">"+ new string(Segment, Width - filled) + "</color>"
                + "<color=#" + ColorUtility.ToHtmlStringRGB(ColorBar)+">]</color>";

            
            return result;
            


        }



    }
}
