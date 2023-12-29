using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] public float ballSpeed = 5f;
    [HideInInspector] private float speedIncrementation = 0f;
    private bool touchingWall = true, touchingMovingWall = false;
    private Vector2 direction;

    [HideInInspector] public float YSpeed;
    [HideInInspector] public float XSpeed;

    public static Action onJump;
    public static Action onWall;

    [Header("Other Components")]
    [SerializeField] private GameObject arrowPoint; //Referencia a la flecha de direccion
    [SerializeField] private Rigidbody2D rb; //For velocity

    

    void FixedUpdate()
    {
        Movement();
    }


    //Movimiento de la pelota
    private void Movement()
    {
        if (!touchingWall)
        {
            //MOVIMIENTO CON TRANSFORM
            transform.position += new Vector3(direction.x * (ballSpeed * Time.deltaTime), direction.y * YSpeed * Time.deltaTime, 0f);
            //MOVIMIENTO CON VELOCITY
            //rb.velocity = direction * ballSpeed * Time.deltaTime;
        }
        //else
        //rb.velocity = Vector2.zero;
    }

    //Cunado estamos en una pared, seleccionamos la nueva direccion de movimento
    private void GetNewDirection()
    {

        if (touchingWall && !arrowPoint.GetComponent<DirectionArrow>().getIsColliding())
        {
            onJump?.Invoke();

            YSpeed = ballSpeed;

            direction = arrowPoint.transform.position - transform.position;
            touchingWall = false;
            arrowPoint.GetComponent<DirectionArrow>().setArrowLine(touchingWall);
            IncrementBallSpeed();

            if (touchingMovingWall) {
                touchingMovingWall = false;
                transform.SetParent(null);
            }
        }
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

        Debug.Log("Ball Colliding Wall"); //CHOQUEM AMB UNA PARET NORMAL
        touchingWall = true;
        arrowPoint.GetComponent<DirectionArrow>().setArrowLine(touchingWall);

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
    }

    private void OnDisable()
    {
        PlayerInputs.selectDirection -= GetNewDirection;
    }
}