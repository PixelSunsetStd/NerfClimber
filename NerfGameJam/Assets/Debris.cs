using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Debris : MonoBehaviour
{
    ParticleSystemForceField forcefield;
    ParticleSystem particle;
    private void Awake()
    {
        forcefield = FindObjectOfType<ParticleSystemForceField>();
        particle = GetComponent<ParticleSystem>();

        particle.trigger.SetCollider(0, forcefield.transform);
    }
}
