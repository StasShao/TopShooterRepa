using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, Iswip
{
    protected AINS aiNS;
    [SerializeField][Range(0.0f,1000.0f)] protected float MoveForce;
    [SerializeField]protected Transform pivot;
    [SerializeField] protected Transform pointer;
    [SerializeField] protected Transform ricochetPointer;
    [SerializeField] protected Transform HeadPivot;
    [SerializeField] protected Transform raySartPoint;
    [SerializeField] protected float rayDistance;
    [SerializeField] protected Color rayColor;
    [SerializeField] protected LayerMask rayLayer;
    [SerializeField] protected Animator searcherAnim;
    [SerializeField] protected NavMeshAgent navMeshA;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float Offset;
    [SerializeField] protected float ANGLE;
    [SerializeField] protected Transform RandomPointer;

    public bool ISSWIP { get; protected set; }

    private void Awake()
    {
        DataBase.EnemyData = gameObject;
        DataBase.IswipData = GetComponent<Iswip>();
    }

    void Start()
    {
        
        aiNS = new AINS(this,pivot, raySartPoint, rayDistance, rayColor, rayLayer, searcherAnim, navMeshA, rb, HeadPivot, pointer, ricochetPointer, Offset, RandomPointer, MoveForce);
    }

    
    void Update()
    {
        aiNS.Tick();
    }
    private void FixedUpdate()
    {
        aiNS.FixedTick();
    }
    public float ShowAngle(float angele)
    {
        ANGLE = angele;
        return angele;
    }

    public void Swip(bool isSwip)
    {
        ISSWIP = isSwip;
    }
}
