using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy2 : MonoBehaviour
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
        if (target.transform.position.x >= 75)
        {
            for (int i = 0; i < 5; i++)
            {
                int randompos = Random.Range(-10, 10);
                Instantiate(robot, new Vector3(randompos + 75, 4 + transform.position.y, 0), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
