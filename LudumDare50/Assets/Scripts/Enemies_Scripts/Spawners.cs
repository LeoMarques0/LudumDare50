using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] private Vector2 spawnMinPos = Vector2.zero, spawnMaxPos = Vector2.zero;
    [SerializeField] private List<ScoreDificulty> scoreDificulties = new List<ScoreDificulty>();

    private int currentDificulty = 0;
    public List<GameObject> possibleEnemies = new List<GameObject>();
    public List<GameObject> currentEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnerTimer());
    }

    private void Update()
    {
    }


    private IEnumerator SpawnerTimer()
    {
        while(GameManager.singleton.playerIsAlive)
        {
            if(currentEnemies.Count < scoreDificulties[currentDificulty].maxAmountOfZombies)
            {
                Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(spawnMinPos.x, spawnMaxPos.x), UnityEngine.Random.Range(spawnMinPos.y, spawnMaxPos.y));
                Enemy_Base newEnemy = FindSpawnableEnemy().GetComponent<Enemy_Base>();
                newEnemy.transform.position = spawnPos;
                currentEnemies.Add(newEnemy.gameObject);
                newEnemy.Respawn();
                newEnemy.spawner = this;
            }
            yield return null;
        }
    }

    private GameObject FindSpawnableEnemy()
    {
        GameObject enemy;
        do
        {
            enemy = possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)];
        } while (currentEnemies.Contains(enemy));

        return enemy;
    }

    public void CheckDificulty()
    {
        if (currentDificulty == scoreDificulties.Count - 1)
            return;

        if (ScoreManager.instance.score > scoreDificulties[currentDificulty + 1].scoreTarget)
            currentDificulty++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(spawnMaxPos.x, spawnMaxPos.y), new Vector2(spawnMaxPos.x, spawnMinPos.y));
        Gizmos.DrawLine(new Vector2(spawnMaxPos.x, spawnMaxPos.y), new Vector2(spawnMinPos.x, spawnMaxPos.y));
        Gizmos.DrawLine(new Vector2(spawnMaxPos.x, spawnMinPos.y), new Vector2(spawnMinPos.x, spawnMinPos.y));
        Gizmos.DrawLine(new Vector2(spawnMinPos.x, spawnMaxPos.y), new Vector2(spawnMinPos.x, spawnMinPos.y));
    }
}

[Serializable]
public class ScoreDificulty
{
    public int scoreTarget = 0;
    public int maxAmountOfZombies = 0;
}
