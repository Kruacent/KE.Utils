using Exiled.API.Features;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace KE.Utils.API.Translations
{
    public class TranslationHandler
    {
        private static TranslationHandler instance = null;
        public static TranslationHandler Instance
        {
            get
            {
                if(instance is null)
                {
                    instance = new();
                }
                return instance;
            }
        }

        private TranslationHandler() { }


        private Dictionary<string,TranslationFile> TranslationFiles = null;




        private string path = Paths.Configs + "/KETranslations/";

        public bool Loaded
        {
            get
            {
                if (TranslationFiles is null) return false;


                return TranslationFiles.Count != 0;
            }
        }
        public void LoadAll(bool force= false)
        {
            
            if(!force && Loaded)
            {
                return;
            }

            if(TranslationFiles is null)
            {
                TranslationFiles = new();
            }

            TranslationFiles.Clear();
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (string file in files)
            {
                string lang = Path.GetFileNameWithoutExtension(file);
                string json = File.ReadAllText(file);

                TranslationFiles.Add(lang,new(lang, json));
            }

        }



        public string GetTranslation(string key,string lang)
        {
            if(!TranslationFiles.TryGetValue(lang, out var file))
            {
                return "Translation file not found : " + key;
            }


            return file.Get(key);
        }

    }
}
