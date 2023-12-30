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

    [SerializeField] private float audioSoundLevel = 0.5f;
    [SerializeField] private AudioClip VictorySound;
    [SerializeField] private AudioClip LostSound;
    private AudioSource src;

    private void Awake()
    {
        gameObject.SetActive(false);
        sceneManager = gameObject.GetComponent<SceneChanger>();

        src = gameObject.GetComponent<AudioSource>();
        src.volume = audioSoundLevel;
    }

    public void showPanel(String nextLevelName, TextMeshProUGUI timer, int jumpsDone, bool win) {
        gameObject.SetActive(true);
        totalTime.text = timer.text;
        this.jumpsDone.text = jumpsDone.ToString();
        this.nextLevelName = nextLevelName;

        if (win) {
            title.text = "Level Completed";
            src.clip = VictorySound;
        }
        else {
            title.text = "Game Over";
            src.clip = LostSound;
        }
        Debug.Log("SOUND");
        src.Play();
    }

    public void RepeatLevel()
    {
        sceneManager.RepeatLevel();
    }

    public void GoNextLevel() {
        sceneManager.CambiarEscena(nextLevelName);
    }
}
