using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using KE.Utils.API.Settings.GlobalSettings;
using System.Collections.Generic;
using System.Data;

namespace KE.Utils.API.Translations
{
    public static class TranslationHub
    {
        private static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> _vault = new();

        public static void Add(string id, string lang, string key, string text)
        {
            Log.Info("id : " + id + " lang : " + lang + " key : " + key + " text : " + text);
            if (!_vault.ContainsKey(id)) _vault[id] = new();
            if (!_vault[id].ContainsKey(lang)) _vault[id][lang] = new();
            _vault[id][lang][key] = text;
        }


        public static void Add(string id, Dictionary<string, Dictionary<string, string>> langToKeyToText)
        {
            foreach (var kvp in langToKeyToText)
            {
                foreach (var kvp2 in kvp.Value)
                {
                    Add(id, kvp.Key, kvp2.Key, kvp2.Value);
                }
            }

        }



        public static string Get(Player player, string id, string key)
        {

            string lang = GetLang(player);



            Log.Info("language : " + lang);
            Log.Info("player : " + player.Nickname + " class name : " + id + " translation key : " + key);
            if (!_vault.ContainsKey(id)) return key;

            var langDict = _vault[id].ContainsKey(lang) ? _vault[id][lang] : _vault[id].ContainsKey(DefaultLang) ? _vault[id][DefaultLang] : null;

            return langDict != null && langDict.TryGetValue(key, out var val) ? val : key;
        }

        public static string GetLang(Player player)
        {

            string lang = DefaultLang;
            if (GlobalSettingsHandler.Instance.TryGet<TranslationSetting>(out var globalsetting)
                && SettingBase.TryGetSetting<DropdownSetting>(player, globalsetting.Setting.Id, out var setting))
            {
                lang = setting.SelectedOption;
            }
            return lang;
        }


        public static readonly string DefaultLang = "en";
    }
}