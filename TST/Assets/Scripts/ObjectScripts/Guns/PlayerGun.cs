using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : ObjetcPool, IGun
{
    private GameObject bulletGameObject;
    [SerializeField] private GameObject FireEffect;
    [SerializeField] [Range(10.0f, 1000.0f)] private float ShootForce;
    [SerializeField] private Transform BulletSpawn;
    private float timer;

    public bool ISFIRE { get; private set; }


    protected override void YourStart()
    {
        DataBase.IGunData = GetComponent<IGun>();
    }

    protected override void YourTick()
    {
        FireEffectLifeTimer();
        if (ISFIRE)
        {
            Shoot();
        }
    }
    protected void Shoot()
    {
        FireEffect.SetActive(true);
        rb = poolObjNS.RbPool();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.transform.position = BulletSpawn.position;
        rb.transform.rotation = BulletSpawn.rotation;
        rb.AddForce(rb.transform.forward * ShootForce, ForceMode.Impulse);
    }
    private void FireEffectLifeTimer()
    {
        if(FireEffect.activeInHierarchy)
        {
            timer += 0.2f * Time.deltaTime;
            if(timer >= 0.05f)
            {
                timer = 0.0f;
                FireEffect.SetActive(false);
            }
        }
    }
    public void Fire(bool isFire)
    {
        ISFIRE = isFire;
    }

  
}
