using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeMovements : MonoBehaviour
{

    public float moveSpeed;
    public GameObject[] wayPoints;
    public int nextPoint;
    float disToPoint; 
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        disToPoint = Vector2.Distance(transform.position , wayPoints[nextPoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].transform.position, moveSpeed * Time.deltaTime);
        if(disToPoint < .2f )
        {
            TakeTurn(); 
        }
    }
    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextPoint].transform.eulerAngles.z; 
        transform.eulerAngles = currRot;
        chooseNextPoint();
    }
    void chooseNextPoint()
    {
        nextPoint ++;
        if ( nextPoint == wayPoints.Length )
        {
            nextPoint = 0;
        }
        
    }
}
