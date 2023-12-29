using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private float ballSpeed = 5f;
    [SerializeField] private float speedIncrementation = 2f;
    private bool touchingWall = true;
    private Vector2 direction;

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
            transform.position += new Vector3(direction.x, direction.y, 0f) * ballSpeed * Time.deltaTime;

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
            direction = arrowPoint.transform.position - transform.position;
            touchingWall = false;
            arrowPoint.GetComponent<DirectionArrow>().setArrowLine(touchingWall);
            IncrementBallSpeed();

            onJump?.Invoke();
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
        Debug.Log("Ball Colliding Wall");
        touchingWall = true;
        arrowPoint.GetComponent<DirectionArrow>().setArrowLine(touchingWall);

        onWall?.Invoke();
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