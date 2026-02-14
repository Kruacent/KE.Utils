using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using KE.Utils.API.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Settings.GlobalSettings
{
    public class TranslationSetting : GlobalSetting
    {
        protected override SettingBase CreateSettings()
        {

            return new DropdownSetting(40001, "Translation", [TranslationHub.DefaultLang, "fr"],header:Header,onChanged:OnChanged);

        }


        public void OnChanged(Player player,SettingBase setting)
        {
            OnTranslationChange?.Invoke(player);
        }


        public static event Action<Player> OnTranslationChange = delegate { };
    }
}
