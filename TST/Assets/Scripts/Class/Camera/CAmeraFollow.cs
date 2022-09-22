using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CAmeraFollow : MonoBehaviour
{
    [SerializeField] protected Transform PivotFolowToTarget;
    [SerializeField] protected Transform CameraTransform;
    [SerializeField] protected Transform Target;
    [SerializeField] protected Vector3 Offset;
    [SerializeField][Range(0.0f,100.0f)] protected float MoveCoficient;
    [SerializeField] protected bool IsCamFollowByDefoult;
    [SerializeField] protected bool IsTopView;
    protected abstract void YourLateTick();
    protected abstract void YourTick();
    protected abstract void YourStart();
    private void FollowByDefoult()
    {
        if(IsCamFollowByDefoult)
        {
            PivotFolowToTarget.position = Vector3.Slerp(PivotFolowToTarget.position, Target.position, MoveCoficient);
            CameraTransform.position = Vector3.Slerp(CameraTransform.position, PivotFolowToTarget.position, MoveCoficient) + Offset;
            CameraTransform.LookAt(Target.position);
        }
        if(IsTopView)
        {
            CameraTransform.position = Vector3.Slerp(CameraTransform.position, Target.position, 2) + new Vector3(0,60.0f,0);
            CameraTransform.LookAt(Target.position);
        }
    }
    private void OnValidate()
    {
        if(IsCamFollowByDefoult)
        {
            IsTopView = false;
        }
        if(IsTopView)
        {
            IsCamFollowByDefoult = false;
        }
    }

    private void Start()
    {
        YourStart();
    }

    
    private void LateUpdate()
    {
        YourLateTick();
        FollowByDefoult();
    }
    private void Update()
    {
        YourTick();
    }
}
