using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public enum LevelConditions { //Para seleccionar las posibles condiciones del nivel
        Nothing,
        Gravity,
        Rainy,
        Windy,
        Electricity,
    }

    //Atributos para controlar las caracteristicas del nivel
    [Header("Caracteristicas del nivel")]
    [SerializeField] private LevelConditions levelCondition;
    [SerializeField] private int totalJumps = 20;
    [SerializeField] private String nextLevelName = "Level_2"; 

    [Header("Instruments")]
    [SerializeField] private List<GameObject> levelInstruments;
    [SerializeField] private List<GameObject> intrumentsUIObjects;

    private TextMeshProUGUI jumpsLeftText;
    private TextMeshProUGUI timerText;
    private float startTime;
    private int index = 0;

    private void Start()
    {
        jumpsLeftText = GameObject.Find("Shots remaining").GetComponent<TextMeshProUGUI>();
        jumpsLeftText.text = totalJumps.ToString();

        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
    }

    private void Update()
    {
        Timer();
        WinAction();
    }
    
    //TIMER DE LA UI
    private void Timer() 
    {
        float transcurredTime = Time.time - startTime;

        int minutes = (int)(transcurredTime / 60);
        int seconds = (int)(transcurredTime % 60);
        int milliseconds = (int)((transcurredTime * 1000) % 1000);

        timerText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    //ILUMINAR LOS INSTRUMENTOS EN EL ORDEN CORRECTO
    private void checkIfCorrectInstrument(GameObject instrument) {

        try
        {
            if (instrument == levelInstruments[index])
            {
                Debug.Log("Got Next One");
                Color instrumentColor = levelInstruments[index].GetComponent<InstrumentLogic>().intrumentUIColor;
                intrumentsUIObjects[index].GetComponent<Image>().color = instrumentColor;
                index++;
            }
        }
        catch (Exception e) {}
    }

    private void WinAction() //CAMBIAR DE ESCENA
    {
        if (index == levelInstruments.Count) {
            this.gameObject.GetComponent<SceneChanger>().CambiarEscena(nextLevelName);
        }
    }

    private void LossAction() {
        if (totalJumps <= 0 && index < levelInstruments.Count) {
            Debug.Log("Lossing");
        }
    }


    //CONTROLAR LOS SALTOS RESTANTES EN LA UI
    private void UpdateUIText()
    {
        totalJumps--;
        jumpsLeftText.text = totalJumps.ToString();
    }



    public LevelConditions getLevelCondition() {
        return levelCondition;
    }

    private void OnEnable()
    {
        InstrumentLogic.onInstrument += checkIfCorrectInstrument;
        BallMovement.onJump += UpdateUIText;
        BallMovement.onWall += LossAction;
    }

    private void OnDisable()
    {
        InstrumentLogic.onInstrument -= checkIfCorrectInstrument;
        BallMovement.onJump -= UpdateUIText;
        BallMovement.onWall -= LossAction;
    }
}
