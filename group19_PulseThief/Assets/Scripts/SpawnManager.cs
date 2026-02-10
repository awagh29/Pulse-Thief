using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3[] enemyPrefabs;
    public GameObject spawnPrefab;
    public float startDelay = 5.0f;
    public float spawnInterval = 10.0f;
    private int counter = 0;
    public bool inSafeZone = false;
    void Start()
    {
        StartCoroutine(SpawnEnemy());  
    }

    void Update()
    {
    }

    IEnumerator SpawnEnemy()
    {
        while(!inSafeZone)
        {
            yield return new WaitForSeconds(startDelay);
            if (enemyPrefabs != null)
            {
                while (counter < 4)
                {
                    for (int i = 0; i < enemyPrefabs.Length; i++)
                    {
                        Vector3 spawnPos = new Vector3(
                            enemyPrefabs[i].x,
                            enemyPrefabs[i].y,
                            enemyPrefabs[i].z);
                        Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
                        counter++;
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            else
            {
                Debug.Log("Prefabs not added!");
            }
        }
    }
}
