using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAccessButton : MonoBehaviour
{
    public Text buttonText;
    int buttonValue;
    Color colorDefault;
    Color colorTimerCompleted = new Color32(232, 255, 0 , 152); //timer complete signal color

    private void Start()
    {
        colorDefault = this.GetComponent<Image>().color;
    }

    public void UpdateButtonName(int val)
    {
        buttonValue = val;
        SetTimerNumberText();
    }

    void SetTimerNumberText()
    {
        buttonText.text = "Timer #" + (buttonValue+1).ToString("00");
    }

    public void SetButtonViewTimerComplete()
    {
        this.GetComponent<Image>().color = colorTimerCompleted;
    }

    public void SetButtonViewTimerDefault()
    {
        this.GetComponent<Image>().color = colorDefault;
    }
}
