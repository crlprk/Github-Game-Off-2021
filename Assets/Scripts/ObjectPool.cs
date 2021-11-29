using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolableItem
{
    public string name;
    public int count;
    public GameObject item;
    public bool isLimited;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public List<PoolableItem> itemList;
    public Dictionary<string, List<GameObject>> pool;
    public Dictionary<string, PoolableItem> db;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        pool = new Dictionary<string, List<GameObject>>();
        db = new Dictionary<string, PoolableItem>();
        for (int i = 0; i < itemList.Count; i++)
        {
            List<GameObject> tempPool = new List<GameObject>();
            for (int j = 0; j < itemList[i].count; j++)
            {
                GameObject temp = Instantiate<GameObject>(itemList[i].item);
                temp.SetActive(false);
                tempPool.Add(temp);
            }
            pool.Add(itemList[i].name, tempPool);
            db.Add(itemList[i].name, itemList[i]);
        }
    }

    public GameObject GetObject(string tag) {
        for (int i = 0; i < pool[tag].Count; i++)
        {
            if (!pool[tag][i].activeInHierarchy)
            {
                return pool[tag][i];
            }
        }
        if (!db[tag].isLimited)
        {
            GameObject temp = Instantiate<GameObject>(db[tag].item);
            temp.SetActive(false);
            pool[tag].Add(temp);
            return temp;
        }
        
        return null;
    }
}
