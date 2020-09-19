using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomStartAnimation : MonoBehaviour
{
    [SerializeField]
    string animationTriggerName = "Activate";
    Animator animator;
    public float delay;

    [Space(1)]
    [SerializeField]
    bool randomMode = true;
    [SerializeField]
    float randomMin = 0;
    [SerializeField]
    float randomMax = 1;
    


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        LaunchAnim();

    }

    private void LaunchAnim()
    {
        StartCoroutine(LaunchAnimAfterDelay(delay));
    }

    void Start()
    {
        
    }

   IEnumerator LaunchAnimAfterDelay(float _delay)
    {
        if (randomMode)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(randomMin, randomMax));

        }
        else
        yield return new WaitForSeconds(_delay);

        animator.SetTrigger(animationTriggerName);
    }
}
