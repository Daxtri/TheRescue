using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBox : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, GetComponent<BoxCollider>().size);
    }
}
