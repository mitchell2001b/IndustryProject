using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SumGeneration : MonoBehaviour
{
    public readonly List<int> values = new();
    public float answer { get; private set; }
    public MathOperators mop;

    public enum MathOperators
    {
        plus,
        min,
        times,
        divided
    }

    public float MakeSum(int buttonAmount, int random)
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
        answer = (float)Math.Round(MakeAnswer(number1, number2, random), 2);
        Debug.Log(number1 + " " + number2);
        return number2;
    }

    public float MakeAnswer(float left, float right, int rnd)
    {
        mop = (MathOperators)rnd;
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
}
