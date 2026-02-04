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
    public class SCPTeam : IUsingEvents
    {


        private static SCPTeam instance = null;

        public static SCPTeam Instance
        {
            get
            {
                if (instance == null)
                    instance = new();
                return instance;
            }
        }

        private SCPTeam() { }
        private bool eventFlag = false;


        private HashSet<Player> Primary = new();
        private HashSet<Player> Secondary = new();

        public IEnumerable<Player> AllSCP => Primary.Union(Secondary);




        public void AddPrimary(Player player)
        {
            Primary.Add(player);
        }

        public void RemovePrimary(Player player)
        {
            Primary.Remove(player);
            if (Primary.Count == 0)
            {
                Upgrade(Secondary.GetRandomValue());
            }
        }

        public void AddSecondary(Player player)
        {
            Secondary.Add(player);
        }

        public void RemoveSecondary(Player player)
        {
            Secondary.Remove(player);
        }

        public void Upgrade(Player player)
        {
            if (!Secondary.Contains(player)) return;


            Secondary.Remove(player);
            Primary.Add(player);
        }


        public void Reset()
        {
            UnsubscribeEvents();
            Primary.Clear();
            Secondary.Clear();
        }


        public Player GetPrimary()
        {
            return Primary.FirstOrDefault();
        }

        public Player GetRandomSCP()
        {
            return AllSCP.GetRandomValue();
        }

        public void SubscribeEvents()
        {
            if (eventFlag) return;

            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            eventFlag = true;
        }

        public void UnsubscribeEvents()
        {
            if (!eventFlag) return;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;


            eventFlag = false;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Player player = ev.Player;
            

            if (!AllSCP.Contains(player)) return;

            if (Primary.Contains(player))
            {
                RemovePrimary(player);
            }


            if (ev.NewRole == PlayerRoles.RoleTypeId.Scp0492)
            {
                AddSecondary(player);
            }
            

        }
    }
}
