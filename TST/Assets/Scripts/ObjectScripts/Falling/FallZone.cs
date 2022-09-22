using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    private float timer;
    private void OnFalling()
    {
        if (!DataBase.IsPlayerGrounded)
        {
            timer += 0.2f * Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                DataBase.PlayerData.SetActive(false);
            }
        }
      
        if (!DataBase.IsEnemyGrounded)
        {
            timer += 0.2f * Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                DataBase.EnemyData.SetActive(false);
            }
        }
      
    }
    private void Update()
    {
        OnFalling();
    }
}
