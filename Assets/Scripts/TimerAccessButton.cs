using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAccessButton : MonoBehaviour
{
    public Text buttonText;
    int buttonValue;

    public void SetButtonValue(int val)
    {
        buttonValue = val;
        SetTimerNumberText();
    }

    void SetTimerNumberText()
    {
        buttonText.text = "Timer #" + (buttonValue+1).ToString("00");
    }

    public int GetButtonValue()
    {
        return buttonValue;
    }
}
