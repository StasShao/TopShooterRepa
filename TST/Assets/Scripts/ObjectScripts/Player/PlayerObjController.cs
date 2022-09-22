using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjController : PlayerController, IplayerController
{
    [SerializeField] private Transform PlayerBodyTransform;
    [SerializeField] private Rigidbody PlayerRigidBody;
    [SerializeField] [Range(0.0f, 1000.0f)] private float MoveForece;
    [SerializeField][Range(0.0f,1000.0f)] private float RotateForce;
    public float ISMOVE { get; private set; }

    public float ISTURN { get; private set; }

    public void Move(float isMove)
    {
        ISMOVE = isMove;
    }

    public void Turn(float isTurn)
    {
        ISTURN = isTurn;
    }

    protected override void YourFixedTick()
    {
        PlayerRigidBody.AddForce(PlayerRigidBody.transform.forward * ISMOVE * MoveForece, ForceMode.Force);
    }

    protected override void YourStart()
    {
        DataBase.PlayerData = gameObject;
        DataBase.IplayerControllerData = GetComponent<IplayerController>();
    }

    protected override void YourTick()
    {
        PlayerBodyTransform.Rotate(0, ISTURN * RotateForce, 0);
        
    }
}
