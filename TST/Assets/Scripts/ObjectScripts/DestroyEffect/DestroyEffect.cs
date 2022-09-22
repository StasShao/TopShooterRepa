using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] protected Rigidbody[] rbs;
    private float timer;
    private bool isActive;
    [SerializeField] protected MeshRenderer[] ColorRanderer;
    [SerializeField] protected Material PlayerMaterial;
    [SerializeField] protected Material EnemyMaterial;
    private void Awake()
    {
        DataBase.DestroyEffectData = gameObject;
        DataBase.PlayerMatData = PlayerMaterial;
        DataBase.EnemyMatData = EnemyMaterial;
        DataBase.DestroyEffectMeshRenderData = ColorRanderer;

    }
    void Update()
    {
        if(isActive)
        {
            if (gameObject.activeInHierarchy)
            {
                isActive = false;
                foreach (var item in rbs)
                {
                    item.transform.localPosition = Vector3.zero;
                    item.AddForce(item.transform.up * 25.0f, ForceMode.Impulse);
                }

            }
        }
        if(!isActive)
        {
            timer += 0.2f * Time.deltaTime;
            if(timer >= 1.5f)
            {
                timer = 0.0f;
                isActive = true;
                gameObject.SetActive(false);
            }
        }
       
    }
}
