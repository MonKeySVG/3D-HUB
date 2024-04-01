using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Machines
{
    Terminal,
    ServerPC,
    Automatic,
    Other
}
public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;

    public UnityEvent interactAction;

    // public bool isTerminal = false;
    public Machines machine;



    SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake() {
        sceneController = FindAnyObjectByType<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneController == null) {
            sceneController = FindAnyObjectByType<SceneController>();
        }




        if (machine == Machines.Other) {
            if(isInRange && Input.GetKeyDown(interactKey)) {
                interactAction.Invoke();
            }
        } else if (machine == Machines.Terminal) {
            if(isInRange && Input.GetKeyDown(interactKey)) {
                sceneController.originScene = sceneController.GetCurrentScene();
                sceneController.GoToTerminal();
            }
        } else if (machine == Machines.ServerPC) {
            if(isInRange && Input.GetKeyDown(interactKey)) {
                sceneController.originScene = sceneController.GetCurrentScene();
                sceneController.GoToPCServer();
            }
        } else if (machine == Machines.Automatic) {
            if(isInRange) {
                interactAction.Invoke();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            isInRange = true;
            Debug.Log("Is in range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            isInRange = false;
            Debug.Log("Is not in range.");
        }
    }


}
