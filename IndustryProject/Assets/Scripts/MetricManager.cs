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
    public TextMeshProUGUI timer;
    public TextMeshProUGUI ScoreText;
    private string metricText;
    private string convertionOptions;
    private int goodAnswers = 0;
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
        Go();
    }

    private void Update()
    {
        CheckGivenAnswers();
    }

    public void Go()
    {
        values.Clear();
        convertedValues.Clear();
        convertedValuesText.Clear();
        metrics.Clear();
        AllButtonsFalse();
        metricText = GenerateMetric();
        GetUasableMetrics();
        GenerateValues();
        ConvertAnswerButtonValues();
        sum = GetComponent<SumGeneration>().MakeMetricSum(playerAmount, values);
        ConvertSum();
        timer.text = 15.ToString();
        sumText.text = sum.ToString() + " " + convertedValue + metricText;
        ScoreText.text = "0/3 goed";
        GetComponent<UIManager>().SetButtonValues(numberButtons, convertedValues, convertedValuesText, metricText);
        StopAllCoroutines();
        StartCoroutine(GetComponent<UIManager>().UpdateTimer(15, 1));
    }

    //Generates values for the buttons and sum
    public void GenerateValues()
    {
        for (int i = 0; i < 6; i++)
        {
            values.Add(Random.Range(1, 101));
        }
    }

    //Chooses 3 metrics to convert numbers to
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
    
    //Adds the right metric text and converted numbers to their own list
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
        MetricSubcatagory subcatagory = metrics[1];
        convertedValue = SetMetricText(subcatagory);
    }


    //Puts the correct metric and number on the button
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

    //Converts the generated numbers to metrics
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

    public void CheckGivenAnswers()
    {
        if (timer.text == "0")
        {
            GetGivenAnswers();
        }

    }

    private void GetGivenAnswers()
    {
        int counter = 0;
        float answer = 0;
        foreach (var button in numberButtons)
        {
            if (button.GetComponent<SetButtonValue>().isPressed)
            {
                Debug.Log(button.GetComponent<SetButtonValue>().isPressed);
                answer += values[counter];
            }
            counter++;
            Debug.Log(answer);
        }
        if (answer == sum)
        {
            Debug.Log("YAAAAAAAAAAAAAAAY");
            goodAnswers++;
            Go();
        }
        else
        {
            Debug.Log("BOOOOOOOOOOOOOOOO");
            Go();
        }
    }

    public void AllButtonsFalse()
    {
        foreach (var buttom in numberButtons)
        {
            buttom.GetComponent<SetButtonValue>().SetPressedFalse();
        }
    }

}
