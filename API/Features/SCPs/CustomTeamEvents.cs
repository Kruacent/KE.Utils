using LabApi.Events.Arguments.PlayerEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Features.Teams
{
    public static class CustomTeamEvents
    {

        private static bool _event = false;
        public static void SubscribeEvents()
        {
            if (!_event)
            {
                LabApi.Events.Handlers.PlayerEvents.ChangingRole += OnChangingRole;
                _event = true;
            }

        }


        public static void UnsubscribeEvents()
        {
            if (_event)
            {
                LabApi.Events.Handlers.PlayerEvents.ChangingRole -= OnChangingRole;
                _event = false;
            }
        }

        private static void OnChangingRole(PlayerChangingRoleEventArgs ev)
        {
            
        }
    }
}
