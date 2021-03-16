using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TotalResultsModel
{
    public List<int> results = new List<int>();
    public List<string> resultLabel = new List<string>();
    public int maxSubtextChars = 35;
    public string preText = null;
    public int total = 0;
}
