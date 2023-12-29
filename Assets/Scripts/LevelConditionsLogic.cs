using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionsLogic : MonoBehaviour
{
    [Header("Gravity Variables")]
    [SerializeField] private float gravityScale = 2;

    [Header("Rainy Variables")]
    [SerializeField] private float slowValue = 5;


    private bool G = false, W = false, S = false, R = false;

    private GameController.LevelConditions levelCondition;
    private GameObject player;

    public bool touchingWall = true;

    private void Start()
    {
        levelCondition = gameObject.GetComponent<GameController>().getLevelCondition();
        player = GameObject.Find("Ball");

        switch (levelCondition)
        {
            case GameController.LevelConditions.Gravity:
                G = true;
                Gravity();
                break;

            case GameController.LevelConditions.Rainy:
                R = true;
                Rainy();
                break;

            case GameController.LevelConditions.Windy:
                W = true;
                Windy();
                break;

            case GameController.LevelConditions.Sound:
                S = true;
                Sound();
                break;

            default:
                //Case Nothing
                break;
        }
    }

    private void Update()
    {
        Gravity();
        Windy();
        Sound();
    }

    private void Gravity(){
        if (G && !touchingWall)
        {
            player.GetComponent<BallMovement>().YSpeed -= (gravityScale * Time.deltaTime);
        }
    }

    private void Rainy() {
        if (R) {
            player.GetComponent<BallMovement>().ballSpeed -= slowValue;
        }
    }

    private void Windy()
    {
        if (W)
        {

        }
    }

    private void Sound()
    {
        if (S)
        {

        }
    }





    private void SetTrue() 
    {
        touchingWall = true;
    }

    

    private void SetFalse()
    {
        touchingWall = false;
    }



    private void OnEnable()
    {
        PlayerInputs.selectDirection += SetFalse;
        BallMovement.onWall += SetTrue;
    }

    private void OnDisable()
    {
        PlayerInputs.selectDirection -= SetFalse;
        BallMovement.onWall -= SetTrue;
    }
}
