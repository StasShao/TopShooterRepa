using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPrefabScript : MonoBehaviour
{
    public Rigidbody _rb;
    
    private void OnTriggerEnter()
    {
        gameObject.SetActive(false);
    }
    private void RocketAxelerate()
    {
        if (gameObject.activeInHierarchy)
        {
            if (_rb != null)
            {
                if ( _rb.velocity.z < 1.0f || _rb.velocity.z > -1.0f)
                {
                    _rb.AddForce(_rb.transform.forward * 100.0f, ForceMode.Force);
                }
            }
        }
    }
    private void BlowTimerActivity()
    {
        if (gameObject.activeInHierarchy)
        {
            DataBase.BlowLifeTimer();

        }
    }
    private void Update()
    {
        RocketAxelerate();
        BlowTimerActivity();


    }

}
