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
    public float answer { get; private set; }



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

    public void MakeSum(int buttonAmount)
    {
        int rnd;
        int previousNumber = 0;
        float number1 = 0;
        float number2 = 0;
        for (int i = 0; i < 2; i++)
        {
            rnd = Random.Range(0, buttonAmount);
            if (i == 0)
            {
                number1 = values[rnd];
                previousNumber = rnd;
            }
            else if (i == 1)
            {
                if (previousNumber != rnd)
                    number2 = values[rnd];
                else
                {
                    while (previousNumber == rnd)
                    {
                        rnd = Random.Range(0, buttonAmount);
                        number2 = values[rnd];
                    }
                }
            }
        }
        answer = (float)Math.Round(MakeAnswer(number1, number2), 2);
        Debug.Log(number1 + " " + number2);
    }

    public float MakeAnswer(float left, float right)
    {
        MathOperators mop = (MathOperators)Random.Range(0, 4);
        Debug.Log(mop);
        switch (mop)
        {
            case MathOperators.plus:
                return left + right;
            case MathOperators.min:
                return left - right;
            case MathOperators.times:
                return left * right;
            case MathOperators.divided:
                return left / right;
            default:
                break;
        }
        return 0;
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
            //This if else makes sure the answer can't be made with a numbers from a button that is already used
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
