using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(2, 0, -2));
    }
}
