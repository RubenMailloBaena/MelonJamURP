using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float shockWaveTime = 0.75f;

    private Coroutine shockWaveCouroutine;
    private Material material;
    private static int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    
    public void CallShockWave(GameObject instrument)
    {
        shockWaveCouroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f, instrument.transform));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos, Transform trans) {
        material.SetVector("RingSpawnPosition", trans.position);
        material.SetFloat(waveDistanceFromCenter, startPos);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < shockWaveTime) {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / shockWaveTime));

            material.SetFloat(waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }
    }

    private void OnEnable()
    {
        InstrumentLogic.onInstrument += CallShockWave;
    }

    private void OnDisable()
    {
        InstrumentLogic.onInstrument -= CallShockWave;
    }
}
