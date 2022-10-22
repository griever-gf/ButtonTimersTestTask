﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameData : MonoBehaviour
{
    public static GameData instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Initialization();
    }

    private void Initialization()
    {
        timers = new List<CountdownTimer>();
    }

    List<CountdownTimer> timers;
    int currentTimerIndex;
    public double timerDefaultValue = 20; //in seconds, if there is not stored value for this timer
    public int timersCount = 3;

    public UnityEvent eventAnyTimerFinished;
    int finishedTimerIndex;

    public void AddTimer(int idx)
    {
        timers.Add(new CountdownTimer(DetermineTimerStartValue(idx)));
        timers[idx].eventTimerFinished.AddListener(() => OnAnyTimerFinished(idx));
    }

    public void RemoveTimer(int idx)
    {
        timers.RemoveAt(idx);
    }

    double DetermineTimerStartValue(int timer_idx)
    {
        if (true) //if there is not stored value in file/PlayerPrefs
            return timerDefaultValue;
    }

    public double GetCurrentTimerValue()
    {
        return timers[currentTimerIndex].GetTime();
    }

    public void SetCurrentTimer(int idx)
    {
        currentTimerIndex = idx;
    }

    public void StartCurrentTimer()
    {
        timers[currentTimerIndex].enabled = true;
    }

    public void StopCurrentTimer()
    {
        timers[currentTimerIndex].enabled = false;
    }

    public void DecreaseCurrentTimerValue(float delta)
    {
        timers[currentTimerIndex].DecreaseTime(delta);
    }

    public void IncreaseCurrentTimerValue(float val)
    {
        timers[currentTimerIndex].IncreaseTime(val);
    }

    public bool GetCurrentTimerState()
    {
        return timers[currentTimerIndex].enabled;
    }

    public int GetCurrentTimerIndex()
    {
        return currentTimerIndex;
    }

    public void OnAnyTimerFinished(int idx_timer)
    {
        //Debug.Log("Timer #" + idx_timer.ToString() + " finished");
        finishedTimerIndex = idx_timer;
        eventAnyTimerFinished.Invoke();
    }

    public int GetFinishedTimerIndex()
    {
        return finishedTimerIndex;
    }

    void Update()
    {
        for (int i = 0; i < timersCount; i++)
        {
            if (timers[i].enabled)
                timers[i].DecreaseTime(Time.deltaTime);
        }
    }
}
