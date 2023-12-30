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

    public GameObject EndLevelPanel;
    public static Action levelWon;
    private GameObject UI;
    private bool stopTimer = false;
    [SerializeField] private float timeToEndPanel = 1.5f;

    //Atributos para controlar las caracteristicas del nivel
    [Header("Caracteristicas del nivel")]
    [SerializeField] private LevelConditions levelCondition;
    [SerializeField] public int totalJumps = 20;
    private int initialJumps;
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
        stopTimer = false;

        jumpsLeftText = GameObject.Find("Shots remaining").GetComponent<TextMeshProUGUI>();
        jumpsLeftText.text = totalJumps.ToString();

        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        startTime = Time.time;

        initialJumps = totalJumps;
        UI = GameObject.Find("UI");
    }

    private void Update()
    {
        if(!stopTimer)
            Timer();
        WinActionFunction();
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
                levelInstruments[index].GetComponent<InstrumentLogic>().ActivateLight();
                Color instrumentColor = levelInstruments[index].GetComponent<InstrumentLogic>().intrumentUIColor;
                intrumentsUIObjects[index].GetComponent<Image>().color = instrumentColor;
                index++;
            }
        }
        catch (Exception e) {}
    }

    private void WinActionFunction() {
        StartCoroutine(WinAction());
    }

    IEnumerator WinAction() //CAMBIAR DE ESCENA
    {
        if (index == levelInstruments.Count) {
            levelWon?.Invoke();
            stopTimer = true;

            yield return new WaitForSeconds(timeToEndPanel);

            UI.GetComponentInChildren<Canvas>().enabled = false;
            EndLevelPanel.GetComponent<EndLevelPanel>().showPanel(nextLevelName, timerText, initialJumps - totalJumps, true);

            
        }
    }

    IEnumerator LossAction() {
        if (totalJumps <= 0 && index < levelInstruments.Count) {
            levelWon?.Invoke();
            stopTimer = true;

            yield return new WaitForSeconds(timeToEndPanel);

            UI.GetComponentInChildren<Canvas>().enabled = false;
            EndLevelPanel.GetComponent<EndLevelPanel>().showPanel(nextLevelName, timerText, initialJumps - totalJumps, false);
        }
    }

    private void LoosActionFunction() {
        StartCoroutine(LossAction());
    }


    //CONTROLAR LOS SALTOS RESTANTES EN LA UI
    private void UpdateUIText()
    {
        totalJumps--;
        jumpsLeftText.text = totalJumps.ToString();
    }

    private void UpdateUITextFromElectricity(GameObject notused) {
        jumpsLeftText.text = totalJumps.ToString();
    }



    public LevelConditions getLevelCondition() {
        return levelCondition;
    }

    private void OnEnable()
    {
        InstrumentLogic.onInstrument += checkIfCorrectInstrument;
        BallMovement.onJump += UpdateUIText;
        BallMovement.onWall += LoosActionFunction;
        BallMovement.onElectricity += UpdateUITextFromElectricity;
    }

    private void OnDisable()
    {
        InstrumentLogic.onInstrument -= checkIfCorrectInstrument;
        BallMovement.onJump -= UpdateUIText;
        BallMovement.onWall -= LoosActionFunction;
        BallMovement.onElectricity -= UpdateUITextFromElectricity;
    }
}
