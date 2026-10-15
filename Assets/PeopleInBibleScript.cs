using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeopleInBibleScript : MonoBehaviour
{
    private Dictionary<string, string> questionsAndAnswerDict = new Dictionary<string, string>
    {
        { "Who was the first man created?", "Adam" },
        { "Who was the first woman created?", "Eve" }
    };
    public string[] questions;
    public string[] answers;

    private string answerText;

    private int questionIndex;
    private string questionForUseText;
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
    public WithScreenScaler WithScreenScalerObject;

    void pickQuestion()
    {
        // question
        questionIndex = Random.Range(0, questions.Length);
        questionForUseText = questions[questionIndex];

        // answer
        answerText = answers[questionIndex];

        // wrong options

    }
    public void updateText()
    {
        // question from dict of questions and asnwer that goes with question
        questionText.text = questionForUseText;
        correctAnswerButton.GetComponentInChildren<TextMeshProUGUI>().text = answerText;
        wrongButton1.GetComponentInChildren<TextMeshProUGUI>().text = "B";
        wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "B";
        wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "B";
    }
    private Dictionary<int, Vector2> buttonPositions = new Dictionary<int, Vector2>
    {
        { 1, new Vector2(0, 0) },
        { 2, new Vector2(0, 0) },
        { 3, new Vector2(0, 0) },
        { 4, new Vector2(0, 0) }
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
        for (int i = 1; i <= 4; i++)
        {
            buttonPositionsOccupied[i] = false;
        }
        for (int i = 0; i < answerButtons.Length; i++)
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
            button.transform.localPosition = buttonPositions[randomPosition];
            Debug.Log(answerButtons[i] + " moved to position " + buttonPositions[randomPosition]);
            buttonPositionsOccupied[randomPosition] = true;
        }
    }
    public void correctAnswerClicked()
    {
        Debug.Log("Correct answer clicked!");
        clearQuestion();
        pickQuestion();
        updateText();
        shuffleButtonPositions();
    }
    public void Start()
    {
        Invoke("setupGame", 0.2f);
    }
    public void setupGame()
    {
        questions = questionsAndAnswerDict.Keys.ToArray();
        answers = questionsAndAnswerDict.Values.ToArray();
        buttonPositions[1] = WithScreenScalerObject.buttonPos1;
        buttonPositions[2] = WithScreenScalerObject.buttonPos2;
        buttonPositions[3] = WithScreenScalerObject.buttonPos3;
        buttonPositions[4] = WithScreenScalerObject.buttonPos4;
        answerButtons = new GameObject[] { correctAnswerButton, wrongButton1, wrongButton2, wrongButton3 };
        clearQuestion();
        pickQuestion();
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
        if (buttonIndex == 1)
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
