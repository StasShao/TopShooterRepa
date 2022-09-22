using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetcPool : MonoBehaviour
{
    protected PoolObjectNS poolObjNS;
    protected Rigidbody rb;
    [SerializeField]protected List<PoolPrefabScript> prefabList;
    [SerializeField]protected PoolPrefabScript prefab;
    [SerializeField]protected Transform container;
    [SerializeField] [Range(1, 100)] protected int PoolCount;
    [SerializeField] [Range(10, 100)] protected int MaxPoolCount;
    [SerializeField] protected bool autoExpand;
   
    
    private void Awake()
    {
        poolObjNS = new PoolObjectNS(this, prefab, prefabList, PoolCount, MaxPoolCount, autoExpand);
        YourStart();
    }
    private void Update()
    {
        YourTick();
    }
    public PoolPrefabScript CreatePool(PoolPrefabScript pref)
    {
        var createdObj = Instantiate(prefab, container);
        prefabList.Add(createdObj);
        createdObj.gameObject.SetActive(false);
        pref = createdObj;
        return pref;
    }
       
    protected abstract void YourTick();
    protected abstract void YourStart();
  
}
