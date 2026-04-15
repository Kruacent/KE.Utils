using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Exceptions
{
    public class ClipsNotLoadedException : Exception
    {

        private static string msg = "clips not loaded ; use SoundPlayer.Load()";
        public ClipsNotLoadedException() : base(msg)
        {
            
        }
    }

}
