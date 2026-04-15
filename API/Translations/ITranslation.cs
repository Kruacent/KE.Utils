
namespace KE.Utils.API.Translations
{
    public interface ILocalizable
    {
        string LocalizationId { get; }
        void RegisterTranslations();
    }
}