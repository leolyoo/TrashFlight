using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    private float[] arrPosX = { -2.2f, -1.1f, 0, 1.1f, 2.2f };

    [SerializeField]
    private float spawnInterval = 1.5f;

    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        float moveSpeed = 5f;
        int enemyIndex = 0;
        int spawnCount = 0;

        yield return new WaitForSeconds(3f);

        while (true)
        {
            foreach (float posX in arrPosX)
            {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;

            if (spawnCount % 10 == 0)
            {
                enemyIndex++;
                moveSpeed += 2;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0)
        {
            index++;
        }

        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }
}
