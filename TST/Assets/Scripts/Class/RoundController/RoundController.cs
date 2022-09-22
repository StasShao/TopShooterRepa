using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    protected RoundControllerNS roundControllerNS;
    [SerializeField] protected Text RoundTimer;
    [SerializeField] protected GameObject FightAnimation;
    private int _convertTimer;
    private void ShowRoundTimer()
    {
        if(DataBase.RoundTimerData > 0 && DataBase.RoundTimerData < 0.5f)
        {
            _convertTimer = 3;
        }
        if (DataBase.RoundTimerData > 0.5f && DataBase.RoundTimerData < 1.0f)
        {
            _convertTimer = 2;
        }
        if (DataBase.RoundTimerData > 1.0f && DataBase.RoundTimerData < 1.7f)
        {
            _convertTimer = 1;
        }
        if (DataBase.RoundTimerData > 1.7f && DataBase.RoundTimerData < 2.0f)
        {
            _convertTimer = 0;
            FightAnimation.SetActive(true);
        }
        if (!DataBase.EnemyData.activeInHierarchy || !DataBase.PlayerData.activeInHierarchy)
        {
            RoundTimer.gameObject.SetActive(true);
            RoundTimer.text = _convertTimer.ToString();
        }
        if(DataBase.EnemyData.activeInHierarchy && DataBase.PlayerData.activeInHierarchy)
        {
            RoundTimer.gameObject.SetActive(false);
            FightAnimation.SetActive(false);
        }
    }

    void Start()
    {
        roundControllerNS = new RoundControllerNS();
    }

    
    void Update()
    {
        roundControllerNS.Tick();
        ShowRoundTimer();
    }
}
