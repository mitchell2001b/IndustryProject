using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MetricManager : MonoBehaviour
{
    [SerializeField] GameObject[] numberButtons;
    public readonly List<float> values = new();
    public readonly List<float> convertedValues = new();
    public readonly List<string> convertedValuesText = new();
    public float sum;
    public int playerAmount = 2;
    public TextMeshProUGUI sumText;
    private string metricText;

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

    void Start()
    {
        GenerateValues();
        GetMetricValues();
        MakeMetricSum();
        
        SetButtonValues();
        sumText.text = sum.ToString();
    }

    void Update()
    {

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

    public void SetButtonValues()
    {
        int counter = 0;
        foreach (var button in numberButtons)
        {
            button.GetComponent<SetButtonValue>().SetNumberButtonValue(
                convertedValues[counter], 
                convertedValuesText[counter]);
            counter++;
        }
    }

    public void GetMetricValues()
    {
        int rnd;
        MetricSubcatagory subcatagory;
        foreach (var number in values)
        {
            rnd = Random.Range(0, 6);
            subcatagory = (MetricSubcatagory)rnd;
            convertedValuesText.Add(SetMetricText(subcatagory));
            convertedValues.Add(ConvertToMetric(subcatagory, number));
        }
    }

    public float ConvertToMetric(MetricSubcatagory metric, float number)
    {
        switch (metric)
        {
            case MetricSubcatagory.milli:
                return number * 1000;
            case MetricSubcatagory.centi:
                return number * 100;
            case MetricSubcatagory.deci:
                return number * 10;
            case MetricSubcatagory.unit:
                break;
            case MetricSubcatagory.deca:
                return number / 10;
            case MetricSubcatagory.hecta:
                return number / 100;
            case MetricSubcatagory.kilo:
                return number / 1000;
        }
        return number;
    }

    public string SetMetricText(MetricSubcatagory metric)
    {
        switch (metric)
        {
            case MetricSubcatagory.milli:
                return "Milli";
            case MetricSubcatagory.centi:
                return "Centi";
            case MetricSubcatagory.deci:
                return "Deci";
            case MetricSubcatagory.unit:
                break;
            case MetricSubcatagory.deca:
                return "Deca";
            case MetricSubcatagory.hecta:
                return "Hecto";
            case MetricSubcatagory.kilo:
                return "Kilo";
        }
        return "";
    }
}
