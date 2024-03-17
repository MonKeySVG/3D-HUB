using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SetCameraRender : MonoBehaviour
{



    void Start() {
        GetComponent<UniversalAdditionalCameraData>().SetRenderer(1);
    }


}
