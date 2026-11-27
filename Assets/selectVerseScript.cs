using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BibleManager;

public class selectVerseScript : MonoBehaviour
{
    public BibleManager bibleManager;
    public Button chapterButtonPrefab;
    public Button versesButtonPrefab;
    public Transform chaptersContainerTransform;
    public Transform versesContainerTransform;
    // use this
    public string selectedBook;
    public BibleManager.BookData selectedBookData;
    // use this
    public int selectedChapter;
    public BibleManager.ChapterData selectedChapterData;
    // use this
    public int selectedVerse;
    public void createChaptersSelector(string bookName) {
        selectedBook = bookName;
        // load book file
        TextAsset jsonFile = Resources.Load<TextAsset>($"Bible/{bookName}");
        // get book data
        BibleManager.BookData bookData = JsonUtility.FromJson<BibleManager.BookData>(jsonFile.text);
        selectedBookData = bookData;
        for (int i = 1; i < (bookData.chapters.Count + 1); i++)
        {
            // instantiate chapter button
            // i is what chapter
            CreateButtonForChapters(bookData, bookName, i);
        }
    }
    public void CreateButtonForChapters(BibleManager.BookData bookData, string bookName, int chapterNumber)
    {

        Button newButton = Instantiate(chapterButtonPrefab, chaptersContainerTransform);
        GameObject newObject = newButton.gameObject;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = chapterNumber.ToString();

        newButton.onClick.AddListener(() => OnButtonClickForChapters(chapterNumber, newObject, bookName));
    }
    // is newObject needed in onclick?
    public void OnButtonClickForChapters(int chapterNumber, GameObject newObject, string bookName)
    {
        selectedChapter = chapterNumber;
        Debug.Log(bookName + " Chapter " + chapterNumber + " button clicked!");
        // do whole process for verses
        createVerseSelector(chapterNumber);
    }
    public void createVerseSelector(int chapterNumber)
    {

        // get chapter data
        BibleManager.ChapterData chapterData = selectedBookData.chapters[chapterNumber - 1];
        selectedChapterData = chapterData;
        for (int i = 1; i < (chapterData.verses.Count + 1); i++)
        {
            // instantiate verse button
            CreateButtonForVerses(i);
        }
    }
    // is newObject needed in onclick?
    public void CreateButtonForVerses(int verseNumber)
    {
        //  use selectedBookData, selectedBook, selectedChapterData, selectedChapter without passing them in
        Button newButton = Instantiate(versesButtonPrefab, versesContainerTransform);
        GameObject newObject = newButton.gameObject;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = verseNumber.ToString();

        newButton.onClick.AddListener(() => OnButtonClickForVerses(verseNumber, newObject));
    }
    public void OnButtonClickForVerses(int verseNumber, GameObject newObject)
    {
        selectedVerse = verseNumber;
        Debug.Log("Verse " + verseNumber + " button clicked!");
    }

}
