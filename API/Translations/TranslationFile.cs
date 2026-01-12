using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserSettings.UserInterfaceSettings;

namespace KE.Utils.API.Translations
{
    public struct TranslationFile
    {

        private string Language;

        private Dictionary<string, string> values;


        public TranslationFile(string language,string rawjson)
        {
            Language = language;

            values = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawjson);
            
        }

        public TranslationFile()
        {
            Language = string.Empty;
            values = null;
        }

        public string Get(string key)
        {
            if(values is null)
            {
                return "null";
            }

            if (!values.ContainsKey(key))
            {
                return "key not found : " + key;
            }

            return values[key];
        }





    }
}
