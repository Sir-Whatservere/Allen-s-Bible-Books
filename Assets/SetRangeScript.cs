using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetRangeScript : MonoBehaviour
{
    [SerializeField] TMP_Dropdown setStartRangeDropdown;
    [SerializeField] TMP_Dropdown setEndRangeDropdown;

    public GameManagerScript GameManagerScript;
    public int rangeStartBookIndex = 0;
    public int rangeEndBookIndex = 66;
    public void Start()
    {
        setStartRangeDropdown.onValueChanged.AddListener(dropdownSetStartOfRange);
        setEndRangeDropdown.onValueChanged.AddListener(dropdownSetEndOfRange);
    }
    public void dropdownSetStartOfRange(int index)
    {
        rangeStartBookIndex = index;
        
        GameManagerScript.correctAnswerClicked();
    }
    public void dropdownSetEndOfRange(int index)
    {
        // this num not included so add one so the original index is included
        rangeEndBookIndex = index + 1;
        
        GameManagerScript.correctAnswerClicked();
    }
}
