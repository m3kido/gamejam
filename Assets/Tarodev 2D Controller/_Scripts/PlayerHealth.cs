using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int health ;
    void Start()
    {
        health = maxHealth ;
    }

    // Update is called once per frame
    public void takeDamage(int damage)
    {
        health -=  damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
