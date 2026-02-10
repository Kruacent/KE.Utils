using Exiled.API.Features;
using System.Collections.Generic;

public static class TranslationHub
{
    private static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> _vault = new();

    public static void Add(string id, string lang, string key, string text)
    {
        Log.Info("id : " + id + " lang : " + lang + " key : " + key + " text : " + text);
        if (!_vault.ContainsKey(id)) _vault[id] = new();
        if (!_vault[id].ContainsKey(lang)) _vault[id][lang] = new();
        _vault[id][lang][key] = text;
    }

    public static string Get(Player p, string id, string key)
    {

        string lang = "fr";
        Log.Info("language : " + lang);
        Log.Info("player : " + p + " class name : " + id + " translation key : " + key);
        if (!_vault.ContainsKey(id)) return key;

        var langDict = _vault[id].ContainsKey(lang) ? _vault[id][lang] : (_vault[id].ContainsKey("en") ? _vault[id]["en"] : null);

        return langDict != null && langDict.TryGetValue(key, out var val) ? val : key;
    }
}