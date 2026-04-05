using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Server;
using KE.CustomRoles.API.Features;
using LabApi.Events.Arguments.PlayerEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RoundSummary;
using LeadingTeam = Exiled.API.Enums.LeadingTeam;

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
                LabApi.Events.Handlers.ServerEvents.RoundEndingConditionsCheck += OnRoundEndingConditionsCheck;
                LabApi.Events.Handlers.PlayerEvents.ChangedRole += OnChangedRole;
                _event = true;
            }

        }

        

        public static void UnsubscribeEvents()
        {
            if (_event)
            {
                Exiled.Events.Handlers.Server.EndingRound -= OnRoundEnding;
                LabApi.Events.Handlers.ServerEvents.RoundEndingConditionsCheck -= OnRoundEndingConditionsCheck;
                LabApi.Events.Handlers.PlayerEvents.ChangedRole -= OnChangedRole;
                _event = false;
            }
        }

        private static void OnRoundEnding(EndingRoundEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                CheckRoundEnd(out LeadingTeam leadingTeam, out _);
                ev.LeadingTeam = leadingTeam;
            }
        }
        private static void OnRoundEndingConditionsCheck(LabApi.Events.Arguments.ServerEvents.RoundEndingConditionsCheckEventArgs ev)
        {
            ev.CanEnd = CheckRoundEnd(out _, out _);
        }


        public static bool CheckRoundEnd(out LeadingTeam leadingTeam,out EndRoundClassList classList)
        {
            classList = new EndRoundClassList();

            return classList.CanRoundEnd(out leadingTeam);
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
