using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance = null;

    void Start()
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

    public void SpawnTimers(int amount)
    {
        timers = new List<CountdownTimer>(amount);
    }
}
