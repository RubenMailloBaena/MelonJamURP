using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PowerUpsLogic : MonoBehaviour
{
    private BallMovement playerScript;
    public GameObject playerPowerUpLight;

    [Header("Slow Time Power Up")]
    [SerializeField] private float SlowTimePowerUpUseTime = 5f;
    [SerializeField] private float SlowMotionValue = 0.6f;
    private float timeToUse;
    private Image SlowTimeBar;


    [Header("Change Direction Mid-Air")]
    [SerializeField] private int maxUses = 2;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color usingColor;
    [SerializeField] private Color doneColor;
    private bool directionChanged, touchingWall = true;
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

            playerPowerUpLight.GetComponent<Light2D>().color = playerScript.SlowTimeColor;
            playerPowerUpLight.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            playerPowerUpLight.SetActive(false);
        }
        SlowTimeBarUI();
    }

    private void StopSlowTime() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        playerPowerUpLight.SetActive(false);
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
        if (!directionChanged && !touchingWall) {
            changeDirectionIcon.color = usingColor;
            playerScript.ChangeShowArrowChangeDirection(true);
            playerScript.ChangeTouchingWall(true);

            playerPowerUpLight.GetComponent<Light2D>().color = playerScript.ChangeDirectionColor;
            playerPowerUpLight.SetActive(true);
        }
    }

    private void StopChangeDirectionMidAir() {
        if (!directionChanged && !touchingWall) {
            playerScript.GetDirectionVector();
            playerScript.ChangeShowArrowChangeDirection(false);
            playerScript.ChangeTouchingWall(false);
            PowerUpUsed();

            playerPowerUpLight.SetActive(false);
        }
    }

    private void PowerUpUsed() {
        maxUses--;
        if (maxUses == 0)
        {
            directionChanged = true;
            changeDirectionIcon.color = doneColor;
        }
        else 
        {
            changeDirectionIcon.color = initialColor;
        }
    }

    private void SetTrue() {
        touchingWall = true;
        playerPowerUpLight.SetActive(false);
    }

    private void SetFalse()
    {
        touchingWall = false;
    }




    private void OnEnable()
    {
        PlayerInputs.slowTime += SlowTime;
        PlayerInputs.stopSlowTime += StopSlowTime;
        PlayerInputs.changeDirection += ChangeDirectionMidAir;
        PlayerInputs.stopChangeDirection += StopChangeDirectionMidAir;
        PlayerInputs.selectDirection += SetFalse;
        BallMovement.onWall += SetTrue;
    }

    private void OnDisable()
    {
        PlayerInputs.slowTime -= SlowTime;
        PlayerInputs.stopSlowTime -= StopSlowTime;
        PlayerInputs.changeDirection -= ChangeDirectionMidAir;
        PlayerInputs.stopChangeDirection -= StopChangeDirectionMidAir;
        PlayerInputs.selectDirection -= SetFalse;
        BallMovement.onWall -= SetTrue;
    }
}
