using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int scene;
    public int health;
    public float[] position;

    public PlayerData (PlayerController player) {
        health = player.GetComponent<Health>().currentHealth;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }


}
