using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour,IDamagable
{
    private PlayerAnimator animator;
    public float health;
    public bool Dead = false;
    private UImanager um;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<PlayerAnimator>();
        health = 100f;
        um= FindObjectOfType<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage)
    {
        health -= damage;
        um.TakeDamage();
        if (health < 0)
        {
            animator.HandlePlayerDeath();
            Dead = true;
            StartCoroutine(RestartSceneAfterDelay());
        }
        else
        {
            animator.HandlePlayerHurt();
           
        }

    }
    private IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);  // Wait for the specified delay
        Scene currentScene = SceneManager.GetActiveScene();  // Get the current scene
        SceneManager.LoadScene(currentScene.name);  // Reload the current scene
    }


}
