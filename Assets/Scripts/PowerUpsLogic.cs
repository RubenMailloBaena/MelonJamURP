using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsLogic : MonoBehaviour
{
    [Header("Slow Time Power Up")]
    [SerializeField] private float SlowTimePowerUpUseTime = 5f;
    [SerializeField] private float SlowMotionValue = 0.6f;
    private float timeToUse;
    private Image SlowTimeBar;


    //[Header("Change Direction Mid-Air")]


    private void Start()
    {
        //SLOW TIME
        SlowTimeBar = GameObject.Find("SlowTimeBar").GetComponent<Image>();
        timeToUse = SlowTimePowerUpUseTime;
    }

    //SLOW TIME
    private void SlowTime() {
        if (timeToUse >= 0)
        {
            Time.timeScale = SlowMotionValue;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            timeToUse -= Time.deltaTime;
        }
        else 
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
        SlowTimeBarUI();
    }

    private void StopSlowTime() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void SlowTimeBarUI() {
        SlowTimeBar.fillAmount = Map(timeToUse, 0f, SlowTimePowerUpUseTime, 0f, 1f);
    }
    
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }


    //CHANGE DIRECTION MID-AIR
    private void ChangeDirectionMidAir() {
        Debug.Log("CHANGING DIRECTION");
    }

















    private void OnEnable()
    {
        PlayerInputs.slowTime += SlowTime;
        PlayerInputs.stopSlowTime += StopSlowTime;
        PlayerInputs.changeDirection += ChangeDirectionMidAir;
    }

    private void OnDisable()
    {
        PlayerInputs.slowTime -= SlowTime;
        PlayerInputs.stopSlowTime -= StopSlowTime;
        PlayerInputs.changeDirection -= ChangeDirectionMidAir;
    }
}
