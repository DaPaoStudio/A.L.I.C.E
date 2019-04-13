using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Pool(GameObject prefab)
    {
        this.thisprefab = prefab;
    }
    private GameObject thisprefab;
    private List<GameObject> prefabs = new List<GameObject>();

    public string name
    {
        get
        {
            return thisprefab.name;
        }
    }

    public GameObject getobj()
    {
        GameObject go = null;
        foreach(var obj in prefabs)
        {
            if(obj.activeInHierarchy)
            {
                go = obj;
                break;
            }          
        }
        if (go == null)
        {
            go = GameObject.Instantiate(thisprefab);
            prefabs.Add(go);
        }
        go.SetActive(true);
        return go;
    }
    public void distroyobj(GameObject go)
    {
        if (prefabs.Contains(go))
            go.SetActive(false);
    }
    public void distroyall()
    {
        foreach (var go in prefabs)
            if (go.activeInHierarchy)
                go.SetActive(false);
    }

    public bool contains(GameObject go)
    {
        return prefabs.Contains(go);
    }
}
