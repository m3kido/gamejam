using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour, IDamagable
{
    private PlayerAnimator animator;
    public float health;
    public bool Dead = false;
    private bool right = true;
    private float timer = 2f;
    private float bascule = 2f;
    // Start is called before the first frame update
    private AudioSource source;


   


    [SerializeField] AudioClip hurt;
    void Start()
    {
        animator = GetComponentInChildren<PlayerAnimator>();
        health = 100f;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead) {  return; }  
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            right = !right;
            timer = bascule;
        }
        if (right)
        {
            transform.position += Vector3.right * 2 * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right * 2 * Time.deltaTime;
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        source.clip = hurt;
        source.Play();
        if (health < 0)
        {
            animator.HandlePlayerDeath();
            Dead = true;
            Destroy(gameObject, 2f);

        }
        else
        {
            animator.HandlePlayerHurt();

        }

    }



    


}
