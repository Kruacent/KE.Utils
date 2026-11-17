using Exiled.API.Features;
using KE.Utils.API.Exceptions;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace KE.Utils.API.Sounds
{
    public class SoundPlayer
    {
        private static SoundPlayer _instance;

        public static SoundPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }

        private SoundPlayer() { }

        private bool _loaded = false;
        public bool Loaded => _loaded;

        public static readonly HashSet<string> clips = new();

        public static string SoundLocation => Path.Combine(Paths.Configs,"Sounds");
        public static void Load() => Instance.TryLoad();
        public void TryLoad()
        {
            if (Loaded)
            {
                return;
            }

            Timing.RunCoroutine(AsyncLoad());
            
        }


        public void UnloadAllClips()
        {
            foreach(string clip in clips)
            {
                AudioClipStorage.DestroyClip(clip);
            }
        }


        private IEnumerator<float> AsyncLoad()
        {
            try
            {
                if (Directory.Exists(SoundLocation))
                {

                    int countLoaded = 0;
                    int count = 0;

                    LoadRecursive(SoundLocation, ref countLoaded, ref count);

                    Log.Info($"loaded {countLoaded}/{count} clips");
                }
                else
                {
                    Log.Warn("Directory not found. creating...");
                    Directory.CreateDirectory(SoundLocation);
                }
            }
            catch (IOException e)
            {
                Log.Error(e);
            }

            _loaded = true;
            yield return 0;
        }


        private void LoadRecursive(string directory,ref int countLoaded,ref int count)
        {
            string[] rawfile = Directory.GetFiles(directory, "*.ogg");
            string[] directories = Directory.GetDirectories(directory);

            count += rawfile.Length;
            foreach (string file in rawfile)
            {
                string noExFile = Path.GetFileNameWithoutExtension(file);
                if (!clips.Add(noExFile))
                {
                    Log.Error("couldn't load clip " + noExFile +" : already exist");
                    continue;
                }

                if (!AudioClipStorage.LoadClip(file))
                {
                    Log.Error("couldn't load clip " + noExFile);
                }

                countLoaded++;
                
            }

            foreach(string direc in directories)
            {
                LoadRecursive(direc,ref countLoaded,ref count);
            }

            
        }



        /// <summary>
        /// Play a clip at a static point
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="pos"></param>
        /// <param name="volume"></param>
        /// <param name="maxDistance"></param>
        public AudioClipPlayback Play(string clipName, Vector3 pos, float volume = 50f, float maxDistance = 20f, bool isSpatial = true)
        {
            if (!Loaded) throw new ClipsNotLoadedException();
            Log.Debug($"playing {clipName} at {pos}");

            var audioPlayer = AudioPlayer.CreateOrGet($"{clipName} ({pos})", onIntialCreation: (p) =>
            {
                GameObject a = new GameObject();
                a.transform.position = pos;
                p.transform.parent = a.transform;
                Speaker speaker = p.AddSpeaker("main", isSpatial: isSpatial, maxDistance: maxDistance, minDistance: 1f);
                speaker.transform.parent = a.transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            audioPlayer.DestroyWhenAllClipsPlayed = true;
            return audioPlayer.AddClip(clipName, volume: volume);


        }

        /// <summary>
        /// Play a clip at a <see cref="GameObject"/>
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="pos"></param>
        /// <param name="volume"></param>
        /// <param name="maxDistance"></param>
        public AudioClipPlayback Play(string clipName, GameObject objectEmittingSound, float volume = 1f, float maxDistance = 20f, bool isSpatial = true)
        {
            if (!Loaded) throw new ClipsNotLoadedException();
            Log.Debug($"playing {clipName} at {objectEmittingSound}");

            var audioPlayer = AudioPlayer.CreateOrGet($"{clipName} ({objectEmittingSound})", onIntialCreation: (p) =>
            {

                p.transform.parent = objectEmittingSound.transform;
                p.transform.localPosition = Vector3.zero;
                Speaker speaker = p.AddSpeaker("main", isSpatial: isSpatial, maxDistance: maxDistance, minDistance: 1f);
                speaker.transform.parent = objectEmittingSound.transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            audioPlayer.DestroyWhenAllClipsPlayed = true;
            return audioPlayer.AddClip(clipName, volume: volume);
        }


        public AudioClipPlayback PlayClientOnly(string clipName, ReferenceHub player, float volume = 1f, float maxDistance = 20f, bool isSpatial = true)
        {
            if (!Loaded) throw new ClipsNotLoadedException();

            var audioPlayer = AudioPlayer.CreateOrGet($"{player.PlayerId}_Client",condition: (hub) =>
            {
                return hub == player;
            }   
            ,onIntialCreation: (p) =>
            {
                p.transform.parent = player.gameObject.transform;
                p.transform.localPosition = Vector3.zero;
                Speaker speaker = p.AddSpeaker("main", isSpatial: isSpatial, maxDistance: maxDistance, minDistance: 1f);
                speaker.transform.parent = player.gameObject.transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            return audioPlayer.AddClip(clipName, volume: volume);


        }

        public AudioClipPlayback PlayAlliesOnly(string clipName, ReferenceHub player, float volume = 1f, float maxDistance = 20f, bool isSpatial = true)
        {
            if (!Loaded) throw new ClipsNotLoadedException();
            var audioPlayer = AudioPlayer.CreateOrGet($"{player.PlayerId}_Allies", condition: (hub) =>
            {
                return Player.Get(player).Role.Side == Player.Get(hub).Role.Side;
            }
            , onIntialCreation: (p) =>
            {
                p.transform.parent = player.gameObject.transform;
                p.transform.localPosition = Vector3.zero;
                Speaker speaker = p.AddSpeaker("main", isSpatial: isSpatial, maxDistance: maxDistance, minDistance: 1f);
                speaker.transform.parent = player.gameObject.transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            return audioPlayer.AddClip(clipName, volume: volume);


        }

        public AudioClipPlayback Play(string clipName, ReferenceHub player, float volume = 1f, float maxDistance = 20f, bool isSpatial = true)
        {
            if (!Loaded) throw new ClipsNotLoadedException();
            var audioPlayer = AudioPlayer.CreateOrGet($"{player.PlayerId}_Client", onIntialCreation: (p) =>
            {
                p.transform.parent = player.gameObject.transform;
                p.transform.localPosition = Vector3.zero;
                Speaker speaker = p.AddSpeaker("main", isSpatial: isSpatial, maxDistance: maxDistance, minDistance: 1f);
                speaker.transform.parent = player.gameObject.transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            return audioPlayer.AddClip(clipName, volume: volume);
        }










    }
}
