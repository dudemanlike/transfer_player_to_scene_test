using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
    private static PlayerPersistence instance;

    private void Awake()
    {
        if (instance == null)
        {
            // If this is the first instance, set it as the instance
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // Persist the entire hierarchy

            // Find all SpawnPoint objects in the scene and mark them as persistent
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
            foreach (GameObject spawnPoint in spawnPoints)
            {
                DontDestroyOnLoad(spawnPoint);
            }
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
}
