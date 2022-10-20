using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAccessButton : MonoBehaviour
{
    public Text buttonText;
    public void SetTimerNumberText(int button_number)
    {
        buttonText.text = "Timer #" + button_number.ToString("00");
    }
}
