using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy4 : EnemySpawner
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject fodder;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject elite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void spawnFodder()
    {
        int rand = Random.Range(0, 3);
        Instantiate(fodder, new Vector3(230 + 10 * rand, 10, 0), Quaternion.identity);
    }
    public override void spawnWave()
    {
        switch (waves)
        {
            case 0:
                Instantiate(elite, new Vector3(280, -48, 0), Quaternion.identity);
                waves++;
                break;
            case 1:
                Instantiate(elite, new Vector3(280, -48, 0), Quaternion.identity);
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(robot, new Vector3(260 + i * 20, -48, 0), Quaternion.identity);
                }
                waves++;
                break;
            case 2:
                battleEnd = true;
                break;
        }
    }
}
