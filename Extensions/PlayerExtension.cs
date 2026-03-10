using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using KE.CustomRoles.API.Features;
using KE.Utils.API.CustomStats;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KE.Utils.Extensions
{
    public static class PlayerExtension
    {
        public static void SetFakeInvis(this Player p, IEnumerable<Player> viewers)
        {
            Vector3 pos = p.Position;

            try
            {
                p.ReferenceHub.transform.localPosition = Vector3.zero;
                foreach (Player viewer in viewers)
                {
                    Server.SendSpawnMessage.Invoke(null, new object[2] { viewer.NetworkIdentity, viewer.Connection });
                }

                p.ReferenceHub.transform.localPosition = pos;
            }
            catch (Exception arg)
            {
                Log.Error(string.Format("{0}: {1}", "SetFakeInvis", arg));
            }
        }




        public static void AddLevelEffect<T>(this Player player, int addintensity) where T : StatusEffectBase
        {

            T effect = player.GetEffect<T>();


            byte newIntensity = (byte)Mathf.Clamp(effect.Intensity + addintensity, byte.MinValue, byte.MaxValue);

            effect.Intensity = newIntensity;

        }


        public static void ChangeRole(this Player player,RoleTypeId newRole,SpawnReason spawnReason,RoleSpawnFlags spawnFlags)
        {
            KECustomRole oldcr = null;
            bool flag = false;
            if(KECustomRole.TryGet(player, out var roles))
            {
                oldcr = (KECustomRole) roles.FirstOrDefault();
                Log.Info("found " + oldcr.Name);
                flag = oldcr.RoleCheck(newRole);
                Log.Info("flag " + flag);

            }


            player.Role.Set(newRole, spawnReason, spawnFlags);


            if (flag)
            {
                oldcr.AddRole(player);
            }
            
        }


    }
}
