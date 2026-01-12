using InventorySystem.Items.Firearms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Translations
{
    public class TranslationKey
    {


        //key per string
        public readonly string Key;


        //default translation
        public readonly string Value;


        public TranslationKey(string key,string value)
        {

            Key = key;
            Value = value;
        }


        public override string ToString()
        {
            return $"({Key}) : {Value}";
        }
    }
}
