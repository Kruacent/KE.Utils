using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace KE.Utils.API.Settings.EventArgs
{
    public class ValueReceivedEventArgs
    {
        public ValueReceivedEventArgs(ServerSpecificSettingBase settings, ReferenceHub referenceHub)
        {
            Settings = settings;
            ReferenceHub = referenceHub;
        }

        public ServerSpecificSettingBase Settings { get; }
        public ReferenceHub ReferenceHub { get; }


        

    }
}
