using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolcontrol : MonoBehaviour
{
    private static Poolcontrol poolcontrol;
    public static Poolcontrol MyPool
    {
        get
        {
            if (poolcontrol == null)
                poolcontrol = new Poolcontrol();
            return poolcontrol;
        }
    }

    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    public GameObject getobj(string prefabname)
    {
        if(!pools.ContainsKey(prefabname))
        {
            GameObject prefab = Resources.Load(prefabname) as GameObject;
            Pool newpool = new Pool(prefab);
            pools.Add(prefab.name, newpool);
        }
        return pools[prefabname].getobj();
    }

    public void distroyobj(GameObject go)
    {
        foreach(Pool pool in pools.Values)
        {
            if(pool.contains(go))
            {
                pool.distroyobj(go);
                break;
            }
        }
    }

    public void distroyall()
    {
        foreach (Pool pool in pools.Values)
            pool.distroyall();
    }
}
