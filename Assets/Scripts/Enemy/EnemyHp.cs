using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float hp = 100;
    public float maxHp;

    public GameObject hpUI;
    ItemDrop getItem;
    void Start()
    {
        hp = maxHp;
     
        getItem = GetComponent<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
      //  slider.value = CalculateHP();

        if (hp < maxHp)
            hpUI.SetActive(true);

        if (hp <= 0)
        {
            if (getItem != null)
            {
                getItem.DropItem();
                Debug.Log("Dropped an Item " + getItem);
            }
            Destroy(gameObject);
        }

        if (hp > maxHp)
            hp = maxHp;
    }

    float CalculateHP()
    {
        return hp / maxHp;
    }

}
