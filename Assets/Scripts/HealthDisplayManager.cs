using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthDisplayManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Health playerHealth;

    List<HealthHeart> hearts = new List<HealthHeart>();

    public string[] scenesToCheck = new string[] { "Terminal", "SampleScene", "NomScene3" };

    public void ClearHearths() {
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

    public void CreateHeart() {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        hearts.Add(heartComponent);
    }

    public void UpdateHearts() {

        ClearHearths();
        
        for (int i=0; i<playerHealth.currentHealth; i++) {
            CreateHeart();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        bool sceneMatchFound = false;

        foreach (string sceneName in scenesToCheck)
        {
            if (currentSceneName == sceneName)
            {
                SetChildrenActive(gameObject, false);
                sceneMatchFound = true;
                
                return;
                
            }
        }

        if (!sceneMatchFound)
        {
            playerHealth = FindAnyObjectByType<PlayerController>().GetComponent<Health>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        bool sceneMatchFound = false;

        foreach (string sceneName in scenesToCheck)
        {
            if (currentSceneName == sceneName)
            {
                SetChildrenActive(gameObject, false);
                sceneMatchFound = true;
                
                return;
                
            }
        }

         if (!sceneMatchFound)
        {
            SetChildrenActive(gameObject, true);

            if (playerHealth == null) {

                playerHealth = FindAnyObjectByType<PlayerController>().GetComponent<Health>();

            }

            UpdateHearts();
        }
        
        
    }

    private void SetChildrenActive(GameObject parent, bool active)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
        
}
