using Exiled.API.Extensions;
using Exiled.API.Features;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace KE.Utils.API.KETextToy
{
    public class FollowingTextToy
    {

        private static HashSet<FollowingTextToy> list = new();
        public IReadOnlyCollection<Player> Following { get; }
        public TextToy Toy { get; }

        public const string EmptyText = " ";

        public bool OnlyMoveY { get; set; } = false;

        public string Text
        {
            get
            {
                return Toy.TextFormat;
            }
            set
            {
                Toy.TextFormat = value;
            }
        }

        public FollowingTextToy(IEnumerable<Player> players, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Following = players.ToList();
            Toy = TextToy.Create(position, rotation, scale, null, false);
            Toy.MovementSmoothing = 0;

            Toy.Spawn();

            foreach (Player player in Player.Enumerable)
            {
                if (!players.Contains(player))
                {
                    Log.Warn("hiding texttoy to " + player.Nickname);
                    player.Connection.Send(new ObjectHideMessage() { netId = Toy.Base.netId });
                }
            }
                


            list.Add(this);
            Start();
        }


        public void SyncToPlayers()
        {
            foreach(Player player in Following)
            {
                Vector3 dir = player.CameraTransform.position - Toy.Position;
                if (OnlyMoveY)
                {
                    dir.y = 0f;
                    dir.Normalize();
                }

                if (!dir.Equals(Vector3.zero))
                {
                    Quaternion rot = Quaternion.LookRotation(dir) * Quaternion.Euler(0f, 180f, 0f);
                    player.SendFakeSyncVar(Toy.Base.netIdentity, typeof(AdminToys.TextToy), nameof(AdminToys.TextToy.NetworkRotation), rot);
                }

            }
        }






        private static CoroutineHandle handle;
        public static void Start()
        {
            if (!handle.IsRunning)
            {
                Log.Info("starting followingTextToy");
                handle = Timing.RunCoroutine(Loop());
            }

        }

        public static void Stop()
        {
            Timing.KillCoroutines(handle);
        }

        public static bool IsRunning
        {
            get
            {
                return handle.IsRunning;
            }
            set
            {
                if (value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }


        public void Destroy()
        {
            list.Remove(this);
            Toy.Destroy();
            if(list.Count == 0)
            {
                Stop();
            }
        }


        private static IEnumerator<float> Loop()
        {
            while (true)
            {

                foreach(FollowingTextToy textToy in list.ToList())
                {
                    textToy.SyncToPlayers();
                }

                yield return Timing.WaitForOneFrame;

            }
        }






    }
}
