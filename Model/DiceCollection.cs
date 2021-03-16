using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceCollection
{
    public static List<Rigidbody2D> throwList = new List<Rigidbody2D>();
    public static List<DiceV3> roundList = new List<DiceV3>();
}
