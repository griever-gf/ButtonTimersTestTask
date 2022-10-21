using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    List<CountdownTimer> timers;
    int currentTimerIndex;
    double timerDefaultValue = 60;
    public int timersCount = 3;

    public void SpawnTimers(int amount)
    {
        timers = new List<CountdownTimer>();
        for (int i=0; i< timersCount; i++)
        {
            timers.Add(new CountdownTimer(DetermineTimerStartValue(i)));
        }
    }

    double DetermineTimerStartValue(int timer_idx)
    {
        if (true) //if no saved value in file/PlayerPrefs
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

    public void UpdateCurrentTimerValue(float delta)
    {
        timers[currentTimerIndex].UpdateTime(delta);
    }

    void Update()
    {
        for (int i = 0; i < timersCount; i++)
        {
            if (timers[i].enabled)
                timers[i].UpdateTime(Time.deltaTime);
        }
    }
}
