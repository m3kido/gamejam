using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{

    private Vector3 vec;

    public void Setup(Vector3 vec)
    {
        this.vec = vec;
        transform.eulerAngles =new Vector3(0,0, Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
        Destroy(gameObject,1f);
    }
    void Update()
    {
        transform.position += vec *500f*Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamagable target = collider.GetComponent<IDamagable>();
        if(target != null)
        {
            target.Damage(40f);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
