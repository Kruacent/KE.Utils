using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace KE.Utils.API.Settings.EventArgs
{
    public class KeybindPressedEventArgs
    {
        public KeybindPressedEventArgs(SSKeybindSetting settings, bool isPressed, ReferenceHub referenceHub)
        {
            Settings = settings;
            IsPressed = isPressed;
            ReferenceHub = referenceHub;
        }

        public SSKeybindSetting Settings { get; }
        public bool IsPressed { get; }
        public ReferenceHub ReferenceHub { get; }


        

    }
}
