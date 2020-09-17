using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextParticleSystemController : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public ParticleSystem textParticleSystem;
    private ParticleSystemRenderer rendererSystem;

    public string _textToDisplay;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textParticleSystem = GetComponent<ParticleSystem>();
        rendererSystem = textParticleSystem.GetComponent<ParticleSystemRenderer>();
    }

    // Start is called before the first frame update
    public void OnEnable()
    {
        rendererSystem.mesh = textMeshPro.mesh;
        textMeshPro.text = _textToDisplay;
        textParticleSystem.Play();
    }
}
