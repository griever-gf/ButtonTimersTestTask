using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmoothAccelerator
{
    public static float GetIncreasedValueOverTime(float time_val)
    {
        time_val *= 1000; //get milliseconds
        time_val =  Mathf.Pow(time_val, 1.1f);
        time_val = (time_val > 20000) ? 20000 : time_val;
        time_val /= 100;
        return time_val;
    }
}
