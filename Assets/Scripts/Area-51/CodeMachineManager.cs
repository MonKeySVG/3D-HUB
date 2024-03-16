using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodeMachineManager : MonoBehaviour
{
    public UnityEvent actionOnCorrectCode;
    public TMP_Text digit1;
    public TMP_Text digit2;
    public TMP_Text digit3;
    public TMP_Text digit4;

     public Image bar1;
     public Image bar2;
     public Image bar3;
     public Image bar4;

    private List<String> code = new List<String>();

    string codeToDisplay = "";


    public string CorrectCode = "0000";

    OpenUI openUI;


    // Start is called before the first frame update
    void Start()
    {
        digit1.text = " ";   
        digit2.text = " ";   
        digit3.text = " ";   
        digit4.text = " ";   

        openUI = GameObject.FindGameObjectWithTag("UI").GetComponent<OpenUI>();
    }

    // public void OnButtonClick(string button) {

    //     if (!codeMachine.isDoorOpened) {
    //         if (button == "Delete") {
    //         if (Code.Length > 0) {
    //             Code = Code.Substring(0, Code.Length - 1);
    //             UpdateDisplayedText(Code);
    //         }
    //     }
    //     else if (button == "Submit"){
            
    //         if (Code.Length != 4) {
    //             Debug.Log("Code faux");
    //         } else if (Code != CorrectCode) {
    //             Debug.Log("Code faux");
    //         } else {
    //             Debug.Log("Code Bon");
    //             openUI.Resume();
    //             StartCoroutine(codeMachine.OpenDoorCoroutine());
    //         }
    //     }
    //     else {
    //         if (Code.Length < 4) {
    //             Code += button;
    //             UpdateDisplayedText(Code);
    //         }
    //     }
    //     }

        
    // }

    public void UpdateDisplayedText(string codeToDisplay)
    {
        // Supprimer tout dans la liste de string
            code.Clear();
            digit1.text = "";   
            digit2.text = "";   
            digit3.text = "";   
            digit4.text = "";  

            // Ajouter chaque caractère de codeToDisplay à la liste de string
            for (int i = 0; i < codeToDisplay.Length; i++)
            {
                // Verification que c'est bien un chiffre
                if (char.IsDigit(codeToDisplay[i]))
                {
                    code.Add(codeToDisplay[i].ToString());
                }
                else
                {
                    // Message d'erreur
                }
            }



             


            // Changer les valeurs des textes affichés avec les valeurs contenues dans la liste de string
            if (code.Count >= 1)
            {
                digit1.text = code[0];
            }
            if (code.Count >= 2)
            {
                digit2.text = code[1];
            }
            if (code.Count >= 3)
            {
                digit3.text = code[2];
            }
            if (code.Count >= 4)
            {
                digit4.text = code[3];
            }
    }

    private void Update()
    {
        // Debug.Log(codeToDisplay);
        // Exemple de code pour la touche "Escape" pour reprendre le jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openUI.ResumeAnCloseUI();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (codeToDisplay.Length > 0)
            {
                
                
                codeToDisplay = codeToDisplay.Substring(0, codeToDisplay.Length - 1);
                UpdateDisplayedText(codeToDisplay);

                Debug.Log(codeToDisplay);
            }
        }

        if (codeToDisplay.Length < 4) {
            // Détecter l'entrée pour chaque chiffre de 0 à 9
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                UpdateDisplayedText(codeToDisplay += "0");
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateDisplayedText(codeToDisplay += "1");
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateDisplayedText(codeToDisplay += "2");
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpdateDisplayedText(codeToDisplay += "3");
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                UpdateDisplayedText(codeToDisplay += "4");
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                UpdateDisplayedText(codeToDisplay += "5");
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                UpdateDisplayedText(codeToDisplay += "6");
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
            {
                UpdateDisplayedText(codeToDisplay += "7");
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                UpdateDisplayedText(codeToDisplay += "8");
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                UpdateDisplayedText(codeToDisplay += "9");
            }
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            if (codeToDisplay == CorrectCode) {
                actionOnCorrectCode.Invoke();
            } else {
                Debug.Log("Code faux");
            }
        }



        
    }

    public void updateBars() {
        // Mettre a jour l'affichache dynamique des barres pour que la barre actuellement active soit surélevée

        // int basePosition = -133;
        // Vector3 newPosition = bar1.transform.position;
        // newPosition.y = -133;

        






        // if (codeToDisplay.Length == 0) {
        //     newPosition = bar1.transform.position;
        //     newPosition.y += 50;
        //     bar1.transform.position = newPosition;
        //     return;
        // }
    }

}
