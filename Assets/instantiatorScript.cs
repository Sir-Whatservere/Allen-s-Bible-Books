using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class instantiatorScript : MonoBehaviour
{
    public Button buttonPrefab;
    public Transform containerTransform;
    public GameObject learnVersesManager;
    public TMP_Text mainVerseAndBlanksText;

    public int[] sortedIndexOrders;
    public string[] modifiableMainVerse;
    public class verseAndBank
    {
        public String[] verseAndBlanks;
        public String[] wordBank;
        public int[] indexOrders;
        public verseAndBank(String[] verseAndBlanks, String[] wordBank, int[] indexOrders)
        {
            this.verseAndBlanks = verseAndBlanks;
            // eg ["a", "b", "_", "a", "_", "c"]
            this.wordBank = wordBank;
            // eg ["b", "c"]
            this.indexOrders = indexOrders;
            // eg [4, 2]
            // gives spot for verseAndBlanks
            // might want to use sortedIndexOrders instead for most cases
        }
    }
    public void CreateButton(int wordBankIndex, int savedIndex)
    {
        
        Button newButton = Instantiate(buttonPrefab, containerTransform);
        GameObject newObject = newButton.gameObject;
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex];

        newButton.onClick.AddListener(() => OnButtonClick(wordBankIndex, savedIndex, newObject));
    }

    public void setupWordBankButtons()
    {
        // for each in wordBank
        for (int e = 0; e < learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank.Length; e++)
        {
            CreateButton(e, learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.indexOrders[e]);
        }
        sortedIndexOrders = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.indexOrders;
        Array.Sort(sortedIndexOrders);
    }
    public void setupMainVerseText()
    {
        modifiableMainVerse = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.verseAndBlanks;
        string textToUse = $"[{string.Join(", ", learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.verseAndBlanks)}]";
        mainVerseAndBlanksText.text = textToUse;
    }

    public void setUpLearnVerses()
    {
        // find a way to assign these values from other input
        int numberOfBlanks = 3;
        string book = "Psalms";
        int chapter = 37;
        int verseNum = 7;
        learnVersesManager.GetComponent<LearnVersesManagerScript>().runVersesLearner(numberOfBlanks, book, chapter, verseNum);
        setupWordBankButtons();
        setupMainVerseText();
    }
    // update sentence and word bank

    // remember     public int[] sortedIndexOrders; and string[] modifiableMainVerse
    public int nextIndexForSortedIndexArray = 0;
    //public int nextIndexToForBlank = 0;
    public void OnButtonClick(int wordBankIndex, int savedIndex, GameObject buttonClicked)
    {
        //Debug.Log("Word in spot -" + savedIndex + "- clicked!");
        if(savedIndex == sortedIndexOrders[nextIndexForSortedIndexArray])
        {
            Debug.Log("Correct");
            modifiableMainVerse[savedIndex] = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex];
            mainVerseAndBlanksText.text = $"[{string.Join(", ", modifiableMainVerse)}]";
            nextIndexForSortedIndexArray++;
            Destroy(buttonClicked);
            //
        }
        // else if saved
        else if (learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex] == learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[sortedIndexOrders[nextIndexForSortedIndexArray]])
        {
            Debug.Log("Correct - same word, different position");
            //Debug.Log(learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex] + " " + savedIndex);
            //Debug.Log(learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[sortedIndexOrders[nextIndexForSortedIndexArray]] + " " + sortedIndexOrders[nextIndexForSortedIndexArray]);
            Destroy(buttonClicked);
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
