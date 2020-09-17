using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextParticleSystemController : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public ParticleSystem textParticleSystem;
    private ParticleSystemRenderer rendererSystem;
    // Start is called before the first frame update
    void OnEnable()
    {
        rendererSystem = textParticleSystem.GetComponent<ParticleSystemRenderer>();
        rendererSystem.mesh = textMeshPro.mesh;

        //textMeshPro.color = Color.Lerp(Color.white, Color.red, GetComponentInParent<TrafficBehaviour>()._numberOfCollisions / 5.0f);

        textMeshPro.text = "Nice";
    }
}
