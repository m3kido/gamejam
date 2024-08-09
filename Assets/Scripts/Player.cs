using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour,IDamagable
{
    private PlayerAnimator animator;
    public float health;
    public bool Dead = false;
    private UImanager um;
    // Start is called before the first frame update
    private AudioSource source;

    // Teleport variables
    private float radius = 0.5f;
    private float animationDelay = 0.2f;
    private bool canTeleport = true;
    private int teleportCount = 0;
    private int maxTeleports = 2;
    private float cooldownDurationTeleport = 10f;
    

    // Slow motion variables
    private bool canUseSlowMotion = true;
    private float slowMotionDuration = 5f;
    private float cooldownDurationSlowMotion = 10f;



    [SerializeField] AudioClip hurt ;
    void Start()
    {
        animator = GetComponentInChildren<PlayerAnimator>();
        health = 100f;
        um= FindObjectOfType<UImanager>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20 && !Dead) { 
            animator.HandlePlayerDeath();
            Dead = true;
            StartCoroutine(RestartSceneAfterDelay());
        }
    }

    /*************************************************************************************************************************************/
    public void Damage(float damage)
    {
        if (Dead) { return; }
        health -= damage;
        um.TakeDamage();
        source.clip = hurt;
        source.Play();
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



    /*************************************************************************************************************************************/

    public void SlowMotion()
    {
        if (canUseSlowMotion)
        {
            StartCoroutine(SlowMotionRoutine());
        }
        
    }

    [SerializeField] GameObject clock;
    [SerializeField] GameObject clockFill;

    private IEnumerator SlowMotionRoutine()
    {
        canUseSlowMotion = false;

        Time.timeScale = 0.2f;

        float progress = 1f;
        float totalDuration = slowMotionDuration;

        float elapsed = 0f;

        while (elapsed < totalDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            progress = 1-Mathf.Clamp01(elapsed / totalDuration);

            clock.GetComponent<Image>().fillAmount = progress;
            clockFill.GetComponent<Image>().fillAmount = progress;
            
            yield return null;
            
        }

        progress = 0f;
        clock.GetComponent<Image>().fillAmount = progress;
        clockFill.GetComponent<Image>().fillAmount = progress;

        Time.timeScale = 1f;





        totalDuration = cooldownDurationSlowMotion;

        elapsed = 0f;

        while (elapsed < totalDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            progress = Mathf.Clamp01(elapsed / totalDuration);

            clock.GetComponent<Image>().fillAmount = progress;
            clockFill.GetComponent<Image>().fillAmount = progress;
            
            yield return null;
            
        }

        progress = 1f;
        clock.GetComponent<Image>().fillAmount = progress;
        clockFill.GetComponent<Image>().fillAmount = progress;

        canUseSlowMotion = true;
        
    }


    /*************************************************************************************************************************************/

    [SerializeField] GameObject portal;
    [SerializeField] GameObject portalFill;
    public void Teleport()
    {
        if (canTeleport && teleportCount < maxTeleports)
        {
            Time.timeScale = 0.1f;

            animator.HandleTeleport();

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            teleportCount++;
            StartCoroutine(TeleportAfterAnimation(targetPosition));

            Time.timeScale = 1f;

            
            if (teleportCount >= maxTeleports)
            {
                StartCoroutine(TeleportCooldown());
            }
        }
    }
    private IEnumerator TeleportAfterAnimation(Vector3 targetPosition)
    {
        float progress = 1f - (float)teleportCount / maxTeleports;  
        Debug.Log(progress);
        portal.GetComponent<Image>().fillAmount = progress;
        portalFill.GetComponent<Image>().fillAmount = progress;
        
        yield return new WaitForSeconds(animationDelay);

        // Check for collisions
        Collider2D hitCollider = Physics2D.OverlapCircle(targetPosition, radius);

        // If no collision, teleport
        if (hitCollider == null)
        {
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }

        // Update the portal fill amount
        
    }
    private IEnumerator TeleportCooldown()
    {   
        float progress = 0f;
        float totalDuration = cooldownDurationTeleport;
        canTeleport = false;

        float elapsed = 0f;

        while (elapsed < totalDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            progress = Mathf.Clamp01(elapsed / totalDuration);

            portal.GetComponent<Image>().fillAmount = progress;
            portalFill.GetComponent<Image>().fillAmount = progress;
            
            yield return null;
            
        }

        progress = 1f;
        portal.GetComponent<Image>().fillAmount = progress;
        portalFill.GetComponent<Image>().fillAmount = progress;

        

        teleportCount = 0;
        canTeleport = true;
    }

    /*************************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Finish"))
        {
            SceneManager.LoadScene(0);

        }
    }




}
