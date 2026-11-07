using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public string[] BibleBooks = new string[]
    {
        "Genesis",
        "Exodus",
        "Leviticus",
        "Numbers",
        "Deuteronomy",
        "Joshua",
        "Judges",
        "Ruth",
        "1 Samuel",
        "2 Samuel",
        "1 Kings",
        "2 Kings",
        "1 Chronicles",
        "2 Chronicles",
        "Ezra",
        "Nehemiah",
        "Esther",
        "Job",
        "Psalms",
        "Proverbs",
        "Ecclesiastes",
        "Song of Solomon",
        "Isaiah",
        "Jeremiah",
        "Lamentations",
        "Ezekiel",
        "Daniel",
        "Hosea",
        "Joel",
        "Amos",
        "Obadiah",
        "Jonah",
        "Micah",
        "Nahum",
        "Habakkuk",
        "Zephaniah",
        "Haggai",
        "Zechariah",
        "Malachi",
        "Matthew",
        "Mark",
        "Luke",
        "John",
        "Acts",
        "Romans",
        "1 Corinthians",
        "2 Corinthians",
        "Galatians",
        "Ephesians",
        "Philippians",
        "Colossians",
        "1 Thessalonians",
        "2 Thessalonians",
        "1 Timothy",
        "2 Timothy",
        "Titus",
        "Philemon",
        "Hebrews",
        "James",
        "1 Peter",
        "2 Peter",
        "1 John",
        "2 John",
        "3 John",
        "Jude",
        "Revelation",
        "Nothing"
    };
    // [66] is nothing
    private int answerBookIndex;
    private string answerBook;
    private int bookBeforeAnswerIndex;
    private string bookBeforeAnswer;
    private int bookAfterAnswerIndex;
    private string bookAfterAnswer;
    private int wrongBook1Index;
    private int wrongBook2Index;
    private int wrongBook3Index;

    // button and text references
    public GameObject correctAnswerButton;
    public GameObject wrongButton1;
    public GameObject wrongButton2;
    public GameObject wrongButton3;
    public TextMeshProUGUI questionText;

    public GameObject[] answerButtons;
    public GameObject SetRangeObject;
    public WithScreenScaler WithScreenScalerObject;
    public bool landscapeMode = true;

    void pickBooksForQuestion()
    {
        // answer
        answerBookIndex = Random.Range(SetRangeObject.GetComponent<SetRangeScript>().rangeStartBookIndex, SetRangeObject.GetComponent<SetRangeScript>().rangeEndBookIndex);
        answerBook = BibleBooks[answerBookIndex];

        // before and after for question
        // if answer is first book, before is last book
        if (answerBookIndex == 0)
        {
            bookBeforeAnswerIndex = BibleBooks.Length - 1;
        }
        else 
        { 
            bookBeforeAnswerIndex = answerBookIndex - 1;
        }
        bookBeforeAnswer = BibleBooks[bookBeforeAnswerIndex];
        if (answerBookIndex == 65)
        {
            bookAfterAnswerIndex = 66;
        }
        else
        {
            bookAfterAnswerIndex = answerBookIndex + 1;
        }
        bookAfterAnswer = BibleBooks[bookAfterAnswerIndex];

        // wrong options
        wrongBook1Index = Random.Range(0, BibleBooks.Length);
        while (wrongBook1Index == answerBookIndex || wrongBook1Index == bookBeforeAnswerIndex || wrongBook1Index == bookAfterAnswerIndex)
        {
            wrongBook1Index = Random.Range(0, BibleBooks.Length);
        }
        wrongBook2Index = Random.Range(0, BibleBooks.Length);
        while (wrongBook2Index == answerBookIndex || wrongBook2Index == bookBeforeAnswerIndex || wrongBook2Index == bookAfterAnswerIndex || wrongBook2Index == wrongBook1Index)
        {
            wrongBook2Index = Random.Range(0, BibleBooks.Length);
        }
        wrongBook3Index = Random.Range(0, BibleBooks.Length);
        while (wrongBook3Index == answerBookIndex || wrongBook3Index == bookBeforeAnswerIndex || wrongBook3Index == bookAfterAnswerIndex || wrongBook3Index == wrongBook1Index || wrongBook3Index == wrongBook2Index)
        {
            wrongBook3Index = Random.Range(0, BibleBooks.Length);
        }
    }
    public void updateText()
    {
        questionText.text = "What book comes after " + bookBeforeAnswer + " and before " + bookAfterAnswer + "?";
        correctAnswerButton.GetComponentInChildren<TextMeshProUGUI>().text = answerBook;
        wrongButton1.GetComponentInChildren<TextMeshProUGUI>().text = BibleBooks[wrongBook1Index];
        wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = BibleBooks[wrongBook2Index];
        wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = BibleBooks[wrongBook3Index];
    }
    private Dictionary<int, Vector2> buttonPositions = new Dictionary<int, Vector2> 
    {
        { 1, new Vector2(-325, -540) },
        { 2, new Vector2(-325, -840) },
        { 3, new Vector2(325, -540) },
        { 4, new Vector2(325, -840) }
    };
    private Dictionary<int, bool> buttonPositionsOccupied = new Dictionary<int, bool>
    {
        { 1, false },
        { 2, false },
        { 3, false },
        { 4, false }
    };
    
    public void shuffleButtonPositions()
    {
        for(int i = 1; i <= 4; i++)
        {
            buttonPositionsOccupied[i] = false;
        }
        for (int i = 0; i< answerButtons.Length; i++)
        {
            // get first button
            GameObject button = answerButtons[i];
            // select random position
            int randomPosition = Random.Range(1, 5);
            // check if position is occupied
            while (buttonPositionsOccupied[randomPosition])
            {
                // if occupied, select new position
                randomPosition = Random.Range(1, 5);
            }
            // if not occupied, move button to position and mark position as occupied
            button.transform.GetComponent<RectTransform>().anchoredPosition = buttonPositions[randomPosition];
            Debug.Log(answerButtons[i] + " moved to position " + buttonPositions[randomPosition]);
            buttonPositionsOccupied[randomPosition] = true;
        }
    }
    public void correctAnswerClicked()
    {
        Debug.Log("Correct answer clicked!");
        clearQuestion();
        pickBooksForQuestion();
        updateText();
        shuffleButtonPositions();
    }
    public void Start()
    {
        Invoke("setupGame", 0.2f);
    }
    public void setupGame()
    {
        if (!landscapeMode)
        {
            buttonPositions[1] = new Vector2(-220, -900);
            buttonPositions[2] = new Vector2(-220, -1500);
            buttonPositions[3] = new Vector2(220, -900);
            buttonPositions[4] = new Vector2(220, -1500);
        }
        answerButtons = new GameObject[] { correctAnswerButton, wrongButton1, wrongButton2, wrongButton3 };
        clearQuestion();
        pickBooksForQuestion();
        updateText();
        shuffleButtonPositions();
    }
    public void clearQuestion()
    {
        for (int i = 1; i < 4; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = true;
        }
    }
    public void wrongAnswerClicked(int buttonIndex)
    {
        if(buttonIndex == 1)
        {
            wrongButton1.GetComponent<Button>().interactable = false;
        }
        if (buttonIndex == 2)
        {
            wrongButton2.GetComponent<Button>().interactable = false;
        }
        if (buttonIndex == 3)
        {
            wrongButton3.GetComponent<Button>().interactable = false;
        }
    }
}
