using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Create a class that we can configure to add a pool of objects
    // We can control the tag, what Prefab, and how many objects in each pool
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;  // List of all of our pools
    Dictionary<string, Queue<GameObject>> poolDictionary;  // This is the actual dictionary where the 
    // pools will live

    void Start()
    {
        // Have to instantiate the poolDictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Loop through the list of pools
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // For each "slot" in the pool
            for (int i = 0; i < pool.size; i++)
            {
                // Instantiate a game object and immediately set it to false so that it is not active just yet
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                // Add the inactive object to the respective objectPool 
                objectPool.Enqueue(obj);
            }

            // Add the tag and the associated pool to the dictionary
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exist.");
            return null;
        }

        // use the tag to find the appropriate pool, and call dequeue to get the next object
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Set the object to active
        objectToSpawn.SetActive(true);

        // Give it the position and rotation as fed into the params
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
