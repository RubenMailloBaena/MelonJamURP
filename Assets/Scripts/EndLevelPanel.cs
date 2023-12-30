using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPanel : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void showPanel() {
        gameObject.SetActive(true);
    }

    private void UpdateEndPanel() { 
    
    }

    private void RepeatLevel()
    {

    }

}
