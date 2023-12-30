using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.Rendering.Universal;

public class InstrumentLogic : MonoBehaviour
{
    public static Action<GameObject> onInstrument;
    public Color intrumentUIColor;
    public GameObject lightPrincipal;
    public Light2D smallLight;
    [SerializeField]
    private Color smallColor;

    private void Start()
    {
        smallLight = GameObject.Find("FormLight").GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            Debug.Log("Instrument Collected");
            onInstrument?.Invoke(this.gameObject);
        }
    }

    public void ActivateLight()
    {
        lightPrincipal.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().color=Color.white;
        smallLight.color = smallColor;
    }
}
