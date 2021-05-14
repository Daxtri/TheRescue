using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests1 : MonoBehaviour
{
    public bool isQuestStart;
    public GameObject Hud_StartLvL1;

    void Start()
    {
        isQuestStart = false;
        Hud_StartLvL1.SetActive(false);
    }

    void Update()
    {

        if (isQuestStart)
        {
            Hud_StartLvL1.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isQuestStart = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Hud_StartLvL1.SetActive(false);

    }
}
