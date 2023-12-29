using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float distanceToSource = 2f;
    [SerializeField] private float speedIncrementation = 2f;
    [SerializeField] private float maxRotationSpeed = 520f;
    [SerializeField] private float obstacleCheckDistance = 1f;
    [SerializeField] private LayerMask wallLayer;
    private bool isColliding = false;
    private Vector3 sceneCenter = new Vector3 (0f,0f,0f);

    [Header("Other Components")]
    [SerializeField] private GameObject orbitPoint; //Punto del que rotar
    [SerializeField] private GameObject arrowSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Update()
    {
        ArrowMovement();
        //SetArrowDirectionToCenter();
    }

    //Orbita sobre el objeto de la bola, para indicar la direccion
    private void ArrowMovement() {
        transform.RotateAround(orbitPoint.transform.position, new Vector3(0,0,1), rotationSpeed * Time.deltaTime);
        arrowSprite.transform.position = transform.position;

        //sprite rotation
        Vector3 dirToTarget = orbitPoint.transform.position - arrowSprite.transform.position;
        dirToTarget.z = 0f;  

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dirToTarget);
        arrowSprite.transform.rotation = lookRotation;
        arrowSprite.transform.Rotate(Vector3.forward, 180f);
    }



    private void SetArrowDirectionToCenter()
    {
        Vector3 screenCenterDirection = sceneCenter - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(orbitPoint.transform.position, screenCenterDirection, obstacleCheckDistance, wallLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(orbitPoint.transform.position, screenCenterDirection, obstacleCheckDistance/2, wallLayer);
        Debug.DrawRay(orbitPoint.transform.position, screenCenterDirection, Color.red);
        Debug.DrawRay(orbitPoint.transform.position, screenCenterDirection, Color.blue);

        if (hit.collider != null)
        {
            Debug.Log("HITTING WALL");
            Vector3 newDirection = Vector3.Reflect(screenCenterDirection.normalized, hit.normal);
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, newDirection);
            transform.rotation = newRotation;
            transform.position = orbitPoint.transform.position + newDirection * distanceToSource;
        }
        else if (hit2.collider != null) 
        {
            Debug.Log("HITTING WALL");
            Vector3 newDirection = Vector3.Reflect(screenCenterDirection.normalized, hit2.normal);
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, newDirection);
            transform.rotation = newRotation;
            transform.position = orbitPoint.transform.position + newDirection * distanceToSource;
        }
        else
        {
            //APUNTAR AL CENTRO
            Vector3 planeNormal = Vector3.forward;
            Vector3 projectedSrcPos = Vector3.ProjectOnPlane(orbitPoint.transform.position, planeNormal);
            Vector3 projectedTargetPos = Vector3.ProjectOnPlane(sceneCenter, planeNormal);

            Vector3 pointerForward = (projectedTargetPos - projectedSrcPos).normalized;
            Quaternion pointerRotation = Quaternion.LookRotation(pointerForward, planeNormal);

            transform.rotation = pointerRotation;
            transform.position = orbitPoint.transform.position + pointerForward * distanceToSource;
        }
    }





    //Si choca con una pared, este cambia el sentido de la orbita
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("MovingWall") || collision.gameObject.tag.Equals("Roof")) {
            rotationSpeed = rotationSpeed * -1;
            //isColliding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("MovingWall") || collision.gameObject.tag.Equals("Roof"))
        {
            //isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("MovingWall") || collision.gameObject.tag.Equals("Roof"))
        {
            isColliding = false;
        }
    }


    public void setArrowLine(bool setActive) {
        spriteRenderer.enabled = setActive;

        if (setActive)
            SetArrowDirectionToCenter();
    }

    public void IncrementArrowSpeed() {
        if (Mathf.Abs(rotationSpeed) >= maxRotationSpeed)
        {
            rotationSpeed = maxRotationSpeed;
        }
        else 
        {
            if (rotationSpeed < 0)
                rotationSpeed -= speedIncrementation;
            else
                rotationSpeed += speedIncrementation;
        }
    }


    public bool getIsColliding() {
        return isColliding;
    }
}
