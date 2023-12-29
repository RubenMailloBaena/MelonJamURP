using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditionsLogic : MonoBehaviour
{
    [Header("Gravity Variables")]
    [SerializeField] private float gravityScale = 2;


    private bool G = false, W = false, S = false, M = false, E = false, R = false;

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
                break;

            case GameController.LevelConditions.Rainy:
                R = true;
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
        if (G && !touchingWall)
        {
            //Debug.Log(player.GetComponent<BallMovement>().YSpeed);
            player.GetComponent<BallMovement>().YSpeed -= (gravityScale * Time.deltaTime);
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

    private void SetRoofTrue() 
    {
    }

    private void SetFalse()
    {
        touchingWall = false;
    }



    private void OnEnable()
    {
        PlayerInputs.selectDirection += SetFalse;
        BallMovement.onWall += SetTrue;
        BallMovement.onRoof += SetRoofTrue;
    }

    private void OnDisable()
    {
        PlayerInputs.selectDirection -= SetFalse;
        BallMovement.onWall -= SetTrue;
        BallMovement.onRoof -= SetRoofTrue;
    }
}
