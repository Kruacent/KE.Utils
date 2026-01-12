using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;
using UserSettings.ServerSpecific.Examples;

namespace KE.Utils.API.Settings
{
    public class SettingsPage
    {
        public readonly string Name;

        public readonly List<ServerSpecificSettingBase> OwnEntries;

        public ServerSpecificSettingBase[] CombinedEntries { get; private set; }

        public SettingsPage(string name, List<ServerSpecificSettingBase> entries)
        {
            Name = name;
            OwnEntries = entries;
        }

        public void GenerateCombinedEntries(List<ServerSpecificSettingBase> pageSelectorSection)
        {
            Log.Info(Name + " generating entries");
            int num = pageSelectorSection.Count + OwnEntries.Count + 1;
            CombinedEntries = new ServerSpecificSettingBase[num];
            
            int num2 = 0;
            List<ServerSpecificSettingBase> array = pageSelectorSection;
            foreach (ServerSpecificSettingBase serverSpecificSettingBase in array)
            {

                CombinedEntries[num2++] = serverSpecificSettingBase;
            }
            CombinedEntries[num2++] = new SSGroupHeader(Name);
            array = OwnEntries;
            foreach (ServerSpecificSettingBase serverSpecificSettingBase2 in array)
            {

                CombinedEntries[num2++] = serverSpecificSettingBase2;
            }

            Log.Info(CombinedEntries is null);

        }

        public override string ToString()
        {
            string str = Name + " (" + OwnEntries.Count + ") ";

            if(CombinedEntries is null)
            {
                str += "null";
            }
            else
            {
                str += CombinedEntries.Length;
            }
            return str;
        }
    }
}
