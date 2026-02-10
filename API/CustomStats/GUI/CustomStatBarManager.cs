using HintServiceMeow.Core.Utilities;
using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp106;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.API.CustomStats.GUI
{
    public class CustomStatBarManager : MonoBehaviour
    {



        private ReferenceHub _hub;
        private CustomPlayerStat _cps;


        private List<CustomStatBar> positions;

        public void Init()
        {
            _hub = ReferenceHub.GetHub(base.gameObject);
            _cps = _hub.GetComponent<CustomPlayerStat>();
            positions = new();
        }



        public void OnDestroy()
        {
            positions = null;
        }


        private float GetSpacing()
        {
            float result = 0f;


            //todo find the good size
            float sizeHealthBar = 1f;
            float sizeStamina = 1f;
            float sizeHs = 1f;
            float sizeVigor = 1f;


            if(_hub.roleManager.CurrentRole is IHealthbarRole health)
            {
                result += sizeHealthBar;
            }

            if(_hub.playerStats.TryGetModule<StaminaStat>(out var stamina))
            {
                if(stamina.CurValue < stamina.MaxValue)
                {
                    result += sizeStamina; 
                }
            }
            IHumeShieldProvider.GetForHub(_hub, out bool humeShieldBarVisible,out _, out _, out _);

            if (humeShieldBarVisible)
            {
                result += sizeHs; 
            }

            if (_hub.playerStats.TryGetModule<AhpStat>(out var ahp))
            {
                if (ahp.CurValue > ahp.MinValue)
                {
                    result += sizeHs; 
                }
            }


            if (_hub.playerStats.TryGetModule<VigorStat>(out var vigor))
            {
                result += sizeVigor;
            }

            return result;
        }


        /*
        private void Update()
        {
            CustomStatBar[] stats = _cps.StatBars;
            for (int i =0;i < stats.Length; i++)
            {
                CustomStatBar stat = stats[i];

                if((stat.AutoHide == StatusBar.AutoHideType.WhenFull && stat.NormalizedValue == 1) 
                    || stat.AutoHide == StatusBar.AutoHideType.WhenEmpty && stat.NormalizedValue == 0)
                {
                    //hide
                }
                else
                {
                    //show
                }
            }
            
        }
        */






    }
}
