

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UIElements;


public class EnemyAimWeapon : MonoBehaviour
{
    private float timer = 0;
    private float cooldown = 1.2f;
    Player player;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;

    }


    [SerializeField] private GameObject aimTransform;
    [SerializeField] private GameObject BulletParent;
    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private GameObject aimGunEndPointTransform;
    [SerializeField] private Animator aimAnimator;
    [SerializeField] private Material tracerMaterial;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip shoot;

    Enemy enemy;
 


    private void Awake()
    {
        player = FindObjectOfType<Player>();
       
        enemy=GetComponent<Enemy>();

    }

    private void Update()
    {
        if (enemy.Dead) { return; }
        HandleAiming(player.transform.position);
        if (PlayerVisible())
        {
            print("damn");
            timer -= Time.deltaTime;
        }
        else
        {
            timer = cooldown;
        }
        HandleShooting(player.transform.position);
        
    }

    private void HandleAiming( Vector3 vec)
    {

        
        Vector3 aimDirection = (vec - aimTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        aimTransform.transform.localScale = aimLocalScale;


    }
    private bool PlayerVisible()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 12)
        {
            return true;
        }
        return false;
    }
    
    private void HandleShooting(Vector3 vec)
    {



        if (timer <= 0)
        {
            timer = cooldown;
            aimAnimator.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.transform.position,
                shootPosition = vec,

            });

            Trace(aimGunEndPointTransform.transform.position, vec);
            Bullet newBullet = Instantiate(BulletPrefab, aimGunEndPointTransform.transform.position, Quaternion.identity).GetComponent<Bullet>();
            newBullet.Setup((vec - aimTransform.transform.position).normalized);

            source.clip = shoot;
            source.Play();
        }
    }
   
    public void Trace(Vector3 fromPosition, Vector3 targetPosition)
    {

        Vector3 shootDir = (targetPosition - fromPosition).normalized;
        float distance = Vector3.Distance(fromPosition, targetPosition);
        float shootAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        Vector3 spawnTracerPosition = fromPosition + shootDir * distance * .5f;


        tracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 256f));

        World_Mesh worldMesh = World_Mesh.Create(spawnTracerPosition, shootAngle - 90, 1f, distance, tracerMaterial, null, 10000);

        int frame = 0;
        int frameBase = 0;

        worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame + 64 * frameBase, 0, 16, 256));
        float framerate = .016f;
        float timer = framerate;

        FunctionUpdater.Create(delegate {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer += framerate;
                frame++;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                worldMesh.AddPosition(shootDir * 2f);
                worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame + 64 * frameBase, 0, 16, 256));
            }
            return false;
        });

    }



}
