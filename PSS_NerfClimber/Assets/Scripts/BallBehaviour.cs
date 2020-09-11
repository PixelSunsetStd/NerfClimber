using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int _combo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<AudioSource>().enabled)
            GetComponent<AudioSource>().Play();

        switch (collision.collider.tag)
        {
            case "Obstacle":
                _combo++;
                break;

            case "Canon":
                _combo = 0;
                break;
        }

        if (collision.collider.CompareTag("Obstacle"))
        {
            
        }
    }
}
