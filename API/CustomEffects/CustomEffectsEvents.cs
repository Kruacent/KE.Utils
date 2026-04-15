using KE.Utils.API.CustomStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.CustomEffects
{
    internal class CustomEffectsEvents
    {

        private static bool _event = false;
        public static void SubscribeEvents()
        {
            if (!_event)
            {
                LabApi.Events.Handlers.PlayerEvents.Joined += OnJoined;
                _event = true;
            }

        }


        public static void UnsubscribeEvents()
        {
            if (_event)
            {
                LabApi.Events.Handlers.PlayerEvents.Joined -= OnJoined;
                _event = false;
            }
        }

        private static void OnJoined(LabApi.Events.Arguments.PlayerEvents.PlayerJoinedEventArgs ev)
        {
            ev.Player.ReferenceHub.gameObject.AddComponent<CustomEffectController>();
        }
    }
}
