public interface ILocalizable
{
    string LocalizationId { get; }
    void RegisterTranslations();
}