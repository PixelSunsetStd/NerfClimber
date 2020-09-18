using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectDebris : MonoBehaviour
{
    ParticleSystemForceField forceField;
    public float delay = 0;
    public float timeBeforeStop = 2;

    private void Awake()
    {
        forceField = GetComponent<ParticleSystemForceField>();
    }

    

    /// <summary>
    /// Just Call this method to Collect debris, it handles the rest from here
    /// </summary>
    /// 
    [ContextMenu("Collect")]
    public void Collect()
    {
        StartCoroutine("CollectWaitAndStop");
    }

  

    IEnumerator CollectWaitAndStop()
    {
        yield return new WaitForSeconds(delay);

        forceField.gravity = 2;

        yield return new WaitForSeconds(timeBeforeStop);

        forceField.gravity = 0;

    }

    private void OnParticleCollision(GameObject other)
    {
        StartCoroutine(WaitAndKillParticleSystem(other.gameObject));

    }

    IEnumerator WaitAndKillParticleSystem(GameObject go)
    {
        yield return new WaitForSeconds(3);
        Destroy(go);
    }


}
