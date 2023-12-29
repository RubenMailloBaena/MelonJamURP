using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstrumentLogic : MonoBehaviour
{
    public static Action<GameObject> onInstrument;
    public Color intrumentUIColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            Debug.Log("Instrument Collected");
            onInstrument?.Invoke(this.gameObject);
        }
    }
}
