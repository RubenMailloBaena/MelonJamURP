using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionsLogic : MonoBehaviour
{
    [Header("Gravity Variables")]
    [SerializeField] private float gravityScale = 2;

    [Header("Rainy Variables")]
    [SerializeField] private float slowValue = 5;

    [Header("Windy Variables")]
    [SerializeField] private float WindValue = 5;

    [Header("Electricity Variables")]
    [SerializeField] private int LessJumps = 2;


    private bool G = false, W = false, S = false, R = false, usingPowerUp = false;

    private GameController.LevelConditions levelCondition;
    private GameObject player;

    private bool touchingWall = true;

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

            case GameController.LevelConditions.Electricity:
                S = true;
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
    }

    private void Gravity(){
        if (G && !touchingWall && !usingPowerUp)
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
        if (W && !touchingWall && !usingPowerUp)
        {
            player.GetComponent<BallMovement>().XSpeed -= (WindValue * Time.deltaTime);
        }
    }

    private void Electricity(GameObject gameObject)
    {
        if (S && gameObject.tag.Equals("Wall"))
        {
            this.gameObject.GetComponent<GameController>().totalJumps -= LessJumps;
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
        BallMovement.onElectricity += Electricity;
        PlayerInputs.changeDirection += SetTrue;
        PlayerInputs.stopChangeDirection += SetFalse;
    }

    private void OnDisable()
    {
        PlayerInputs.selectDirection -= SetFalse;
        BallMovement.onWall -= SetTrue;
        BallMovement.onElectricity -= Electricity;
        PlayerInputs.changeDirection -= SetTrue;
        PlayerInputs.stopChangeDirection -= SetFalse;
    }
}
