using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy3 : EnemySpawner
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject fodder;
    [SerializeField] private GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        clear();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void spawnFodder()
    {
        int rand = Random.Range(0, 5);
        Instantiate(fodder, new Vector3(180, 3, 0), Quaternion.identity);
    }
    public override void spawnWave()
    {
        switch (waves)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(robot, new Vector3(160 + i * 10, 3, 0), Quaternion.identity);
                }
                waves++;
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(robot, new Vector3(160 + i * 10, 3, 0), Quaternion.identity);
                }
                waves++;
                break;
            case 2:
                battleEnd = true;
                break;
        }
    }
}
