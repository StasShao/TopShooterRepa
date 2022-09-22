using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    protected GroundCheckerNS groundCheckerNS;
    [SerializeField]protected Transform _rayStartPoint;
    protected GameObject _thisGo;
    [SerializeField][Range(0.0f,10.0f)] protected float _rayDist;


    private void Start()
    {
        groundCheckerNS = new GroundCheckerNS(_rayStartPoint,gameObject,_rayDist);
    }

   
    private void FixedUpdate()
    {
        groundCheckerNS.FixTick();
    }
}
