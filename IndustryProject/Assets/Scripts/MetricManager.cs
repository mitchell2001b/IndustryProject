using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MetricManager : MonoBehaviour
{
    [SerializeField] KeyTracker keyTracker;
    [SerializeField] int questionCount;
    private int score = 0;
    [SerializeField] UnityEvent onComplete;
    [SerializeField] public int rngMin;
    [SerializeField] public int rngMax;
    [SerializeField] public int startTime;
    [SerializeField] PlaySound sound;
    [SerializeField] GameObject[] numberButtons;
    public List<float> values = new();
    public readonly List<MetricSubcatagory> metrics = new();
    public readonly List<float> convertedValues = new();
    public readonly List<string> convertedValuesText = new();
    public float answer;
    public float sum;
    public int playerAmount = 2;
    string convertedValue;
    private string metricText;
    private string convertionOptions;

    public enum MetricSubcatagory
    {
        milli,
        centi,
        deci,
        unit,
        deca,
        hecto,
        kilo
    }

    private void Start()
    {
        GameSetup();
    }


    private void Update()
    {
        CheckGivenAnswers();
        UpdatePlayerSum();
    }

    public void GameSetup()
    {
        ClearAllLists();
        SetAllClickedButtonsFalse();
        metricText = GetComponent<UIManager>().GenerateMetricUnit();
        GetUasableMetrics();
        values = GetComponent<SumGeneration>().GenerateValues(numberButtons.Length, rngMin, rngMax);
        AddConvertionsToList();
        sum = GetComponent<SumGeneration>().MakeMetricSum(playerAmount, values);
        AddMetricToSum();
        GetComponent<UIManager>().SetMetricStartText(sum, convertedValue, metricText, score, startTime);
        GetComponent<UIManager>().SetButtonValues(numberButtons, convertedValues, convertedValuesText, metricText);
        StopAllCoroutines();
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(GetComponent<UIManager>().UpdateTimer(startTime, 1));
        }
        
    }

    //Chooses 3 metrics to convert numbers to
    public void GetUasableMetrics()
    {
        int rnd = Random.Range(0, 3);
        if (rnd == 0)
        {
            metrics.Add((MetricSubcatagory)rnd + 1);
            metrics.Add((MetricSubcatagory)rnd);
            metrics.Add((MetricSubcatagory)rnd + 2);
            convertionOptions = "milli";
        }
        else if (rnd == 2)
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
            convertionOptions = "Other";
        }
    }

    //Adds the right metric text and converted numbers to their own list to be used for the buttons
    public void AddConvertionsToList()
    {
        int rnd;
        foreach (var number in values)
        {
            rnd = Random.Range(0, 3);
            float convertedNumber = GetComponent<MetricConverter>().ConvertToMetric(rnd, number, convertionOptions);
            convertedValuesText.Add(GetSubMetricText(metrics[rnd]));
            convertedValues.Add(convertedNumber);
        }
    }

    //This adds the correct metric text to the sum
    public void AddMetricToSum()
    {
        convertedValue = GetSubMetricText(metrics[1]);
    }

    public string GetSubMetricText(MetricSubcatagory metric)
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
            case MetricSubcatagory.hecto:
                return "H";
            case MetricSubcatagory.kilo:
                return "K";
        }
        return "";
    }

    public void CheckGivenAnswers()
    {
        if (GetComponent<UIManager>().timerText.text == "0")
        {
            GetGivenAnswers();
            CompareAnswerSum(answer);
        }
    }

    private void GetGivenAnswers()
    {
        int counter = 0;
        answer = 0;
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
    }

    public void CompareAnswerSum(float answer)
    {
        if (answer == sum)
        {
            GoodAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void GoodAnswer()
    {
        sound.playButton();
        //show answer was right
        Debug.Log("YAAAAAAAAAAAAAAAY");
        score++;
        if(score >= questionCount)
        {
            onComplete.Invoke();
        }
        GameSetup();
    }

    public void WrongAnswer()
    {
        //show answer was wrong
        Debug.Log("BOOOOOOOOOOOOOOOO");
        GameSetup();
    }

    public void UpdatePlayerSum()
    {
        int counter = 0;
        float playerSum = 0;
        foreach (var button in numberButtons)
        {
            if (button.GetComponent<SetButtonValue>().isPressed)
            {
                playerSum += values[counter];
            }
            GetComponent<UIManager>().UpdatePlayerMetricSum(playerSum);
            counter++;
        }
    }

    public void SetAllClickedButtonsFalse()
    {
        foreach (var buttom in numberButtons)
        {
            buttom.GetComponent<SetButtonValue>().SetPressedFalse();
        }
    }

    public void ClearAllLists()
    {
        values.Clear();
        convertedValues.Clear();
        convertedValuesText.Clear();
        metrics.Clear();
    }

}