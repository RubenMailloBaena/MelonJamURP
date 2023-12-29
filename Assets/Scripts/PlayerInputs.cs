using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    public static Action selectDirection;
    public KeyCode jumpKey;
    
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs() {
        if (Input.GetKeyDown(jumpKey)) { 
            selectDirection?.Invoke();
            Debug.Log(jumpKey + " pressed");
        }
    }
}
