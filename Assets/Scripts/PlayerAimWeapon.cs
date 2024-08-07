
 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAimWeapon : MonoBehaviour {

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;
    }

    
   [SerializeField] private GameObject aimTransform;
  
   
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    [SerializeField] private Animator aimAnimator;

    private void Awake() {


        
       // aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        //aimShellPositionTransform = aimTransform.Find("ShellPosition");
    }

    private void Update() {
        HandleAiming();
        HandleShooting();
       
    }

    private void HandleAiming() {

        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(vec);
        Vector3 aimDirection = (vec -  aimTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.transform.eulerAngles = new Vector3(0, 0, angle);
        
        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            aimLocalScale.y = -1f;
        } else {
            aimLocalScale.y = +1f;
        }
        aimTransform.transform.localScale = aimLocalScale;

        
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimAnimator.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs { 
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position,
            });
        }
    }
    

}
