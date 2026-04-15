using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Translations.Events.EventArgs
{
    public class ChangedTranslationEventArgs : IExiledEvent, IPlayerEvent
    {
        public Player Player { get; }
        public string NewLanguage { get; }



        public ChangedTranslationEventArgs(Player player, string newLang)
        {
            Player = player;
            NewLanguage = newLang;
        }


    }
}
