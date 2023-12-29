using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionsLogic : MonoBehaviour
{
    private bool G = false, W = false, S = false, M = false, E = false;

    private GameController.LevelConditions levelCondition;
    private Rigidbody2D playerRB;

    private bool touchingWall = true;

    private void Start()
    {
        levelCondition = this.gameObject.GetComponent<GameController>().getLevelCondition();
        playerRB = GameObject.Find("Ball").GetComponent<Rigidbody2D>();

        switch (levelCondition)
        {
            case GameController.LevelConditions.Gravity:
                G = true;
                break;

            case GameController.LevelConditions.Windy:
                W = true;
                break;

            case GameController.LevelConditions.Sound:
                S = true;
                break;

            case GameController.LevelConditions.Magnetism:
                M = true;
                break;

            case GameController.LevelConditions.Electricity:
                E = true;
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
        Magnetism();
        Electricity();
    }

    private void Gravity(){
        if (G && !touchingWall) {
            Debug.LogWarning(touchingWall);

        }
    }

   

    

    private void Windy()
    {
    }

    private void Sound()
    {
    }

    private void Magnetism()
    {
    }

    private void Electricity()
    {
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
        BallMovement.onJump += SetFalse;
        BallMovement.onWall += SetTrue;
    }

    private void OnDisable()
    {
        BallMovement.onJump -= SetFalse;
        BallMovement.onWall -= SetTrue;
    }
}
