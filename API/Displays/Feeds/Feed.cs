using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Displays.Feeds
{
    public sealed class Feed
    {

        public string RawHint { get; }

        public DateTime TimeCreated { get; }

        public Feed(string msg)
        {
            RawHint = msg;
            TimeCreated = DateTime.Now;
        }
    }
}
