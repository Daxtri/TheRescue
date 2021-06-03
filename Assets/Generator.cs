using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public List<GameObject> enemies;
    public float radius = 5f;
    public float distance = 10f;
    public int MAX_ENEMIES = 3;

    void Update()
    {
        float spawnDistance = Vector3.Distance(transform.position, player.transform.position);

        if (spawnDistance < distance)
        {
            if (enemies.Count < MAX_ENEMIES)
            {
                Vector3 randomPos = new Vector3(Random.Range(-radius, radius), transform.position.y, Random.Range(-radius, radius));
                GameObject enemy1 = Instantiate(enemy, transform.position + randomPos, Quaternion.identity);
                enemies.Add(enemy1);
            }
        }

        foreach (GameObject e in enemies)
        {
            if (e == null)
                enemies.Remove(e);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}