using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartColor : MonoBehaviour
{
    [SerializeField]
    Renderer renderer;

    [SerializeField]
    Material[] randomMats;


    private void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
    }
    private void Start()
    {
        //Getmats
        Material[] newMats = renderer.materials;
        int randomNumber = Random.Range(0, randomMats.Length);



        for (int i = 0; i < renderer.materials.Length; i++)
        {

            newMats[i] = randomMats[randomNumber];
        }

        //SetMats
        renderer.materials = newMats;

    }

    
}
