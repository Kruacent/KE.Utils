using Discord;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Subtitles.SubtitleCategory;

namespace KE.Utils.API.Features
{
    public static class KELog
    {
        public static void Register(string message)
        {
            Log.SendRaw("[REGISTER] [" + Assembly.GetCallingAssembly().GetName().Name + "]" + message, ConsoleColor.DarkBlue);
        }

        public static void Debug(string message)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            if (Log.DebugEnabled.Contains(callingAssembly))
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();

                Log.Send($"[{methodBase.ReflectedType}.{methodBase.Name}] " + message, LogLevel.Debug, ConsoleColor.Green);
            }
        }

        public static void Debug(object message)
        {
            Debug(message.ToString());
        }
    }
}
