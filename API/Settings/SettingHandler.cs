using Exiled.API.Features;
using KE.Utils.API.Interfaces;
using KE.Utils.API.Settings.EventArgs;
using LabApi.Events.Arguments.PlayerEvents;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace KE.Utils.API.Settings
{
    public class SettingHandler : IUsingEvents
    {
        private static SettingHandler _instance;

        public static SettingHandler Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }
        private SettingHandler()
        {
            
        }
        private bool _event = false;

        public const int navigId = 100;
        public void SubscribeEvents()
        {
            if (_event) return;
            ReferenceHub.OnPlayerRemoved += OnPlayerDisconnected;
            ServerSpecificSettingsSync.ServerOnSettingValueReceived += ServerOnSettingValueReceived;
            LabApi.Events.Handlers.PlayerEvents.Joined += AddPlayer;
            _event = true;


            Init();
        }
        public void UnsubscribeEvents()
        {
            if (!_event) return;
            ReferenceHub.OnPlayerRemoved -= OnPlayerDisconnected;
            ServerSpecificSettingsSync.ServerOnSettingValueReceived -= ServerOnSettingValueReceived;
            LabApi.Events.Handlers.PlayerEvents.Joined -= AddPlayer;
            _event = false;


        }


        private void Init()
        {

            _lastSentPages = new();

            /*_pinned = new SettingsPage("Pinned", new()
            {
                new SSDropdownSetting(navigId, "Page", GetPageDisplay(), 0, SSDropdownSetting.DropdownEntryType.ScrollableLoop),
            });*/


        }

        private string[] GetPageDisplay()
        {

            
            string[] array = new string[_pages.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = $"{_pages[i].Name} ({i + 1} out of {_pages.Count})";
            }
            return array;
        }



        private SettingsPage _pinned;
        private SSDropdownSetting _nav => _pinned?.OwnEntries[0] as SSDropdownSetting;

        private List<SettingsPage> _pages = new();

        private Dictionary<ReferenceHub, int> _lastSentPages;

        public void AddPages(SettingsPage page)
        {
            if (!_event)
            {
                Log.Warn("don't forget to subscribe the events");
            }

            if(page is null)
            {
                throw new ArgumentNullException("page null");
            }

            if (string.IsNullOrEmpty(page.Name))
            {
                throw new ArgumentException("page name null or empty");
            }

            if (_pages.Contains(page))
            {
                throw new ArgumentException("page already created");
            }

            _pages.Add(page);



            _pages.ForEach(delegate (SettingsPage page)
            {
                page.GenerateCombinedEntries(_pinned.OwnEntries);
            });

            List<ServerSpecificSettingBase> allSettings = new(_pinned.OwnEntries);
            _pages.ForEach(delegate (SettingsPage page)
            {
                allSettings.AddRange(page.OwnEntries);
                Log.Info(page);
            });

            ServerSpecificSettingsSync.DefinedSettings = allSettings.ToArray();
            ServerSpecificSettingsSync.SendToAll();
        }



        private void ServerOnSettingValueReceived(ReferenceHub hub, ServerSpecificSettingBase setting)
        {
            try
            {
                ValueReceived?.Invoke(new ValueReceivedEventArgs(setting,hub));
                if (setting is SSDropdownSetting sSDropdownSetting && sSDropdownSetting.SettingId == _nav.SettingId)
                {
                    ServerSendSettingsPage(hub, sSDropdownSetting.SyncSelectionIndexValidated);
                }

                if(setting is SSKeybindSetting keybind)
                {
                    KeybindPressedEventArgs args = new(keybind, keybind.SyncIsPressed, hub);
                    OnKeybindPressed?.Invoke(args);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void ServerSendSettingsPage(ReferenceHub hub, int settingIndex)
        {
            if (!_lastSentPages.TryGetValue(hub, out var value) || value != settingIndex)
            {
                
                _lastSentPages[hub] = settingIndex;
                ServerSpecificSettingsSync.SendToPlayer(hub, _pages[settingIndex].CombinedEntries);
                
            }


        }

        private void AddPlayer(PlayerJoinedEventArgs ev)
        {
            _nav?.SendDropdownUpdate(GetPageDisplay());
            ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);
        }

        private void OnPlayerDisconnected(ReferenceHub hub)
        {
            _lastSentPages?.Remove(hub);
        }



        public static event Action<KeybindPressedEventArgs> OnKeybindPressed = delegate { };

        public static event Action<ValueReceivedEventArgs> ValueReceived = delegate { };




    }
}
