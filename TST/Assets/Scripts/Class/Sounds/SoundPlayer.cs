using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    protected SoundPlayerNS soundPlayerNS;
    [SerializeField]protected AudioSource _aSource;
    [SerializeField]protected AudioClip[] _aclips;
    private void Awake()
    {
      
    }

    void Start()
    {
        soundPlayerNS = new SoundPlayerNS(_aSource, _aclips);
    }

   
    void Update()
    {
        soundPlayerNS.Tick();
    }
}
