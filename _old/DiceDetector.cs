using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.layer = 8;
    }
}
