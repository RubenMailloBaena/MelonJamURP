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

    
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs() {
        //SALTO
        if (Input.GetKeyDown(jumpKey)) { 
            selectDirection?.Invoke();
            Debug.Log(jumpKey + " pressed");
        }

        //SLOW TIME
        if (Input.GetKey(slowTimeKey)) {
            slowTime?.Invoke();
            Debug.Log(slowTimeKey + " pressed");
        }
        if (Input.GetKeyUp(slowTimeKey)) {
            stopSlowTime?.Invoke();
        }
    }
}
