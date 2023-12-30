using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] public float ballSpeed = 5f;
    [HideInInspector] private float speedIncrementation = 0f;
    public bool touchingWall = true;
    private bool touchingMovingWall = false;
    [HideInInspector] public Vector2 direction;

    [HideInInspector] public float YSpeed;
    [HideInInspector] public float XSpeed;

    public static Action onJump;
    public static Action onWall;
    public static Action<GameObject> onElectricity;

    [Header("Other Components")]
    [SerializeField] private GameObject arrowPoint; //Referencia a la flecha de direccion
    [SerializeField] private Rigidbody2D rb; //For velocity

    [Header("Power Up Lights")]
    public Color SlowTimeColor;
    public Color ChangeDirectionColor;

    private bool levelWon = false;



    void FixedUpdate()
    {
        Movement();
    }


    //Movimiento de la pelota
    private void Movement()
    {
        if (!touchingWall && !levelWon)
        {
            transform.position += new Vector3(direction.x * (XSpeed * Time.deltaTime), direction.y * (YSpeed * Time.deltaTime), 0f);
        }
    }

    //Cunado estamos en una pared, seleccionamos la nueva direccion de movimento
    private void GetNewDirection()
    {

        if (touchingWall && !arrowPoint.GetComponent<DirectionArrow>().getIsColliding())
        {
            onJump?.Invoke();

            YSpeed = ballSpeed;
            XSpeed = ballSpeed;

            GetDirectionVector();
            touchingWall = false;
            ChangeShowArrow(touchingWall);
            IncrementBallSpeed();

            if (touchingMovingWall) { //Chocar pared con movimiento
                touchingMovingWall = false;
                transform.SetParent(null);
            }
        }
    }


    public void GetDirectionVector() {
        direction = arrowPoint.transform.position - transform.position;
        YSpeed = ballSpeed;
        XSpeed = ballSpeed;
    }

    public void ChangeShowArrow(bool show) {
        arrowPoint.GetComponent<DirectionArrow>().setArrowLine(show);
    }

    public void ChangeShowArrowChangeDirection(bool show)
    {
        arrowPoint.GetComponent<DirectionArrow>().setArrowLineChangeDirection(show);
    }

    public void ChangeTouchingWall(bool change) {
        touchingWall = change;
    }


    private void IncrementBallSpeed()
    {
        ballSpeed += speedIncrementation;
        arrowPoint.GetComponent<DirectionArrow>().IncrementArrowSpeed();
    }

    //Cunado tocamos una pared, detemos el movimiento
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onWall?.Invoke();
        onElectricity?.Invoke(collision.gameObject);

        Debug.Log("Ball Colliding Wall"); //CHOQUEM AMB UNA PARET NORMAL
        touchingWall = true;
        ChangeShowArrow(touchingWall);

        if (collision.gameObject.tag.Equals("MovingWall")) //CHOQUEM AMB PARET EN MOVIMENT
        {
            touchingMovingWall = true;
            transform.SetParent(collision.transform);
        }
    }


    //Dibujar la direccion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, arrowPoint.transform.position);
    }

    //Controlar las Actions
    private void OnEnable()
    {
        PlayerInputs.selectDirection += GetNewDirection;
        GameController.levelWon += SetLevelWon;
    }

    private void OnDisable()
    {
        PlayerInputs.selectDirection -= GetNewDirection;
        GameController.levelWon -= SetLevelWon;
    }


    public bool getTouchingWall() { return touchingWall; }

    private void SetLevelWon()
    {
        levelWon = true;
    }
}