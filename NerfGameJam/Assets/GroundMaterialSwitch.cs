using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaterialSwitch : MonoBehaviour
{
    public Material groundMaterial;

    
    string groundTag = "Ground";
    private void OnEnable()
    {
        Switch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch()
    {
        GameObject.FindGameObjectWithTag(groundTag).GetComponent<Renderer>().material = groundMaterial;
    }
}
