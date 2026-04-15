using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using KE.Utils.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace KE.Utils.API.Settings.SettingsCategories
{
    public class SettingsCategory
    {
        private static List<SettingsCategory> _list = new();

        public ushort Priority { get; }

        public HeaderSetting Header { get; }


        public List<SettingBase> Settings { get; }

        private List<SettingBase> conct = null;


        public SettingsCategory(HeaderSetting header, ushort priority, List<SettingBase> settings)
        {

            Priority = priority;
            Header = header;
            //Header.Label += priority.ToString();

            Settings = settings.ToList();

            _list.Add(this);
        }

        protected virtual List<SettingBase> Conct()
        {

            if(conct == null)
            {
                conct =
                [
                    Header, 
                    .. Settings
                ];
            }

            return conct;

        }



        public static IReadOnlyCollection<SettingBase> AllSettings { get; private set; }


        public static void Register()
        {
            if (locked) return;

            List<SettingsCategory> orderedList = _list.OrderByDescending(s => s.Priority).ToList();
            List<SettingBase> allSettings = new();

            for (int i = 0; i < orderedList.Count; i++)
            {
                allSettings.AddRange(orderedList[i].Conct());
            }


            AllSettings = allSettings.AsReadOnly();
            ServerSpecificSettingsSync.DefinedSettings = AllSettings.Select(s => s.Base).ToArray();

            locked = true;
            //ServerSpecificSettingsSync.DefinedSettings = new ServerSpecificSettingBase[1];
            //ServerSpecificSettingsSync.SendOnJoinFilter = (ReferenceHub hub) =>
            //{
            //    return false;
            //};


            //KELog.Debug("allsettings length"+ AllSettings.Count);
            //KELog.Debug("definedsettings length" + ServerSpecificSettingsSync.DefinedSettings.Length);

            //if (reset)
            //{
            //    ServerSpecificSettingsSync.DefinedSettings = AllSettings.Select((SettingBase s) => s.Base).ToArray();
            //}
            //else
            //{
            
            //}

            SettingBase.SendToAll();
        }

        private static bool locked = false;

    }
}
