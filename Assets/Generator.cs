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

    void Update()
    {
        float spawnDistance = Vector3.Distance(transform.position, player.transform.position);

        if (spawnDistance < distance)
        {
            if (enemies.Count < 3)
            {
                Vector3 randomPos = new Vector3(Random.Range(0, radius), transform.position.y, Random.Range(0, radius));
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