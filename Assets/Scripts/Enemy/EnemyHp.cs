using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float currentHp;
    public float maxHp = 100f;

    public GameObject hpUI;
    ItemDrop getItem;
    void Start()
    {
        currentHp = maxHp;
     
        getItem = GetComponent<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        //  slider.value = CalculateHP();

        //if (currentHp < maxHp)
        //   hpUI.SetActive(true);
            
        if (currentHp <= 0)
        {
            //if (getItem != null)
            //{
            //    getItem.DropItem();
            //    Debug.Log("Dropped an Item " + getItem);
            //}
            Destroy(gameObject);
        }

        if (currentHp > maxHp)
            currentHp = maxHp;
    }

    float CalculateHP()
    {
        return currentHp / maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }

}
