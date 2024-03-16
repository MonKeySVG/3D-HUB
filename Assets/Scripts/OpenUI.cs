using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OpenUI : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject ui;

    public HealthDisplayManager hud;

    private Volume globalVolume;
    public VolumeProfile defaultProfile; 
    public VolumeProfile screenProfile; 

    // Start is called before the first frame update
    void Start()
    {
        globalVolume = FindObjectOfType<Volume>();

        if (globalVolume == null)
        {
            Debug.LogError("Aucun volume global trouvé dans la scène.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        

        if (globalVolume == null)
        {
            globalVolume = FindObjectOfType<Volume>();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                ResumeAnCloseUI();
            }
        }
    }

    public void PauseAndOpenUI () {
        
        hud = FindAnyObjectByType<HealthDisplayManager>();

        if (hud != null) {
            hud.gameObject.SetActive(false);
        } 
        
        ui.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (globalVolume != null)
        {

            globalVolume.profile = screenProfile;
            
        }
    }

    public void ResumeAnCloseUI () {
        if (globalVolume != null)
        {
            globalVolume.profile = defaultProfile;

            
        }
        hud = FindObjectOfType<HealthDisplayManager>(true);
            
        hud.gameObject.SetActive(true);
 
        ui.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
