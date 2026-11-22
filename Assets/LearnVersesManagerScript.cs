using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static LearnVersesManagerScript;

public class LearnVersesManagerScript : MonoBehaviour
{
    public verseAndBank publicUseVerseAndBank = new verseAndBank(null, null, null);
    public static verseAndBank SetupVerse(int numberOfBlanks, int verseIndex, String[] verses, verseAndBank verseAndBank_toReturn)
    {
        // split string into string[] of words
        String[] splitVerse = verses[verseIndex].Split(" ");
        if (numberOfBlanks > splitVerse.Length)
        {
            Debug.Log("Number of blanks exceeds verse length.");
        }
        // pick a number of words/indexes to be blanks to learn the verse
        int wordsToTake = numberOfBlanks;
        bool[] wordTaken = new bool[splitVerse.Length];
        // set all false
        for (int e = 0; e < wordTaken.Length; e++)
        {
            wordTaken[e] = false;
        }

        // going to become blanks later | length = amount of wordstotake
        int[] savedIndexes = new int[wordsToTake];

        // find the indexes for the blanks
        for (int i = 0; i < wordsToTake; i++)
        {
            // find a random index/word
            int indexToTake = indexToTake = (int)(UnityEngine.Random.Range(0.0f, 1.0f) * (splitVerse.Length - 1) + 1);
            // find a random index/word that isnt already saved
            while (wordTaken[indexToTake] == true)
            {
                indexToTake = (int)(UnityEngine.Random.Range(0.0f, 1.0f) * (splitVerse.Length - 1) + 1);
            }
            // set word taken to true
            wordTaken[indexToTake] = true;
            // save the index
            savedIndexes[i] = indexToTake;
        }
        // word bank
        String[] wordBankArray = new String[wordsToTake];
        // cycle through each saved index and save that word
        for (int o = 0; o < savedIndexes.Length; o++)
        {
            wordBankArray[o] = splitVerse[savedIndexes[o]];
        }
        // create a new string of the verse with ___ replacing the picked indexes
        // set equal to split verse and add blanks later
        String[] verseWithBlanks = splitVerse;

        // set saved indexes to blank
        for (int a = 0; a < savedIndexes.Length; a++)
        {
            verseWithBlanks[savedIndexes[a]] = "___";
        }
        // String[] wordBankArray
        // String[] verseWithBlanks
        //System.out.println(Arrays.toString(verseWithBlanks));
        //System.out.println(Arrays.toString(wordBankArray));

        verseAndBank_toReturn.verseAndBlanks = verseWithBlanks;
        verseAndBank_toReturn.wordBank = wordBankArray;
        // wordBankArray[i] goes in verseWithBlanks[savedIndexes[i]]
        verseAndBank_toReturn.indexOrders = savedIndexes;
        return verseAndBank_toReturn;

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
        }
    }
    public void runVersesLearner(int numberOfBlanks)
    {
        String[] verses = {"Thy word is a lamp unto my feet and a light unto my path", "Rest in the LORD, and wait patiently for him: fret not thyself because of him who prospereth in his way, because of the man who bringeth wicked devices to pass." };
        // number of blanks, index for verse from array of all verses..., reference to array with all verses, preconcieved verseAndBank
        SetupVerse(numberOfBlanks, 0, verses, publicUseVerseAndBank);
        Debug.Log($"[{string.Join(", ", publicUseVerseAndBank.verseAndBlanks)}]");
        Debug.Log($"[{string.Join(", ", publicUseVerseAndBank.wordBank)}]");
        Debug.Log($"[{string.Join(", ", publicUseVerseAndBank.indexOrders)}]");
    }
}
