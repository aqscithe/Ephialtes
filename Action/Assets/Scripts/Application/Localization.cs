using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;

using System.Linq;

public class Localization : MonoBehaviour
{
    public class LanguageConfig
    {
        List<string> language = new List<string>();
        List<string> keyword = new List<string>();

        void initLanguages(StreamReader file)
        {
            string str = file.ReadLine();
            string[] splitStr = str.Split('|');

            for (int i = 0; i < splitStr.Length; ++i)
            {
                language.Add(splitStr[i]);
            }
        }

        void initKeyWords(StreamReader file)
        {
            string str = file.ReadLine();
            string[] splitStr = str.Split('|');

            for (int i = 0; i < splitStr.Length; ++i)
            {
                keyword.Add(splitStr[i]);
            }
        }

        public List<string> getLanguages()
        {
            return language;
        }

        public List<string> getKeyWords()
        {
            return keyword;
        }

        public void initDictionary()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Assets\Localization\config.txt");

            if (!File.Exists(path))
            {
                Debug.Log("could not open config file " + path);
                return;
            }

            StreamReader file = new StreamReader(path);

            initLanguages(file);
            initKeyWords(file);

            file.Close();
            file.Dispose();
        }
    }

    public class Language
    {
        string language;

        Dictionary<string, string> word = new Dictionary<string, string>();

        //loading a language that exists
        public Language(string l)
        {
            language = l;

            string path = Path.Combine(Environment.CurrentDirectory, @"Assets\Localization\" + language + ".txt");

            if (!File.Exists(path))
            {
                Debug.Log("could not open language file " + path);
                return;
            }

            StreamReader file = new StreamReader(path);

            int lineCount = 0;
            while (file.ReadLine() != null)
            {
                ++lineCount;
            }
            file.DiscardBufferedData();
            file.BaseStream.Position = 0;

            addWords(file, lineCount);

            file.Close();
            file.Dispose();
        }

        void addWords(StreamReader file, int lineCount)
        {
            for (int i = 0; i < lineCount; ++i)
            {
                string str = file.ReadLine();
                string[] splitStr = str.Split('|');

                word.Add(splitStr[0], splitStr[1]);
            }
        }

        public int getKeyWordsCount()
        {
            return word.Count;
        }

        public string getName()
        {
            return language;
        }

        public string getKeyWords(int i)
        {
            return word.ElementAt(i).Key;
        }

        public string getContent(string key)
        {
            if (word.ContainsKey(key))
                return word[key];

            //return "no keyword " + key + " found in " + language;
            return "?";
        }
    }







    private LanguageConfig config = new LanguageConfig();
    private Dictionary<int, Language> dictionary = new Dictionary<int, Language>();

    //0 for English
    //1 for French
    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        config.initDictionary();

        dictionary.Add(0, new Language("English"));
        dictionary.Add(1, new Language("French"));
    }

    // Update is called once per frame
    void Update()
    {
        if (current < 0 || current > config.getLanguages().Count - 1)
            current = 0;
    }

    public string getMessage(string key)
    {
        return dictionary[current].getContent(key);
    }

    public void changeLanguage()
    {
        //get the actual gameobject that has not been destroyed
        Localization undestroyed = GameObject.Find("Localization").GetComponent<Localization>();

        undestroyed.current = Mathf.Abs(undestroyed.current - 1);
    }

    public void selectLanguage(int languageIndex)
    {
        //get the actual gameobject that has not been destroyed
        Localization undestroyed = GameObject.Find("Localization").GetComponent<Localization>();

        if (undestroyed.current < 0 || undestroyed.current > undestroyed.config.getLanguages().Count - 1)
            undestroyed.current = languageIndex;
    }
}
