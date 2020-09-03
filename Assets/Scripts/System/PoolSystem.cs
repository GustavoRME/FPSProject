using System.Collections.Generic;
using UnityEngine;

public static class PoolSystem
{
    private static readonly Dictionary<Object, Queue<Object>> _pools = new Dictionary<Object, Queue<Object>>();

    public static void InitiliazePool(Object prefab, Transform parent, int size)
    {
        if (_pools.ContainsKey(prefab))
            return;

        Queue<Object> queue = new Queue<Object>();

        for (int i = 0; i < size; i++)
        {
            var obj = Object.Instantiate(prefab, parent);

            DisableObject(obj);
            
            queue.Enqueue(obj);
        }

        _pools[prefab] = queue;
    }
    public static void InstantiatePool(Object key)
    {

    }
    public static T GetInstance<T>(Object key) where T: Object
    {
        if(_pools.TryGetValue(key, out Queue<Object> queue))
        {
            Object obj;

            if(queue.Count > 0)
            {
                obj = queue.Dequeue();                             
            }
            else
            {
                obj = Object.Instantiate(key);
            }
            
            queue.Enqueue(obj);            

            return obj as T;
        }

        return null;
    }

    private static void DisableObject(Object obj)
    {
        GameObject go;

        if(obj is Component component)
        {
            go = component.gameObject;
        }
        else
        {
            go = obj as GameObject;
        }

        go.SetActive(false);
    }
}
