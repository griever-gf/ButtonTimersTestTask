using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SmoothAccelerator
{
    const float MAX_VAL = 20;
    const float SHARPNESS_PARAM = 2.3f; //the more the sharper, min = 2.0f
    const float GROWTH_SPEED_PARAM = 6.5f; //the more the slower

    public static float GetIncreasedValueOverTime(float time_val)
    {
        float res = MAX_VAL * (1f - (float)Mathf.Exp(-1f * Mathf.Pow(time_val/ GROWTH_SPEED_PARAM, SHARPNESS_PARAM)));

        return res;
    }
}
