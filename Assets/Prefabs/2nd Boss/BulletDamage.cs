using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float dist = 0.1f;
    public GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }
    void Update()
    {
        if (this.transform.position.y <= -100)
            Destroy(this.gameObject);


        if (Vector3.Distance(this.transform.position, player.transform.position) <= dist)
        {
            player.GetComponent<PlayerController>().TakeDamage(20f);
            Destroy(this.gameObject);
        }
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, dist);
    }
}
