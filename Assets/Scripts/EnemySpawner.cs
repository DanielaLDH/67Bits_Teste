using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Prefab do inimigo que será spawnado.")]
    [SerializeField] GameObject enemyPrefab;

    [Tooltip("Intervalo de tempo entre cada spawn.")]
    [SerializeField] float spawnInterval;

    [Tooltip("Ponto mínimo da área de spawn.")]
    [SerializeField] Vector3 spawnAreaMin;

    [Tooltip("Ponto máximo da área de spawn.")]
    [SerializeField] Vector3 spawnAreaMax;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gera inimigos em intervalos regulares
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    //instancia um novo inimigo em uma posição aleatória dentro da área de spawn
    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
