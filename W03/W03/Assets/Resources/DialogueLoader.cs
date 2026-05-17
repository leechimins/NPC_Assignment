using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public Dictionary<string, List<Dialogue>> dialogueDict = new Dictionary<string, List<Dialogue>>();

    private void Awake() {
        LoadCSV();
    }

    void LoadCSV()
    {
        TextAsset csv = Resources.Load<TextAsset>("W03_Dialogue");
        string[] lines = csv.text.Split('\n');
        string currentEvent = "";
        List<Dialogue> currentList = null;
        for (int i = 1; i < lines.Length; i++) {
            string[] row = lines[i].Split(',');

            string eventName = row[0].Trim();
            string name = row.Length > 1 ? row[1].Trim() : " ";
            string text = row.Length > 2 ? row[2].Trim() : " ";

            if (eventName == "end") {
                if (currentEvent != "")
                    dialogueDict[currentEvent] = currentList;
                currentEvent = "";
                currentList = null;
                continue;
            }

            if (eventName != "") {
                currentEvent = eventName;
                currentList = new List<Dialogue>();
            }

            if (currentList != null && text != "") {
                currentList.Add(new Dialogue(name, text));
            }
        }
    }

    public List<Dialogue> GetDialogue(string eventName) {
        return dialogueDict[eventName];
    }
}
