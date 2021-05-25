using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    //Nome do npc
    public Text nameText;
    //Dialogo do npc
    public Text dialogueText;

    //Texto que aparece quando se aproxima do monge
    public GameObject fToChat;

    //vai meter o chat ativo e desativo
    public Transform dialogueBoxGUI;

    // velocidade das teclas do texto a aparecer
    public float letterDelay = 0.1f;
    // se carregarmos no F continuadamente é a velicidade que aumenta
    public float letterMultiplier = 0.5f;

    // tecla para o Dialogue, para continuar e para dar speed
    public KeyCode DialogueInput = KeyCode.F;

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;



    void Start()
    {

        dialogueText.text = "";
    }

    // Distancia do player ao npc
    public void EnterRangeOfNPC()
    {
        outOfRange = false;

        fToChat.SetActive(true);

        if (dialogueActive == true)
        {
            fToChat.SetActive(false);
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Entrou2");
            //se for falso
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                // se nao for multiplicado
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    // phrases
    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    // se a tecla F for clicada
                    if (Input.GetKey(DialogueInput))
                    {
                        // aumenda a velocidade
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                    }
                    else
                    {
                        //volta a velocidade normal
                        yield return new WaitForSeconds(letterDelay);

                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }

            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        fToChat.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            fToChat.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}
