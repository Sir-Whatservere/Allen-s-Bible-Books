using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibleManager : MonoBehaviour
{
    [System.Serializable]
    public class VerseData
    {
        public string verse;
        public string text;
    }

    [System.Serializable]
    public class ChapterData
    {
        public string chapter;
        public List<VerseData> verses;
    }

    [System.Serializable]
    public class BookData
    {
        public string book;
        public List<ChapterData> chapters;
    }

    private Dictionary<string, string> verseLookup = new Dictionary<string, string>();

    void Awake()
    {
        LoadEntireBible();
    }

    private void LoadEntireBible()
    {
        TextAsset[] bookFiles = Resources.LoadAll<TextAsset>("Bible");

        foreach (TextAsset file in bookFiles)
        {
            BookData bookData = JsonUtility.FromJson<BookData>(file.text);

            foreach (ChapterData chapter in bookData.chapters)
            {
                foreach (VerseData verse in chapter.verses)
                {
                    string reference = $"{bookData.book} {chapter.chapter}:{verse.verse}";
                    verseLookup[reference] = verse.text;
                }
            }
        }

        Debug.Log($"Loaded {verseLookup.Count} verses successfully.");
    }

    public string GetVerse(string book, int chapter, int verse)
    {
        string key = $"{book} {chapter}:{verse}";
        if (verseLookup.TryGetValue(key, out string text))
        {
            return text;
        }
        return "Verse not found.";
    }
}

