using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private float BattleTime;
    public int waves = 0;
    public bool battleEnd;
    public bool nextWave;
    protected void Start()
    {
        spawnWave();
    }

    // Update is called once per frame
    protected void Update()
    {
    }
    public abstract void spawnFodder();
    public abstract void spawnWave();
    public void clear()
    {
        GameObject[] Enemy_living = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in Enemy_living)
        {
            Destroy(enemy);
        }
        GameObject[] EliteEnemy_living = GameObject.FindGameObjectsWithTag("EliteEnemy");
        foreach (GameObject enemy in EliteEnemy_living)
        {
            Destroy(enemy);
        }
    }
}
