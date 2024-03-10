using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battle : MonoBehaviour
{
    [SerializeField] public GameObject EnemySpawner;
    [SerializeField] public GameObject BattleSeal;
    [SerializeField] public GameObject player;
    [SerializeField] public Vector2 StartPosition;
    [SerializeField] public bool WithElite;
    public float BattleTime;
    public bool activated;
    // Start is called before the first frame update
    protected void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!activated && player.transform.position.x > StartPosition.x)
        {
            EnemySpawner.SetActive(true);
            if (BattleSeal != null) BattleSeal.SetActive(true);
            activated = true;
        }
        if (activated)
        {
            BattleTime += Time.deltaTime;
            if (BattleTime > 10 && !EnemySpawner.GetComponent<EnemySpawner>().battleEnd)
            {
                EnemySpawner.GetComponent<EnemySpawner>().spawnFodder();
                BattleTime = 0;
            }
            if (WithElite)
            {
                if (BattleTime > 5 && !EnemySpawner.GetComponent<EnemySpawner>().battleEnd && GameObject.FindGameObjectsWithTag("EliteEnemy").Length == 0)
                {
                    EnemySpawner.GetComponent<EnemySpawner>().spawnWave();
                    BattleTime = 0;
                }
            }
            else
            {
                if (BattleTime > 5 && !EnemySpawner.GetComponent<EnemySpawner>().battleEnd && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    EnemySpawner.GetComponent<EnemySpawner>().spawnWave(); 
                    BattleTime = 0;
                }
            }

            if (EnemySpawner.GetComponent<EnemySpawner>().battleEnd)
            {
                BattleEnd();
            }
        }
    }
    protected void BattleEnd()
    {
        EnemySpawner.SetActive(false);
        BattleSeal.SetActive(false);
        Destroy(gameObject);
    }
}
