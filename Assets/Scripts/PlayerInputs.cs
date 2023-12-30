using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    //SALTO
    public KeyCode jumpKey;
    public static Action selectDirection;

    //RELENTIZAR TIEMPO
    public KeyCode slowTimeKey;
    public static Action slowTime;
    public static Action stopSlowTime;

    //CAMBIAR DIRECCION EN EL AIRE
    public KeyCode changeDirectionMidAir;
    public static Action changeDirection;
    public static Action stopChangeDirection;

    private bool levelDone = false;
    
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs() {
        if (!levelDone) {
            //SALTO
            if (Input.GetKeyDown(jumpKey))
            {
                selectDirection?.Invoke();
                Debug.Log(jumpKey + " pressed");
            }

            //SLOW TIME
            if (Input.GetKey(slowTimeKey))
            {
                slowTime?.Invoke();
                Debug.Log(slowTimeKey + " pressed");
            }
            if (Input.GetKeyUp(slowTimeKey))
            {
                stopSlowTime?.Invoke();
            }

            //CAMBIAR DIRECCION EN EL AIRE
            if (Input.GetKey(changeDirectionMidAir))
            {
                changeDirection?.Invoke();
            }
            if (Input.GetKeyUp(changeDirectionMidAir))
            {
                stopChangeDirection?.Invoke();
            }
        }
    }

    private void SetLevelWon() {
        levelDone = true;
    }

    private void OnEnable()
    {
        GameController.levelWon += SetLevelWon;
    }

    private void OnDisable()
    {
        GameController.levelWon -= SetLevelWon;
    }
}
