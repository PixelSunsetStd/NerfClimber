using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves away the title when Player enters its trigger
/// </summary>
public class TitleMove : MonoBehaviour
{
    [SerializeField]
    string animationTrigger = "MoveAway";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger(animationTrigger);
        }
    }
}
