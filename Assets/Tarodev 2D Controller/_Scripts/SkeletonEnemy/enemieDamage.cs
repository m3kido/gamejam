using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class enemieDamage : MonoBehaviour
{
    [SerializeField] int damage;
    public PlayerHealth health;
    //public Animator animator;
    public Transform playerTransform;
    public float Distance; 

    private void Update()
    {
        Distance = math.abs(Vector2.Distance(transform.position, playerTransform.position));
        //animator.SetFloat("attack", Distance); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        { 
            health.takeDamage(damage);
        }
    }
}
