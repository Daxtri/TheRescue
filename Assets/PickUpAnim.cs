using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        switch (this.transform.tag)
        {
            case "Health":
                this.transform.Rotate(new Vector3(2, 0, -2));
                break;

            case "Armor":
                this.transform.Rotate(new Vector3(2, 0, -2));
                break;

            case "RifleAmmo":
                this.transform.Rotate(new Vector3(0, 2, 0));
                break;

            case "SniperAmmo":
                this.transform.Rotate(new Vector3(0, 2, 0));
                break;
        }
    }
}