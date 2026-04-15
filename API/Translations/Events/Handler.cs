using KE.Utils.API.Translations.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Translations.Events
{
    public static class Handler
    {

        public static event Action<ChangedTranslationEventArgs> TranslationChanged = delegate { };



        public static void OnTranslationChanged(ChangedTranslationEventArgs ev)
        {
            TranslationChanged?.Invoke(ev);
        }

    }
}
