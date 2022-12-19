using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MetricManager;

public class MetricConverter : MonoBehaviour
{
    public float ConvertToMetric(int random, float number, string convertionOptions)
    {
        if (convertionOptions == "milli")
        {
            if (random == 0)
                return number / 10;
            else if (random == 1)
                return number;
            else if (random == 2)
                return number / 100;
            return number;
        }
        else if (convertionOptions == "kilo")
        {
            if (random == 0)
                return number * 10f;
            else if (random == 1)
                return number;
            else if (random == 2)
                return number * 100f;
            return number;
        }
        else
        {
            if (random == 0)
                return number / 0.1f;
            else if (random == 1)
                return number;
            else if (random == 2)
                return number / 10;
            return number;
        }
    }
}
