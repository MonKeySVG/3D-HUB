using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWorldFromHub : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            // Charger une nouvelle scène (remplacez "NomDeLaScene" par le nom de la scène que vous souhaitez charger)
            SceneController.instance.GoToScene(sceneName);
        }
    }
}
