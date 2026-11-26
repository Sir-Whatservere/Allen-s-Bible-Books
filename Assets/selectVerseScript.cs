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
    public Transform containerTransform;

    public void createChaptersSelector(string bookName) {
        // load book file
        TextAsset jsonFile = Resources.Load<TextAsset>($"Bible/{bookName}");
        // get book data
        BibleManager.BookData bookData = JsonUtility.FromJson<BibleManager.BookData>(jsonFile.text);
        for (int i = 1; i < (bookData.chapters.Count + 1); i++)
        {
            // instantiate chapter button
            CreateButton(bookName, i);
        }
    }
    public void CreateButton(string bookName,int chapterNumber)
    {

        Button newButton = Instantiate(chapterButtonPrefab, containerTransform);
        GameObject newObject = newButton.gameObject;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = chapterNumber.ToString();

        newButton.onClick.AddListener(() => OnButtonClick(chapterNumber, newObject, bookName));
    }
    public void OnButtonClick(int chapterNumber, GameObject newObject, string bookName)
    {
        Debug.Log(bookName + " Chapter " + chapterNumber + " button clicked!");
        // do whole process for verses
    }
}
