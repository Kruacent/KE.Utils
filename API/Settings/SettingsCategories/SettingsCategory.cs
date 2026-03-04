using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
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



        public static void Register(bool reset = false,bool sendToAll =true)
        {

            List<SettingsCategory> orderedList = _list.OrderByDescending(s => s.Priority).ToList();
            List<SettingBase> AllSettings = new();

            for(int i = 0; i < orderedList.Count; i++)
            {
                AllSettings.AddRange(orderedList[i].Conct());
            }

            if (reset)
            {
                ServerSpecificSettingsSync.DefinedSettings = AllSettings.Select((SettingBase s) => s.Base).ToArray();
            }
            else
            {
                ServerSpecificSettingsSync.DefinedSettings = (ServerSpecificSettingsSync.DefinedSettings ?? Array.Empty<ServerSpecificSettingBase>()).Concat(AllSettings.Select((SettingBase s) => s.Base)).ToArray();
            }


            if (sendToAll)
            {
                SettingBase.SendToAll();
            }
            
        }


    }
}
