using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemies;
    public float radius = 5f;

    void Update()
    {
        if (enemies.Count < 3)
        {
            GameObject enemy1 = Instantiate(enemy, transform.position, Quaternion.identity);
            enemies.Add(enemy1);
        }

        foreach (GameObject e in enemies)
        {
            if (e == null)
                enemies.Remove(e);
        }
    }
}
