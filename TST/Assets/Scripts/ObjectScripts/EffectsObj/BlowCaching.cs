using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowCaching : MonoBehaviour
{
   
    void Awake()
    {
        DataBase.BlowGoData = gameObject;
        gameObject.SetActive(false);
    }

    
}
