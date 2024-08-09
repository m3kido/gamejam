using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class enemieMovments : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patroleDestination;
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float Distance;


    // Update is called once per frame
    void Update()
    {
        Distance = math.abs(Vector2.Distance(transform.position, playerTransform.position));
        if (isChasing)
        {
            //if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance*1.5)
            //{
            //    isChasing = false;
            //    return;
            //}
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime * 300;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);

                transform.position += Vector3.right * moveSpeed * Time.deltaTime * 300;
            }
            if (transform.position.x - playerTransform.position.x < 0.2f)
            {
                isChasing = false;
            }

        }
        else
        {
            if ( Vector2.Distance(transform.position , playerTransform.position) < chaseDistance )
            {
                isChasing = true;
            }  
            if (patroleDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    
                    patroleDestination = 1;
                    //transform.localScale = new Vector3(-1, 1, 1);
                }
            }

            if (patroleDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    
                    patroleDestination = 0;
                    //transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
