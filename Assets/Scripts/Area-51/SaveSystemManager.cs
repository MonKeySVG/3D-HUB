using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            player = FindAnyObjectByType<PlayerController>();
        }
    }

    public void SavePlayer() {
        
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        Health health = player.GetComponent<Health>();

        health.currentHealth = data.health;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];



        player.transform.position = position;
    }
}
