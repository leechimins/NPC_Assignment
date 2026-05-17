using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueLoader loader;
    public string eventName;
    List<Dialogue> dialogueList;
    int index = 0;

    void Start()
    {
        StartDialogue();
    }

    void StartDialogue()
    {
        dialogueList = loader.GetDialogue(eventName);
        index = 0;
        ShowDialogue();
    }

    void ShowDialogue()
    {
        string name = dialogueList[index].name;
        string text = dialogueList[index].text.Replace("\\n", "\n");
        Debug.Log(name + " : " + text);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        index++;
        if (index < dialogueList.Count)
            ShowDialogue();
    }
}
