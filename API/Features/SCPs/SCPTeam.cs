using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using KE.CustomRoles.API.Features;
using KE.Utils.API.Interfaces;
using PlayerRoles.Visibility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KE.Utils.API.Features.SCPs
{
    public static class SCPTeam
    {
        private static HashSet<ReferenceHub> _scps = new();
        public static IReadOnlyCollection<ReferenceHub> SCPs => _scps;

        public static void AddSCP(ReferenceHub hub)
        {
            Log.Info("adding player");
            _scps.Add(hub);
        }

        public static void RemoveSCP(ReferenceHub hub)
        {
            Log.Info("removeign player");
            _scps.Remove(hub);
        }

        public static bool IsSCP(ReferenceHub hub)
        {
            return SCPs.Contains(hub);
        }
    }
}
