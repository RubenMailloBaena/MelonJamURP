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

    private AudioSource src;
    [SerializeField] AudioClip interactionSound;
    private bool interactionDone;

    private void Start()
    {
        smallLight = GameObject.Find("FormLight").GetComponent<Light2D>();

        src = gameObject.GetComponent<AudioSource>();
        src.volume = 0.5f;

        interactionDone = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            Debug.Log("Instrument Collected");
            onInstrument?.Invoke(this.gameObject);
        }
    }

    public void InstrumentAction()
    {
        lightPrincipal.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().color=Color.white;
        smallLight.color = smallColor;

        if (!interactionDone) {
            src.clip = interactionSound;
            src.Play();
            interactionDone = true;
        }
    }
}
