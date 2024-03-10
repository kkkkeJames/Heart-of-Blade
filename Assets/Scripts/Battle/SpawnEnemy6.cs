using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy6 : EnemySpawner
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject fodder;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject elite;
    [SerializeField] private GameObject elite2;
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
        Instantiate(fodder, new Vector3(530, -30, 0), Quaternion.identity);
        Instantiate(fodder, new Vector3(600, -30, 0), Quaternion.identity);
    }
    public override void spawnWave()
    {
        switch (waves)
        {
            case 0:
                Instantiate(elite, new Vector3(566, -28, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(556, -40, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(576, -40, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(550, -20, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(580, -20, 0), Quaternion.identity);
                waves++;
                break;
            case 1:
                Instantiate(elite2, new Vector3(530, -30, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(530, -10, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(556, -40, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(576, -40, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(580, -20, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(600, -30, 0), Quaternion.identity);
                waves++;
                break;
            case 2:
                Instantiate(elite, new Vector3(565, -10, 0), Quaternion.identity);
                Instantiate(elite2, new Vector3(600, -30, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(550, -20, 0), Quaternion.identity);
                Instantiate(robot, new Vector3(580, -20, 0), Quaternion.identity);
                waves++;
                break;
            case 3:
                battleEnd = true;
                waves++;
                break;
        }
    }
}
