using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using KE.Utils.API.Interfaces;
using LabApi.Events.Arguments.PlayerEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace KE.Utils.API.Settings.GlobalSettings
{
    public class GlobalSettingsHandler : IUsingEvents
    {

        private static GlobalSettingsHandler _instance = null;
        public static GlobalSettingsHandler Instance
        {
            get
            {
                if(_instance is null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }

        private GlobalSettingsHandler() { }




        private IEnumerable<GlobalSetting> settings = null;
        private List<SettingBase> settingsbase = null;

        public bool IsLoaded => settings != null;


        private bool _event = false;

        public void TryLoad()
        {
            if (IsLoaded) return;


            settings = ReflectionHelper.GetObjects<GlobalSetting>();


            settingsbase = new(settings.Count());

            foreach (GlobalSetting setting in settings)
            {
                setting.Create();
                settingsbase.Add(setting.Setting);


            }

            
            SettingBase.Register(settingsbase);

        }


        public T Get<T>() where T : GlobalSetting
        {
            foreach (GlobalSetting setting in settings)
            {
                if(setting is T)
                {
                    return setting as T;
                }
            }
            return null;
        }

        public bool TryGet<T>(out T setting) where T : GlobalSetting
        {
            setting = Get<T>();
            return setting != null;
        }

        public void SubscribeEvents()
        {
            if (_event) return;

            LabApi.Events.Handlers.PlayerEvents.Joined += AddPlayer;
            _event = true;


        }

        public void UnsubscribeEvents()
        {
            if (!_event) return;
            LabApi.Events.Handlers.PlayerEvents.Joined -= AddPlayer;
            _event = false;
        }

        private void AddPlayer(PlayerJoinedEventArgs ev)
        {
            Log.Info("join");
            ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);
        }
    }
}
