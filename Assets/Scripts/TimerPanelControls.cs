using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanelControls : MonoBehaviour
{
    public Text timerText;
    Animator animatorTimerPanel;
    TimerMenuControls timerSelectionMenuControls;

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
    }

    public void OnStartButtonPress()
    {
        //сначал запустить таймер, а затем...
        timerSelectionMenuControls.ShowAllTimerButtons();
        HidePanel();
    }

    void Update()
    {
        
    }
}
