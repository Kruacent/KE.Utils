using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Translations
{
    public abstract class TranslationFile
    {
        public abstract string Lang { get; }

        //translation key per file
        public abstract string Key { get; }

        public abstract List<TranslationKey> Values { get; }



        public override string ToString()
        {
            string result = $"({Lang}) {Key}\n";

            foreach(TranslationKey key in Values)
            {
                result += key + "\n";
            }
            return result;
        }

    }
}
