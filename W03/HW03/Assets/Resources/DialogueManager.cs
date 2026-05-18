using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueLoader loader;
    public string eventName;
    List<Dialogue> dialogueList;
    int index = 0;
    GameObject nameObj;
    GameObject textObj;

    void Start()
    {
        nameObj = GameObject.Find("Name");
        textObj = GameObject.Find("Text");

        StartDialogue();
    }

    void StartDialogue()
    {
        dialogueList = loader.GetDialogue(eventName);
        index = 0;
        ShowDialogue();
    }

    void ShowDialogue() {
        string name = dialogueList[index].name;
        string text = dialogueList[index].text.Replace("\\n", "\n");
        Debug.Log(name + " : " + text);
        nameObj.GetComponent<TextMeshProUGUI>().text = "<" + name + ">";
        textObj.GetComponent<TextMeshProUGUI>().text = text;
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
