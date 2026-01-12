using InventorySystem.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE.Utils.API.Translations
{
    public interface ITranslation
    {

        TranslationFile Translation { get; }
    }
}
