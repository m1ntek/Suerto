using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NumGenerator
{
    //decided not to use the unity's random class
    private static System.Random rnd = new System.Random();

    public static int GetResult(int _maxNum)
    {
        return rnd.Next(1, _maxNum);
    }
}
