using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;
    public TMPro.TMP_InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect scrollRect;
    public GameObject commandList;

    public int caretWidth;

    Interpreter interpreter;

    bool isLeavingScene = false;

    public Color connectedColor = new Color(0.5f, 0.8f, 0.2f);

    private void Start() {

        userInputLine.SetActive(false);
        interpreter = GetComponent<Interpreter>();

        // AddInterpreterLines(interpreter.LoadTitle("ascii.txt", "red", 0));
        // AddInterpreterLines(interpreter.LoadSubtitle("red", 2));

        // StartCoroutine(AddInterpreterLinesDelayed(interpreter.LoadSubtitle("red", 2), 0.003f));
        // StartCoroutine(AddInterpreterLinesDelayed(interpreter.LoadTitle("ascii.txt", "red", 2), 0.003f));

        StartCoroutine(StartCoroutinesSequentially());

        

        userInputLine.transform.SetAsLastSibling();

        // terminalInput.ActivateInputField();
        // terminalInput.Select();

        terminalInput.caretWidth = caretWidth;
    }

    private void OnGUI() {

        if (interpreter.isConnected) {
            userInputLine.GetComponentInChildren<TextMeshProUGUI>().color = connectedColor;
        } else {
            userInputLine.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

        }
        
        if (!Input.GetKeyDown(KeyCode.Escape)) {
            terminalInput.ActivateInputField();
            terminalInput.Select();

            if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return)) {

                string userInput = terminalInput.text;

                // Clear the input field
                ClearInputField();

                AddDirectoryLine(userInput);

                int lines = AddInterpreterLines(interpreter.Interpret(userInput));

                ScrollToBottom(lines);

                userInputLine.transform.SetAsLastSibling();

                terminalInput.ActivateInputField();
                terminalInput.Select();
            }

        } else {
            if (!isLeavingScene) {
                SceneController.instance.BackFromTerminal();
                isLeavingScene=true;
            }
            
        }
    }

    void ClearInputField() {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput) {
        Vector2 commandListSize = commandList.GetComponent<RectTransform>().sizeDelta;
        commandList.GetComponent<RectTransform>().sizeDelta = new Vector2 (commandListSize.x, commandListSize.y + 17);

        GameObject command = Instantiate(directoryLine, commandList.transform);

        command.transform.SetSiblingIndex(commandList.transform.childCount -1);

        command.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = userInput;
    }

    int AddInterpreterLines(List<String> interpretation) {

        for (int i = 0; i < interpretation.Count; i++) {
            GameObject res = Instantiate(responseLine, commandList.transform);

            res.transform.SetAsLastSibling();

            Vector2 listSize = commandList.GetComponent<RectTransform>().sizeDelta;
            commandList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 17);

            res.GetComponentInChildren<TMPro.TextMeshProUGUI>().text =  interpretation[i];
        }

        return interpretation.Count;
    }

    IEnumerator AddInterpreterLinesDelayed(List<String> interpretation, float delayPerCharacter) {
        foreach (string line in interpretation) {
            GameObject res = Instantiate(responseLine, commandList.transform);
            res.transform.SetAsLastSibling();

            Vector2 listSize = commandList.GetComponent<RectTransform>().sizeDelta;
            commandList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 17);

            TMPro.TextMeshProUGUI textComponent = res.GetComponentInChildren<TMPro.TextMeshProUGUI>();

            userInputLine.transform.SetAsLastSibling();
            ScrollToBottom(interpretation.Count);
            textComponent.text = "";
            // Iterate over each character in the line
            foreach (char c in line) {
                // Append the character to the text component
                textComponent.text += c;

                // Wait for the delay before appending the next character
                yield return new WaitForSeconds(delayPerCharacter);
            }

            
        }
    }

    IEnumerator StartCoroutinesSequentially() {
        // Start the first coroutine
        yield return StartCoroutine(AddInterpreterLinesDelayed(interpreter.LoadTitle("ascii.txt", "red", 0), 0.001f));
        
        yield return StartCoroutine(AddInterpreterLinesDelayed(interpreter.LoadSubtitle("red", 2), 0.001f));
       
       yield return new WaitForSeconds(0.7f);

       userInputLine.SetActive(true);
    }

    void ScrollToBottom(int lines) {

        scrollRect.verticalNormalizedPosition=0;

    }
}
