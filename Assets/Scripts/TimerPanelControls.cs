using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanelControls : MonoBehaviour
{
    public Text timerText;
    Animator animatorTimerPanel;
    TimerMenuControls timerSelectionMenuControls;

    void ShowTimerValue()
    {
        double t_val = GameData.instance.GetCurrentTimerValue();
        TimeSpan ts = TimeSpan.FromSeconds(t_val);
        timerText.text = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") + ":" + ts.Milliseconds.ToString("000");
    }

    public void SetTimerSelectionPanelLink(TimerMenuControls menu_controls)
    {
        timerSelectionMenuControls = menu_controls;
    }

    void ShowPanel()
    {
        animatorTimerPanel.SetTrigger("ShowPanel");
    }

    void HidePanel()
    {
        animatorTimerPanel.SetTrigger("HidePanel");
    }

    void Start()
    {
        animatorTimerPanel = gameObject.GetComponent<Animator>();
        ShowPanel();
        ShowTimerValue();
    }

    void Update()
    {
        ShowTimerValue();
    }

    //when "Timer Start" button is pressed
    public void OnStartButtonPress()
    {
        GameData.instance.StartCurrentTimer();
        timerSelectionMenuControls.SetDefaultColorToCurrentTimerButton();
        timerSelectionMenuControls.ShowAllTimerButtons();
        HidePanel();
    }
}
