using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected PlayerControllerNS playerControllerNS;

    void Awake()
    {
        playerControllerNS = new PlayerControllerNS();
        YourStart();
    }
   
   
    void Update()
    {
        playerControllerNS.Tick();
        YourTick();
    }
    private void FixedUpdate()
    {
        YourFixedTick();
    }
    protected abstract void YourFixedTick();
    protected abstract void YourTick();
    protected abstract void YourStart();
}
