using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PointsCalculator : MonoBehaviour
{
    [SerializeField] protected Text PlayerPoints;
    [SerializeField] protected Text EnemyPoints;
    private int curentPlayerPoints;
    private int curentEnemyPoints;


    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerPoints"))
        {
            DataBase.PointsData = PlayerPrefs.GetInt("PlayerPoints");
        }
        if (PlayerPrefs.HasKey("PlayerPoints"))
        {
            DataBase.EnemyPointsData = PlayerPrefs.GetInt("EnemyPoints");
        }
    }
    private void ShowPoints()
    {
        if(curentPlayerPoints < DataBase.PointsData)
        {
            curentPlayerPoints = DataBase.PointsData;
            PlayerPrefs.SetInt("PlayerPoints", DataBase.PointsData);
        }
        if (curentEnemyPoints < DataBase.EnemyPointsData)
        {
            curentEnemyPoints = DataBase.EnemyPointsData;
            PlayerPrefs.SetInt("EnemyPoints", DataBase.EnemyPointsData);
        }
        PlayerPoints.text = DataBase.PointsData.ToString();
        EnemyPoints.text = DataBase.EnemyPointsData.ToString();
    }

    
    void Update()
    {
        ShowPoints();
    }
}
