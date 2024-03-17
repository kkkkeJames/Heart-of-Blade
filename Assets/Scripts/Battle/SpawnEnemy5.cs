using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy5 : EnemySpawner
{
    [SerializeField] private GameObject target;
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
    }
    public override void spawnWave()
    {
        switch (waves)
        {
            case 0:
                Instantiate(elite, new Vector3(396, -36, 0), Quaternion.identity);
                Instantiate(elite, new Vector3(396, -1, 0), Quaternion.identity);
                Instantiate(elite2, new Vector3(456, -5, 0), Quaternion.identity);
                Instantiate(elite2, new Vector3(435, -48, 0), Quaternion.identity);
                battleEnd = true;
                break;
        }
    }
}
