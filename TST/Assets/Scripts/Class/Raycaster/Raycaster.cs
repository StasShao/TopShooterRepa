using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Raycaster : MonoBehaviour
{
    [SerializeField]protected Transform RayStartPosition;
    [SerializeField] protected float RayDistance;
    [SerializeField] protected Color RayColor;
    [SerializeField] protected LayerMask RayMask;
    private bool IsOn;

    protected abstract void YourStart();
    protected abstract void YourTick();
    protected void RayCasting(bool isOn)
    {
        if(isOn)
        {
            Ray ray = new Ray(RayStartPosition.position, RayStartPosition.up * RayDistance);
            Debug.DrawRay(ray.origin, ray.direction * RayDistance, RayColor);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Enemy")
                {
                    DataBase.IswipData.Swip(true);
                    IsOn = false;
                }
            }
        }
      
    }
    
    void Start()
    {
        YourStart();
        IsOn = true;
    }
    private void FixedUpdate()
    {
        RayCasting(IsOn);
    }

    void Update()
    {
        YourTick();
        if(!gameObject.activeInHierarchy)
        {
            IsOn = true;
        }
    }
}
