using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests1 : MonoBehaviour
{
    public bool isQuestStart;
    public GameObject Hud_StartLvL1;

    
    /// 
     Open gate;
    public GameObject bigDoor;
    //variavel para usar variaveis do script DialogueSystem
    private DialogueSystem dialogueSystem;

    //Nome do NPC
    public string Name;

    //Mexer no tamanho da Area da Caixa de Texto, danos mais espaço para escrever nas frases
    [TextArea(5, 10)]

    public string[] phrases;


    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        
        isQuestStart = false;
        Hud_StartLvL1.SetActive(false);
    }

    void Update()
    {

        //if (isQuestStart)
        //{
        //    Hud_StartLvL1.SetActive(true);

        //}

        if (bigDoor.GetComponent<Open>().finishedLvL)
        {
            dialogueSystem.DropDialogue();
        }

    }
    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Quests1>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();

        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Entrou");
                isQuestStart = true;
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = phrases;
                FindObjectOfType<DialogueSystem>().NPCName();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
       // Hud_StartLvL1.SetActive(false);
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<Quests1>().enabled = false;
    }
}
