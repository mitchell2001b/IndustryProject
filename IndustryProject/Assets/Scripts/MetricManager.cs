using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MetricManager : MonoBehaviour
{
    [SerializeField] GameObject[] numberButtons;
    public readonly List<float> values = new();
    public readonly List<MetricSubcatagory> metrics = new();
    public readonly List<float> convertedValues = new();
    public readonly List<string> convertedValuesText = new();
    string convertedValue;
    public float sum;
    public int playerAmount = 2;
    public TextMeshProUGUI sumText;
    private string metricText;
    private string convertionOptions;
    enum MetricSystem
    {
        meter,
        gram,
        liter
    }

    public enum MetricSubcatagory
    {
        milli,
        centi,
        deci,
        unit,
        deca,
        hecta,
        kilo
    }
    
    public void GenerateValues()
    {
        for (int i = 0; i < 6; i++)
        {
            values.Add(Random.Range(1, 101));
        }
    }
    public int CheckAnswerAmount()
    {
        if (playerAmount > 2)
        {
            return Random.Range(2, 5);
        }
        return 2;
    }

    public void GetUasableMetrics()
    {
        int rnd = Random.Range(0, 7);
        if (rnd == 0)
        {
            metrics.Add((MetricSubcatagory)rnd + 1);
            metrics.Add((MetricSubcatagory)rnd);
            metrics.Add((MetricSubcatagory)rnd + 2);
            convertionOptions = "milli";
        }
        else if (rnd == 6)
        {
            metrics.Add((MetricSubcatagory)rnd - 1);
            metrics.Add((MetricSubcatagory)rnd);
            metrics.Add((MetricSubcatagory)rnd - 2);
            convertionOptions = "kilo";
        }
        else
        {
            metrics.Add((MetricSubcatagory)rnd - 1);
            metrics.Add((MetricSubcatagory)rnd);
            metrics.Add((MetricSubcatagory)rnd + 1);
        }
    }   

    public void MakeMetricSum()
    {
        int answerAmount = CheckAnswerAmount();
        float answer = 0;
        List<int> doubles = new();
        for (int i = 0; i < answerAmount; i++)
        {
            int x = Random.Range(0, values.Count);
            if (!doubles.Contains(x))
            {
                doubles.Add(x);
            }
            else
            {
                while (doubles.Contains(x))
                {
                    x = Random.Range(0, values.Count);
                }
                doubles.Add(x);
            }
            answer += values[x];
            Debug.Log(answer);
        }
        sum = answer;
    }

    public void ConvertAnswerButtonValues()
    {
        int rnd;
        foreach (var number in values)
        {
            rnd = Random.Range(0, 3);
            ConvertToMetric(rnd, number);
            convertedValuesText.Add(SetMetricText(metrics[rnd]));
            convertedValues.Add(ConvertToMetric(rnd, number));
        }
    }

    public void ConvertSum()
    {
        //int rnd = Random.Range(0, 3);
        MetricSubcatagory subcatagory = metrics[1];
        sum = ConvertToMetric(1, sum);
        convertedValue = SetMetricText(subcatagory);
    }




    void Start()
    {
        Go();
    }

    public void Go()
    {
        values.Clear();
        convertedValues.Clear();
        convertedValuesText.Clear();
        metrics.Clear();
        metricText = GenerateMetric();
        GetUasableMetrics();
        GenerateValues();
        ConvertAnswerButtonValues();
        MakeMetricSum();
        ConvertSum();
        Debug.Log(convertedValuesText.Count);
        SetButtonValues();
        sumText.text = sum.ToString() + " " + convertedValue + metricText;
    }


    public void SetButtonValues()
    {
        int counter = 0;
        foreach (var button in numberButtons)
        {
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(
                convertedValues[counter], 
                convertedValuesText[counter],
                metricText);
            counter++;
        }
    }

    public float ConvertToMetric(int random, float number)
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

    public string SetMetricText(MetricSubcatagory metric)
    {
        switch (metric)
        {
            case MetricSubcatagory.milli:
                return "M";
            case MetricSubcatagory.centi:
                return "C";
            case MetricSubcatagory.deci:
                return "D";
            case MetricSubcatagory.unit:
                break;
            case MetricSubcatagory.deca:
                return "Da";
            case MetricSubcatagory.hecta:
                return "H";
            case MetricSubcatagory.kilo:
                return "K";
        }
        return "";
    }

    public string GenerateMetric()
    {
        int rnd = Random.Range(0, 4);
        MetricSystem metric = (MetricSystem)rnd;
        switch (metric)
        {
            case MetricSystem.meter:
                return "M";
            case MetricSystem.gram:
                return "G";
            case MetricSystem.liter:
                return "L";
            default:
                return "M";
        }
    }
}
