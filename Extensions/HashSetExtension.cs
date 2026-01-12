using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.Extensions
{
    public static class HashSetExtension
    {

        public static bool TryAdd<T, L>(this HashSet<T> hashset, L value)
        {
            if (value is T)
            {
                return hashset.Add((T)(object)value);
            }
            return false;
        }
    }
}
