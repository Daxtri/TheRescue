using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> targetsList;
    public GameObject pos;
    public bool positioned;

    private void Start()
    {
        positioned = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject c in targetsList)
            {
            if (c == null)
            {
                targetsList.Remove(c);
                Debug.Log($"{targetsList.Count}");
            }
        }
    }

    private void LateUpdate()
    {
        if (targetsList.Count <= 0 && positioned == false)
        {
            player.transform.position = pos.transform.position;
            StartCoroutine(Position());
        }
    }

    IEnumerator Position()
    {
        yield return new WaitForSeconds(0.5f);
        positioned = true;
    }
}
