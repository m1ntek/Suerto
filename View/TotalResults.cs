using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalResults : MonoBehaviour
{
    public void DisplayTrimResults(ref TotalResultsModel _trModel, TMPro.TextMeshPro _totalSubtext)
    {
        int counter = 0;
        _totalSubtext.text = "";

        //get the last 35 characters of the string and just display that.
        //see the most recent calculations of the last few throws of dice.
        if (_trModel.preText.Length >= _trModel.maxSubtextChars)
        {
            for (int i = _trModel.preText.Length - _trModel.maxSubtextChars; i < _trModel.preText.Length; i++)
            {
                if (counter < 3)
                {
                    _totalSubtext.text += ".";
                    ++counter;
                }
                else
                {
                    _totalSubtext.text += _trModel.preText[i];
                }
            }
        }
        else
        {
            _totalSubtext.text = _trModel.preText;
        }
    }
}
