using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WithScreenScaler : MonoBehaviour
{
    
    public GameObject correctAnswerButton;
    public GameObject wrongButton1;
    public GameObject wrongButton2;
    public GameObject wrongButton3;
    public TextMeshProUGUI questionText;
    //public GameObject toMainMenuButton;
    public GameObject[] answerButtons;
    float widthInPixels;
    float heightInPixels;
    public Vector2 buttonPos1;
    public Vector2 buttonPos2;
    public Vector2 buttonPos3;
    public Vector2 buttonPos4;
    public GameObject canvas;
    public RectTransform canvasRect;
    float canvasWidth;
    float canvasHeight;
    [SerializeField] public TextMeshProUGUI[] textElements;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;
        canvasHeight = canvasRect.rect.height;
        withScreenScalerForRealFunc();
        ChangeAllFontSizes();
    }
    public void withScreenScalerForRealFunc() {
        widthInPixels = canvasWidth * correctAnswerButton.GetComponentInParent<Canvas>().scaleFactor;
        heightInPixels = canvasHeight * correctAnswerButton.GetComponentInParent<Canvas>().scaleFactor;
        answerButtons = new GameObject[] { correctAnswerButton, wrongButton1, wrongButton2, wrongButton3 };
        for (int i = 0; i < 4; i++)
        {
            //change button sizes 
            answerButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(widthInPixels * 0.3125f, heightInPixels * (25f / 108f));

            // change button positions
            if (i == 0)
            {
                buttonPos1 = new Vector2((widthInPixels * 0.3125f) * (-1 * (13f / 24f)), (heightInPixels / 2f) - (heightInPixels * (540f / 1080f)));
            }
            else if (i == 1)
            {
                buttonPos2 = new Vector2((widthInPixels * 0.3125f) * (-1 * (13f / 24f)), (heightInPixels / 2f) - (heightInPixels * (840f / 1080f)));
            }
            else if (i == 2)
            {
                buttonPos3 = new Vector2((widthInPixels * 0.3125f) * (1 * (13f / 24f)), (heightInPixels / 2f) - (heightInPixels * (540f / 1080f)));
            }
            else if (i == 3)
            {
                buttonPos4 = new Vector2((widthInPixels * 0.3125f) * (1 * (13f / 24f)), (heightInPixels / 2f) - (heightInPixels * (840f / 1080f)));
            }
            
        }
        //toMainMenuButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthInPixels * (200f / 1920f), heightInPixels * (130f / 1080f));
                                                                              // width/canvaswidth -  posx/width -                                              posy/canvasheight
        //toMainMenuButton.transform.localPosition = new Vector2((widthInPixels * (200f/1920f)) * (1 * (800f / 200f)), (heightInPixels / 2f) - (heightInPixels * (140f / 1080f)));
        // change text size
        questionText.GetComponent<RectTransform>().sizeDelta = new Vector2(widthInPixels * (65f / 96f), questionText.GetComponent<RectTransform>().sizeDelta.y);

        // change text position
        questionText.transform.localPosition = new Vector2(0, (heightInPixels / 2f) - (heightInPixels * (230f / 1080f)));
    }
    public void ChangeAllFontSizes()
    {
        foreach (TextMeshProUGUI textItem in textElements)
        {
            // Null check prevents errors if an array slot was left empty in the inspector
            if (textItem != null)
            {
                textItem.fontSize = (canvasWidth * (70f/1920f));
            }
        }
        //toMainMenuButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = (canvasWidth * (45f / 1920f));
    }

    void Update()
    {
        
    }
    
}
