using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowing : MonoBehaviour, IBlow
{
    protected HitCollisionsNS hitCollissionNS;

    public bool ISBLOW { get; protected set; }

    public GameObject BLOW(GameObject blowEffect, Vector3 position, bool isBlow)
    {
        ISBLOW = isBlow;
        blowEffect = DataBase.BlowGoData;
        blowEffect.transform.position = position;
        return blowEffect;
    }

    void Start()
    {
        hitCollissionNS = new HitCollisionsNS(this);
    }
    private void Update()
    {
       
    }
    private void OnCollisionEnter(Collision col)
    {
        hitCollissionNS.OnCollisionEnter(col);
    }

    public void BlowEnd(bool isBlow)
    {
        ISBLOW = isBlow;
    }
}
