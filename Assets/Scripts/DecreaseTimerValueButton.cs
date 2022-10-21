using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseTimerValueButton : MonoBehaviour
{
    bool isHoldStarted;
    bool previousTimerState;
    float timeButtonPressed;
    HoldDetectionButton hdb;

    private void Start()
    {
        hdb = GetComponent<HoldDetectionButton>();
        isHoldStarted = false;
        timeButtonPressed = 0;
    }

    void Update()
    {
        if (hdb.buttonPressed)
        {
            if (!isHoldStarted)
            {
                isHoldStarted = true;
                previousTimerState = GameData.instance.GetCurrentTimerState();
                GameData.instance.StopCurrentTimer();
            }
            timeButtonPressed += Time.deltaTime;
            GameData.instance.DecreaseCurrentTimerValue(SmoothAccelerator.GetIncreasedValueOverTime(timeButtonPressed));
        }
        else if (isHoldStarted)
        {
            isHoldStarted = false;
            timeButtonPressed = 0;
            if (previousTimerState)
                GameData.instance.StartCurrentTimer();
        }
    }
}
