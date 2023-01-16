using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MinigameManager;
using Random = UnityEngine.Random;

public class SumGeneration : MonoBehaviour
{
    public List<float> values = new();
    List<int> doubles = new();
    public float answer { get; private set; }
    public float inputB;

    public string correctOperator = "";

    /// the number you want to generate from.
    public int timesStart = 1;
    public int divideStart = 1;
    public int plusMinStart = 1;
    /// the number you want to generate toward.
    public int timesEnd = 20;
    public int divideEnd = 100;
    public int plusMinEnd = 1000;


    //Generates values for the buttons and sum
    public List<float> GenerateValues(int amountOfValues, int from, int to)
    {
        for (int i = 0; i < amountOfValues; i++)
        {
            values.Add(Random.Range(from, to + 1));
        }
        return values;
    }

    #region Calculator
    public enum MathOperators
    {
        plus,
        min,
        times,
        divided
    }


    public void GenerateSum(int buttonAmount)
    {
        MathOperators mop = (MathOperators)Random.Range(0, 4);
        GetValues(buttonAmount);
        Debug.Log(mop);
        switch (mop)
        {
            case MathOperators.plus:
                correctOperator = "+";
                GenerateValues(buttonAmount, plusMinStart, plusMinEnd);
                answer = values[doubles[0]] + values[doubles[1]];
                inputB = values[doubles[1]];
                break;
            case MathOperators.min:
                correctOperator = "-";
                GenerateValues(buttonAmount, plusMinStart, plusMinEnd);
                MakeSumMin(values[doubles[0]], values[doubles[1]]);
                break;
            case MathOperators.times:
                correctOperator = "x";
                GenerateValues(buttonAmount, timesStart, timesEnd);
                answer = values[doubles[0]] * values[doubles[1]];
                inputB = values[doubles[1]];
                break;
            case MathOperators.divided:
                correctOperator = ":";
                GenerateValues(buttonAmount, divideStart, divideEnd);
                MakeSumDivide(buttonAmount);
                break;
            default:
                break;
        }
        Debug.Log(values[doubles[0]] + " ------------------- " + values[doubles[1]]);
    }

    public void GetValues(int buttonAmount)
    {
        doubles.Clear();
        for (int i = 0; i < 2; i++)
        {
            int rnd = Random.Range(0, buttonAmount);
            if (!doubles.Contains(rnd))
            {
                doubles.Add(rnd);
            }
            else
            {
                while (doubles.Contains(rnd))
                {
                    rnd = Random.Range(0, metricValues.Count);
                }
                doubles.Add(rnd);
            }
        }
    }


    public void MakeSumMin(float number1, float number2)
    {
        if (number1 > number2)
        {
            answer = number1 - number2;
            inputB = number2;
        }
        else
        {
            answer = number2 - number1;
            inputB = number1;
        }
    }

    public void MakeSumDivide(int buttonAmount)
    {
        if (values[doubles[0]] > values[doubles[1]])
        {
            answer = values[doubles[0]] / values[doubles[1]];
            while (answer % 1 != 0)
            {
                values.Clear();
                GenerateValues(buttonAmount, timesStart, timesEnd);
                GetValues(buttonAmount);
                answer = values[doubles[0]] / values[doubles[1]];
                inputB = values[doubles[1]];
            }
        }
        else
        {
            answer = values[doubles[1]] / values[doubles[0]];
            while (answer % 1 != 0)
            {
                values.Clear();
                GenerateValues(buttonAmount, timesStart, timesEnd);
                GetValues(buttonAmount);
                answer = values[doubles[1]] / values[doubles[0]];
                inputB = values[doubles[0]];
            }
        }
    }

    #endregion

    #region Metric
    public List<float> metricValues = new();

    //Checks how many answers can be given based on the amount of players
    public int CheckAnswerAmount(int playerAmount)
    {
        if (playerAmount > 2)
        {
            return Random.Range(2, playerAmount + 1);
        }
        return 2;
    }

    //generates a sum based on non converted numbers and the amount of players
    public float MakeMetricSum(int playerAmount, List<float> generatedValues)
    {
        metricValues = generatedValues;
        int answerAmount = CheckAnswerAmount(playerAmount);
        float answer = 0;
        List<int> doubles = new();
        for (int i = 0; i < answerAmount; i++)
        {
            int x = Random.Range(0, metricValues.Count);
            //This if else makes sure the answer can't be made with 2 numbbers from the same button
            if (!doubles.Contains(x))
            {
                doubles.Add(x);
            }
            else
            {
                while (doubles.Contains(x))
                {
                    x = Random.Range(0, metricValues.Count);
                }
                doubles.Add(x);
            }
            answer += metricValues[x];
            Debug.Log(answer);
        }
        return answer;
    }
    #endregion

}
