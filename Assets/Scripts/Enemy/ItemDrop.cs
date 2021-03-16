using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    [SerializeField]
    private GameObject[] itemList; //Objetos que vão dropar -> ( public GameObject[] )
    private Transform NPCpos; //NPC posição;
    private int itemNum; // Seleciona o numero da itemlist
    private int itemNum2;
    private int randNum;


    private void Start()
    {

        NPCpos = GetComponent<Transform>();

    }

    public void DropItem()
    {
        //RANDOMIZE
        randNum = Random.Range(0, 101); // 100% total for determining loot chance;
        //INSTACIAR OBJETO
        if (randNum <= 75)
        {
            itemNum = 1;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
        }
        else if (randNum > 75 && randNum < 95)
        {
            itemNum = 0;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
        }
        else if (randNum >= 95)
        {
            itemNum = 0;
            itemNum2 = 1;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
            Instantiate(itemList[itemNum2], NPCpos.position, Quaternion.identity);
        }
    }
}