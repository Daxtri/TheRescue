using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest0 : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    public SpawnPlayer spawn;
    //Nome do NPC
    public string Name;

    //Mexer no tamanho da Area da Caixa de Texto, danos mais espaço para escrever nas frases
    [TextArea(5, 10)]

    public string[] phrases;


    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.positioned)
        {
            dialogueSystem.DropDialogue();
        }

    }
    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<Quests2>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();

        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
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
        this.gameObject.GetComponent<Quests2>().enabled = false;
    }


}
