using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x >= -5)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(robot, new Vector3(20 + 5 * i, 1.6f - 0.2f * i, 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
