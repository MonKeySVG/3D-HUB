using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PCServerManager : MonoBehaviour
{

    bool isLeavingScene = false;

    private void Start() {


        


    }

    private void OnGUI() {




        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isLeavingScene) {
                SceneController.instance.BackFromTerminal();
                isLeavingScene=true;
            }
            
        }
    }

    
}
