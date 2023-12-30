using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsLogic : MonoBehaviour
{
    private BallMovement playerScript;

    [Header("Slow Time Power Up")]
    [SerializeField] private float SlowTimePowerUpUseTime = 5f;
    [SerializeField] private float SlowMotionValue = 0.6f;
    private float timeToUse;
    private Image SlowTimeBar;


    [Header("Change Direction Mid-Air")]
    [SerializeField] private Color initialColor;
    [SerializeField] private Color usingColor;
    [SerializeField] private Color doneColor;
    private bool directionChanged;
    private Image changeDirectionIcon;


    private void Start()
    {
        playerScript = GameObject.Find("Ball").GetComponent<BallMovement>();

        //SLOW TIME
        SlowTimeBar = GameObject.Find("SlowTimeBar").GetComponent<Image>();
        timeToUse = SlowTimePowerUpUseTime;

        //CHANGE DIRECTION
        directionChanged = false;
        changeDirectionIcon = GameObject.Find("ChangeDirectionIcon").GetComponent<Image>();
        changeDirectionIcon.color = initialColor;
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
        if (!directionChanged) {
            changeDirectionIcon.color = usingColor;
            playerScript.ChangeShowArrowChangeDirection(true);
            playerScript.ChangeTouchingWall(true);
        }
    }

    private void StopChangeDirectionMidAir() {
        if (!directionChanged) {
            changeDirectionIcon.color = doneColor;
            playerScript.GetDirectionVector();
            playerScript.ChangeShowArrowChangeDirection(false);
            playerScript.ChangeTouchingWall(false);
            directionChanged = true;
        }
        
    }




    private void OnEnable()
    {
        PlayerInputs.slowTime += SlowTime;
        PlayerInputs.stopSlowTime += StopSlowTime;
        PlayerInputs.changeDirection += ChangeDirectionMidAir;
        PlayerInputs.stopChangeDirection += StopChangeDirectionMidAir;
    }

    private void OnDisable()
    {
        PlayerInputs.slowTime -= SlowTime;
        PlayerInputs.stopSlowTime -= StopSlowTime;
        PlayerInputs.changeDirection -= ChangeDirectionMidAir;
        PlayerInputs.stopChangeDirection -= StopChangeDirectionMidAir;
    }
}
