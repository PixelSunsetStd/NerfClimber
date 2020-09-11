using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetVictory : MonoBehaviour
{
    Animator animator;
    public GameObject planet;
    BallBehaviour ballBehaviour;
    GameObject ball;

    bool animLaunched;
    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        animator = transform.parent.GetComponent<Animator>();
        ballBehaviour = ball.GetComponent<BallBehaviour>();
    }

    

    [ContextMenu("Victory")]
    public void VictoryAnimation()
    {
        planet.transform.position = ball.transform.position;
        animLaunched = true;
        animator.SetTrigger("Activate");
    }
}
