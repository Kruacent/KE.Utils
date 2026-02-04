using Exiled.API.Features;
using Exiled.API.Enums;
using Discord;
using System.Linq;
using Exiled.API.Extensions;
using UnityEngine;

namespace KE.Utils.Extensions
{
    public static class RoomExtensions
    {


        public static Room RandomSafeRoom(this ZoneType zone)
        {
            return Room.List.Where(r => r.IsSafe() && r.Zone == zone).GetRandomValue();
        }


        /// <summary>
        /// Check if a <see cref="Room"/> is Safe (Decontamination,Warhead)
        /// <para>
        /// Note: Does NOT check if the <see cref="Room"/> is safe to teleport in (ex : TestRoom)
        /// </para>
        /// </summary>
        /// <returns>return true if the <see cref="Room"/> is safe for a <see cref="Player"/> ; false otherwise</returns>
        public static bool IsSafe(this Room room)
        {
            return room.Zone.IsSafe();
        }


        /// <summary>
        /// Check if a <see cref="ZoneType"/> is Safe (Decontamination,Warhead)
        /// </summary>
        /// <returns>return true if the zone is safe for a <see cref="Player"/> ; false otherwise</returns>
        public static bool IsSafe(this ZoneType zone)
        {
            bool result = true;
            if (zone == ZoneType.LightContainment)
            {
                result = Map.DecontaminationState < DecontaminationState.Countdown;
            }
                
            switch (zone)
            {
                case ZoneType.LightContainment:
                case ZoneType.HeavyContainment:
                case ZoneType.Entrance:
                    result &= !Warhead.IsDetonated;
                    break;
            }
            return result;
        }

        //by @marcosvll2 on discord
        public static Vector3 GetValidPosition(this Room room)
        {
            Vector3 offset = Vector3.zero;
            switch (room.Type)
            {
                case RoomType.HczStraightPipeRoom:
                    offset = new Vector3(2.96f, 1f, -6.19f);
                    break;
                case RoomType.Hcz127:
                    offset = new Vector3(2.37f, 1f, 0.82f);
                    break;
                case RoomType.LczCheckpointA:
                case RoomType.LczCheckpointB:
                case RoomType.HczTesla:
                    offset = new Vector3(5.34f, 1f, 0.12f);
                    break;
                case RoomType.HczTestRoom:
                    offset = new Vector3(6.53f, 1f, 5.48f);
                    break;
                case RoomType.HczCrossRoomWater:
                    offset = new Vector3(-5f, 1f, 0f);
                    break;
                case RoomType.HczNuke:
                    offset = new Vector3(-3.14f, 1f, -0.12f);
                    break;
                case RoomType.HczArmory:
                    offset = new Vector3(-2.58f, 1f, 0f);
                    break;
                case RoomType.Hcz939:
                    offset = new Vector3(2.04f, 1f, -0.45f);
                    break;
                case RoomType.Lcz330:
                    offset = new Vector3(-4.50f, 1f, 0f);
                    break;
                case RoomType.Lcz173:
                    offset = new Vector3(-4.28f, 1f, 0f);
                    break;
                case RoomType.EzCollapsedTunnel:
                case RoomType.EzShelter:
                    offset = new Vector3(0f, 1f, 4.26f);
                    break;

            }
            return room.WorldPosition(offset) + Vector3.up;
        }

    }
}
