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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class verseAndBank
    {
        public String[] verseAndBlanks;
        public String[] wordBank;
        public int[] indexOrders;
        public verseAndBank(String[] verseAndBlanks, String[] wordBank, int[] indexOrders)
        {
            this.verseAndBlanks = verseAndBlanks;
            this.wordBank = wordBank;
            this.indexOrders = indexOrders;
            // eg [0, 2, 1]
        }
    }
    public void CreateButton(int wordBankIndex, int savedIndex)
    {
        
        Button newButton = Instantiate(buttonPrefab, containerTransform);
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex];

        newButton.onClick.AddListener(() => OnButtonClick(wordBankIndex, savedIndex));
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

    public void setUpLearnVerses(int numberOfBlanks)
    {
        learnVersesManager.GetComponent<LearnVersesManagerScript>().runVersesLearner(numberOfBlanks);
        setupWordBankButtons();
        setupMainVerseText();
    }
    // update sentence and word bank

    // remember     public int[] sortedIndexOrders; and string[] modifiableMainVerse
    public int nextIndexForSortedIndexArray = 0;
    //public int nextIndexToForBlank = 0;
    public void OnButtonClick(int wordBankIndex, int savedIndex)
    {
        //Debug.Log("Word in spot -" + savedIndex + "- clicked!");
        if(savedIndex == sortedIndexOrders[nextIndexForSortedIndexArray])
        {
            Debug.Log("Correct");
            modifiableMainVerse[savedIndex] = learnVersesManager.GetComponent<LearnVersesManagerScript>().publicUseVerseAndBank.wordBank[wordBankIndex];
            mainVerseAndBlanksText.text = $"[{string.Join(", ", modifiableMainVerse)}]";
            nextIndexForSortedIndexArray++;
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
