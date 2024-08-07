using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
        else
        {

        }

    }
    private void Die() { 
        
    }
}
