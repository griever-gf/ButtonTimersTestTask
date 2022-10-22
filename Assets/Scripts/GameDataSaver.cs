using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameDataSaver : MonoBehaviour
{
    void SaveTimerValues()
    {
        GameData.instance.StopAllTimers();
        PlayerPrefs.SetInt("TimerTestTaskAmount", GameData.instance.timersCount);
        string data = "";
        for (int i = 0; i < GameData.instance.timersCount; i++)
            data += i.ToString() + ";" + GameData.instance.GetTimerValue(i) + ";" + GameData.instance.GetTimerValue(i) + Environment.NewLine;

        //string can be easily saved either to PlayerPrefs or Text File
        PlayerPrefs.SetString("TimerTestTaskValues", data);

        /*
        try
        {
            System.IO.File.WriteAllText(filename, data);
        }
        catch (System.Exception ex)
        {
            AddLog("Ошибка записи взаиморасположения и состояния поля " + ex.Message);
        }*/
    }

    private void OnApplicationQuit()
    {
        SaveTimerValues();
    }

    public static int LoadTimerCount()
    {
        return PlayerPrefs.GetInt("TimerTestTaskAmount", 0);
    }

    public static double[] LoadTimerValues()
    {
        string data = PlayerPrefs.GetString("TimerTestTaskValues", "");
        if (data != "")
        {
            using (var reader = new StringReader(data))
            {
                List<double> res_list = new List<double>();
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    try
                    {
                        string[] values = line.Split(';');
                        res_list.Add(Convert.ToDouble(values[1]));
                        Debug.Log(Convert.ToDouble(values[1]));
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(ex.Message);
                        return null;
                    }
                }
                return (res_list.ToArray());
            }
        }
        else
            return null;
    }
}
