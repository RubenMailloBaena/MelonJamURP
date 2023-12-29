using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5;
    public Transform a, b;
    private Vector3 aPosition, bPosition;
    public bool aDone = false;

    void Start()
    {
        aPosition = a.position;
        bPosition = b.position;
    }

    void Update()
    {
       if(!aDone)
        {
            transform.position = Vector2.MoveTowards(transform.position, aPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, bPosition, speed * Time.deltaTime);
        }
        
       if(transform.position == aPosition)
        {
            aDone = true;
        }
       else if(transform.position == bPosition)
        {
            aDone = false;
        }
    }
}
