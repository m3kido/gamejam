using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] private GameObject[] Hearts;
    [SerializeField] private GameObject[] Bullets;

    private int heart = 3;
    private int bullets = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotFired()
    {
        if(bullets != 0)
        {
            bullets--;
            Bullets[bullets].gameObject.SetActive(false) ;
        }
    }
    public void TakeDamage()
    {
        if (heart != 0)
        {
            heart--;
            Hearts[heart].gameObject.SetActive(false);
        }
    }
    public void ResetBuletts()
    {
        bullets = 7;
        foreach (GameObject go in Bullets)
        {
            go.SetActive(true) ;
        }
    }
}
