using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PoolItem{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    public static Pool singleton;
    public List<PoolItem> items; // we have 2 , bulletPrefab & astereoid
    public List<GameObject> pooledItems;
    
    private void Awake() 
    {
        singleton = this; // so we cna use it later on -singleton inst.
    }

    public GameObject Get(string tag)
    {
        for (int i=0; i<pooledItems.Count; i++)
        {
            if(!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                return pooledItems[i];
            }
        }

        foreach(PoolItem item in items) //create new item if we are out of em in pool
        {
            if(item.prefab.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        pooledItems = new List<GameObject>();
        foreach(PoolItem item in items) //loop thro items to create for pool
        {
            for(int i=0; i<item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
