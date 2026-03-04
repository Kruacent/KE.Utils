using Exiled.API.Features.Core.UserSettings;
using KE.Utils.API.Settings.SettingsCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Settings.GlobalSettings
{
    public abstract class GlobalSetting
    {
        public SettingBase Setting { get; private set; }

        public static HeaderSetting Header { get; private set; }

        private SettingsCategory category = null;

        public void Create()
        {
            if(Header is null)
            {
                Header = new HeaderSetting(40000, "GlobalSettings");
            }


            Setting= CreateSettings();
        }

        protected abstract SettingBase CreateSettings();



        public SettingsCategory GetCategory()
        {
            if (category == null)
            {
                category = new SettingsCategory(Header, ushort.MaxValue, [Setting]);
            }
            return category;
        }


    }
}
