using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Server;
using KE.CustomRoles.API.Features;
using LabApi.Events.Arguments.PlayerEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Features.SCPs
{
    public static class CustomTeamEvents
    {

        private static bool _event = false;
        public static void SubscribeEvents()
        {
            if (!_event)
            {
                Exiled.Events.Handlers.Server.EndingRound += OnRoundEnding;
                LabApi.Events.Handlers.PlayerEvents.ChangedRole += OnChangedRole;
                _event = true;
            }

        }


        public static void UnsubscribeEvents()
        {
            if (_event)
            {
                Exiled.Events.Handlers.Server.EndingRound -= OnRoundEnding;
                LabApi.Events.Handlers.PlayerEvents.ChangedRole -= OnChangedRole;
                _event = false;
            }
        }

        private static void OnRoundEnding(EndingRoundEventArgs ev)
        {
            if (ev.ClassList.mtf_and_guards != 0 || ev.ClassList.scientists != 0) ev.IsAllowed = false;
            else if (ev.ClassList.class_ds != 0 || ev.ClassList.chaos_insurgents != 0) ev.IsAllowed = false;
            else if (SCPTeam.SCPs.Count > 0) ev.IsAllowed = true;
            else ev.IsAllowed = true;

        }
        private static void OnChangedRole(PlayerChangedRoleEventArgs ev)
        {
            KECustomRole kecr = KECustomRole.Get(ev.Player).FirstOrDefault();


            if (SCPTeam.IsSCP(ev.Player.ReferenceHub) && (kecr is not CustomSCP || !ev.NewRole.RoleTypeId.IsScp()))
            {
                SCPTeam.RemoveSCP(ev.Player.ReferenceHub);
            }



            if (ev.NewRole.RoleTypeId.IsScp() || kecr is CustomSCP)
            {

                SCPTeam.AddSCP(ev.Player.ReferenceHub);
            }

            

        }
    }
}
