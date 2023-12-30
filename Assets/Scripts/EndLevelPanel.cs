using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EndLevelPanel : MonoBehaviour
{
    public TextMeshProUGUI totalTime;
    public TextMeshProUGUI jumpsDone;
    public TextMeshProUGUI title;

    private String nextLevelName;
    private SceneChanger sceneManager;

    private void Awake()
    {
        gameObject.SetActive(false);
        sceneManager = gameObject.GetComponent<SceneChanger>();
    }

    public void showPanel(String nextLevelName, TextMeshProUGUI timer, int jumpsDone, bool win) {
        gameObject.SetActive(true);
        totalTime.text = timer.text;
        this.jumpsDone.text = jumpsDone.ToString();
        this.nextLevelName = nextLevelName;

        if (win)
            title.text = "Level Completed";
        else 
            title.text = "Game Over";
    }

    public void RepeatLevel()
    {
        sceneManager.RepeatLevel();
    }

    public void GoNextLevel() {
        sceneManager.CambiarEscena(nextLevelName);
    }
}
